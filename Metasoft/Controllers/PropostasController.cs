using System;
using System.Linq;
using System.Web.Mvc;
using Metasoft.Models;
using Metasoft.Infrastructure;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace Metasoft.Controllers
{
    public class PropostasController : Controller
    {
        private readonly IRepository repository;
        public PropostasController(IRepository objIrepository) { repository = objIrepository; }
        // GET: Propostas
        public ActionResult ListaPropostas()
        {
            ViewBag.categoriaselected = "0"; ViewBag.clienteselected = "0"; ViewBag.deselected = ""; ViewBag.ateselected = ""; ViewBag.situacaoselected = "0"; ViewBag.responsavelselected = "";
            PopulateDropDown("fp", "");
            var result = repository.PropostasAll("","","0",0,0,0,"");
            if(result != null)
            {
                if (result.Count() > 0)
                {
                    return View(result);
                }
            }
         
            return View();
        }
        [HttpPost]
        public ActionResult ListaPropostas(FormCollection fc)
        {
            //ViewBag.situacaoselected = String.IsNullOrEmpty(fc["situacao"].ToString()) ? "0" : fc["situacao"].ToString();

            
            try { ViewBag.situacaoselected = fc["situacao"].ToString(); } catch { ViewBag.situacaoselected = "0"; }
            ViewBag.categoriaselected = String.IsNullOrEmpty(fc["categorias"].ToString()) ? "0" : fc["categorias"].ToString();
            ViewBag.clienteselected = String.IsNullOrEmpty(fc["clientes"].ToString()) ? "0" : fc["clientes"].ToString();
            ViewBag.responsavelselected = String.IsNullOrEmpty(fc["responsaveis"].ToString()) ? "" : fc["responsaveis"].ToString();
            ViewBag.deselected = String.IsNullOrEmpty(fc["de"].ToString()) ? "" : fc["de"].ToString();
            ViewBag.ateselected = String.IsNullOrEmpty(fc["ate"].ToString()) ? "" : fc["ate"].ToString();

            /* Download flag */
            string download = String.IsNullOrEmpty(fc["download"].ToString()) ? "" : fc["download"].ToString();

            if (download == "true")
            {
                DownloadExcel(ViewBag.deselected, ViewBag.ateselected, int.Parse(ViewBag.categoriaselected), int.Parse(ViewBag.clienteselected), int.Parse(ViewBag.situacaoselected),"");
            }


            PopulateDropDown("fp", "");
            var result = repository.PropostasAll(ViewBag.deselected, ViewBag.ateselected,"0", int.Parse(ViewBag.categoriaselected), int.Parse(ViewBag.clienteselected), int.Parse(ViewBag.situacaoselected), ViewBag.responsavelselected);
            if (result != null)
            {
                return View(result);
            }
            return View();
            }

        public void DownloadExcel(string de, string ate, int categoria, int cliente, int situacao, string responsavel)
        {
            var result = repository.PropostasAll(de, ate, "0",categoria, cliente, situacao, responsavel);
            /* Convert para ViewModel Excel mais simples */
            List<PropostaDownloadExcel> propostas = new List<PropostaDownloadExcel>();

            foreach(var item in result)
            {
                PropostaDownloadExcel pd = new PropostaDownloadExcel
                {
                    Proposta = item.propostaid.ToString(),
                    Cliente = item.clientenome,
                    Categoria = item.categorianome,
                    Descricao = item.descricao,
                    Valor = item.valor,
                    Faturado = item.faturado,
                    Recebido = (item.recebido==true) ? "Sim": "Não",
                    Previsao = item.previsao,
                    Situacao = item.situacao,
                    Envio = item.envio,
                    Aceite = item.dtaceite,
                    Finalizacao = item.dtfinalizacao,
                    Num_po = item.numpo,
                    Num_nf = item.numnf,
                    HE = item.horasestimadas,
                    HU = item.horasutilizadas
                };
                propostas.Add(pd);
            }

            var grid = new System.Web.UI.WebControls.GridView();
            var lista = propostas;
            grid.DataSource = lista;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Propostas_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }

        public ActionResult GetClientes(string tipo = null)
        {
            var result = repository.CliForAll(tipo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetResponsaveis(string tipo = null)
        {
            var result = repository.GetResponsaveis();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NovaProposta()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaProposta([Bind(Include = "propostaid,clienteid,categoriaid,descricao,he,hu,numpo,valor,he,previsao, responsavel")] Proposta proposta, FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {

                proposta.situacaoid = 1;
                proposta.clienteid = int.Parse(fc["clienteid"].ToString());
                proposta.categoriaid = int.Parse(fc["categoriaid"].ToString());
                proposta.responsavel = fc["responsaveis"];
                int contaprid = repository.NovaProposta(proposta);
                if (contaprid > 0)
                {
                    return RedirectToAction("ListaPropostas");
                }
                //else
                //{

                //    return Json("error", JsonRequestBehavior.AllowGet);
                //}

            }
            ViewBag.categoriaid = proposta.categoriaid;
            ViewBag.clienteid = proposta.clienteid;
            return View();
        }
        public ActionResult AlterarProposta(int id)
        {

            Proposta proposta = repository.GetProposta(id);
            if (proposta == null)
            {
                return HttpNotFound();
            }
            /* Passa id para posicionar dropdowns */
            proposta.valor = Decimal.Parse(proposta.valor.ToString().Replace(".", ","));
            ViewBag.categoriaid = proposta.categoriaid;
            ViewBag.clienteid = proposta.clienteid;
            ViewBag.responsavel = proposta.responsavel ?? "";

            return View(proposta);
        }
        [HttpPost]
        public ActionResult AlterarProposta( Proposta proposta, FormCollection fc)
        {
            Proposta old = repository.GetProposta(proposta.propostaid);
            int clienteid = int.Parse(fc["clienteid"].ToString());
            int categoriaid = int.Parse(fc["categoriaid"].ToString());
            string responsavel = fc["responsaveis"];
            old.categoriaid = categoriaid;
            old.clienteid = clienteid;
            old.responsavel = responsavel ?? "";
            old.descricao = proposta.descricao;
            old.numpo = proposta.numpo;
            old.he = proposta.he;
            old.hu = proposta.hu;
            old.previsao = proposta.previsao;
            old.valor = proposta.valor;


            if (ModelState.IsValid)
            {
               
                Boolean result = repository.AlterarProposta(old);
                if (result)
                {
                    return RedirectToAction("ListaPropostas");
                }
                
            }
            ViewBag.clienteid = proposta.clienteid;
            ViewBag.categoriaid = proposta.categoriaid;
            ViewBag.responsavel = proposta.responsavel;
            return View(proposta);
        }
        public ActionResult ExcluirProposta(int id)
        {

            var result = repository.ExcluirProposta(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private void PopulateDropDown(string table, object selected = null)
        {
            string chave = ""; string valor = "";
            switch (table.ToLower())
            {
                case "clientes":
                    //chave = "clienteid"; valor = "nome";
                    //var clienteslist = repository.ClientesAll();
                    //if (clienteslist == null) { RedirectToAction("ErroConexao"); }
                    //ViewBag.ClientesList = new SelectList(clienteslist, chave, valor, selected);
                    break;
                case "contatos":
                    //chave = "contatoid"; valor = "nome";
                    //var list = repository.ContatosCliente(Int32.Parse(selected.ToString()));
                    //ViewBag.contatosList = new SelectList(list, chave, valor, selected);
                    break;

                case "fp":
                    chave = "fpid"; valor = "nome";
                    var fps = repository.FpsAll();
                    ViewBag.fp = new SelectList(fps, chave, valor, selected);
                    break;

                default:
                    break;
            }

        }
        public ActionResult ShowContasProposta(int propostaid)
        {
            var result = repository.GetContasProposta(propostaid);
            return PartialView("_Lancamentos", result);
        }

        public ActionResult ShowDetalhes_e_NotasFiscaisProposta(int propostaid)
        {
            var result = repository.GetDetalhes_e_Nfs_Proposta(propostaid);
            return PartialView("_Detalhes_e_Nfs_Proposta", result);
        }
        /* 25/07/2018 */
        public ActionResult ShowVisitas(int propostaid)
        {
            var result = repository.ShowVisitas(propostaid);
            return PartialView("_ShowVisitas", result);
        }

        public ActionResult GetClienteDetails(int clienteid)
        {
            var result = repository.GetClienteDetail(clienteid);
            return PartialView("_DetalhesCliente", result);
        }
        public ActionResult AceitarProposta(int propostaid, string dataaceite)
        {
            string sqldata = dataaceite.Substring(6, 4) + "-" + dataaceite.Substring(0, 2) + "-" + dataaceite.Substring(3, 2);
            string query = "update propostas set situacaoid=2,dtaceite= CONVERT(DateTime, '" + sqldata + "', 104) where propostaid =" + propostaid;
            Boolean result = repository.UpdateTable(query);
            if (result)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult NegarProposta(int propostaid, string dataaceite,string motivo="")
        {
            string sqldata = dataaceite.Substring(6, 4) + "-" + dataaceite.Substring(0, 2) + "-" + dataaceite.Substring(3, 2);
            string query = "update propostas set situacaoid=3,dtaceite= CONVERT(DateTime, '"+ sqldata+ "', 104), motivo = '" + motivo+ "' where propostaid =" + propostaid;
            Boolean result = repository.UpdateTable(query);
            if (result)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }   
        public ActionResult LiberarProposta(int propostaid)
        {
            string query = "update propostas set situacaoid= 4 where propostaid =" + propostaid;
            Boolean result = repository.UpdateTable(query);
            if (result)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult AvancarFluxo(int propostaid, int currentstatus)
        {
            int nextstatus = 0;
            switch (currentstatus)
            {
                case 2:
                    /* Aceita, avanca para aguardando po */
                    nextstatus = 7; break;
                case 7:
                    /* Aguardando PO , avanca para liberada */
                    nextstatus = 4; break;
                case 4:
                    /* Liberada , avanca para Faturada */
                    nextstatus = 5; break;

            }

            string query = "update propostas set situacaoid= " + nextstatus.ToString() + " where propostaid =" + propostaid;
            Boolean result = repository.UpdateTable(query);
            if (result)
            {
               return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult VoltarFluxo(int propostaid,int currentstatus)
        {
            int prevstatus = 0;
            switch (currentstatus)
            {
                case 2:
                case 3:
                    /* Aceita, volta para em analise */
                    prevstatus = 1;break;
                case 7:
                    /* Aguardando PO ,volta para Aceita */
                    prevstatus = 2; break;
                case 4:
                    /* Liberada ,volta para Aguardando PO */
                    prevstatus = 7; break;
                case 5:
                    /* Liberada ,volta para Liberada */
                    prevstatus = 4; break;



            }
            if (prevstatus != 0)
            {
                string query = "update propostas set situacaoid= " + prevstatus.ToString();
                if(prevstatus ==1) {
                    query += ",dtaceite = NULL ";
                }
                query +=" where propostaid =" + propostaid;
                Boolean result = repository.UpdateTable(query);
                if (result)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("error", JsonRequestBehavior.AllowGet);
        }
        /* 24/07/2018 */

        public ActionResult NovaVisita (int id)
        {
            Visita visita = new Visita();
            visita.propostaid = id;

            return View(visita);
        }
        [HttpPost]
        public ActionResult NovaVisita(Visita visita, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                int result = repository.NovaVisita(visita);
                return RedirectToAction("ListaPropostas");
            }
                return View();
        }

        protected Double ToDecimal(string source)
        {
            NumberFormatInfo valor = new NumberFormatInfo();
            valor.NumberDecimalSeparator = ",";
            return double.Parse(source, valor);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
