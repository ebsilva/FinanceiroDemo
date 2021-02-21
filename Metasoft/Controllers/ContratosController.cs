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
    public class ContratosController : Controller
    {
        private readonly IRepository repository;
        public ContratosController(IRepository objIrepository) { repository = objIrepository; }
        public ActionResult ListaContratos()
        {
            ViewBag.tipocontratoselected = "0"; ViewBag.clienteselected = "0"; ViewBag.deselected = "";
            ViewBag.ateselected = ""; ViewBag.situacaoselected = "0"; ViewBag.tipodataselected = "";

            var result = repository.ContratosAll("","","",0,0,0);
            if (result.Count() > 0)
            {
                return View(result);
            }
            return View();
        }
        [HttpPost]
        public ActionResult ListaContratos(FormCollection fc)
        {
            ViewBag.tipodataselected = String.IsNullOrEmpty(fc["tipodata"].ToString()) ? "0" : fc["tipodata"].ToString();
            ViewBag.deselected = String.IsNullOrEmpty(fc["de"].ToString()) ? "" : fc["de"].ToString();
            ViewBag.ateselected = String.IsNullOrEmpty(fc["ate"].ToString()) ? "" : fc["ate"].ToString();
            ViewBag.tipocontratoselected = String.IsNullOrEmpty(fc["tiposcontrato"].ToString()) ? "0" : fc["tiposcontrato"].ToString();
            ViewBag.clienteselected = String.IsNullOrEmpty(fc["clientes"].ToString()) ? "0" : fc["clientes"].ToString();
            ViewBag.situacaoselected = String.IsNullOrEmpty(fc["situacao"].ToString()) ? "0" : fc["situacao"].ToString();


            /* Download flag */
            string download = String.IsNullOrEmpty(fc["download"].ToString()) ? "" : fc["download"].ToString();

            if (download == "true")
            {
                DownloadExcel(ViewBag.tipodataselected,ViewBag.deselected, ViewBag.ateselected, int.Parse(ViewBag.tipocontratoselected), int.Parse(ViewBag.clienteselected), int.Parse(ViewBag.situacaoselected));
            }
            var result = repository.ContratosAll(ViewBag.tipodataselected,ViewBag.deselected, ViewBag.ateselected, int.Parse(ViewBag.tipocontratoselected), int.Parse(ViewBag.clienteselected), int.Parse(ViewBag.situacaoselected));
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        public void DownloadExcel(string tipodata,string de, string ate, int categoria, int cliente, int situacao)
        {
            var result = repository.ContratosAll(tipodata,de, ate, categoria, cliente, situacao);
            /* Convert para ViewModel Excel mais simples */
            var grid = new System.Web.UI.WebControls.GridView();
          
            List<ContratosExcelViewModel>  excellist = new List<ContratosExcelViewModel>();
            foreach (var ct in result)
            {
                ContratosExcelViewModel cte = new ContratosExcelViewModel
                {
                    Numero = ct.id,
                    Cliente = ct.clientenome,
                    Indice = ct.indice,
                    Tipo_Contrato = ct.tipocontrato,
                    Inicio = ct.dtinicio,
                    Termino = ct.dttermino,
                    Valor = ct.valor,
                    Reajuste = ct.proxreajuste,
                    Prazo = ct.prazo,
                    Observacao = ct.observacao,
                    Situacao = ct.situacao
                };
                excellist.Add(cte);
            }

            grid.DataSource = excellist;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Contratos_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public ActionResult GetClientes(string tipo = null)
        {
            var result = repository.GetClientesWithPropostas();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NovoContrato()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoContrato([Bind(Include = "clienteid,tipocontrato,indice,descricao,observacao,dtinicio,dttermino,iniciosv,valor,prazo,proxreajuste")] ContratoViewModel contrato, FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {

 
                contrato.clienteid = int.Parse(fc["clienteid"].ToString());
                contrato.tipocontrato = int.Parse(fc["tipocontrato"].ToString());
                contrato.indice = int.Parse(fc["indice"].ToString());
                contrato.prazo= int.Parse(fc["prazo"].ToString());
                /* Calcula prazo */


                int id = repository.NovoContrato(contrato);
                if (id > 0)
                {
                    return RedirectToAction("ListaContratos");
                }

            }
            ViewBag.indice = contrato.indice;
            ViewBag.tipocontrato = contrato.tipocontrato;
            ViewBag.clienteid = contrato.clienteid;
            return View();
        }

        public ActionResult LancarCr( int id)
        {
            ContratoViewModel contrato = repository.GetContrato(id);
            ContaPR conta = new ContaPR
            {
                categoriaid = contrato.tipocontrato,
                cliforid = contrato.clienteid,
                contratoid = contrato.renovacaoid
            };
            var parcela = repository.CountContasReceberForContrato(id);

            ViewBag.categoriaid = conta.categoriaid;
            ViewBag.clienteid = conta.cliforid;
            ViewBag.contratoid = conta.contratoid;
            string no = (parcela + 1).ToString().PadLeft(2,'0');
            conta.noordem = no + "/" + contrato.prazo.ToString();
            conta.valor = Math.Round(Decimal.Parse((contrato.valor / contrato.prazo).ToString()), 2);
            conta.vencimento = contrato.iniciosv.Value.AddMonths(parcela+1);
            return View(conta);
        }
        [HttpPost]
        public ActionResult LancarCr([Bind(Include = "contaprid,cliforid,categoriaid,contratoid,propostaid,descricao,noordem,npar,valor,vencimento,situacao,recorrente")] ContaPR conta, FormCollection fc)
        {

            conta.tipo = "R";
            conta.dtcad = DateTime.Today;
            conta.situacao = "A";

            int par = int.Parse(conta.noordem.Substring(0, 2));
            conta.npar = par;
            try { conta.propostaid = int.Parse(fc["propostaid"].ToString()); } catch (Exception exc) { conta.propostaid = 0;  var msg = exc.Message; }

            //if (ModelState.IsValid)
            //{
                
               // conta.cliforid = int.Parse(fc["cliforid"].ToString());
                
                /* remove commas with tokem remove dots */
                string svalor = conta.valor.ToString();
                svalor.Replace(",", "#").Replace(".", "").Replace("#", ".");
                conta.valor = decimal.Parse(svalor, NumberStyles.AllowDecimalPoint);
                int contaprid = repository.NovaContaPR(conta);
                if (contaprid > 0)
                {
                    //AddNewHistory(contaprid, "P", "Pagamento confirmado em " + DateTime.Now.ToString());
                    return RedirectToAction("ListaContratos");
                }

            //} else
            //{
            //    foreach (ModelState state in ViewData.ModelState.Values.Where(x => x.Errors.Count > 0))
            //    {
            //        var erro = state.ToString();
            //    }
            //}
            try { ViewBag.clienteid = int.Parse(fc["cliforid"].ToString()); } catch { }
            try { ViewBag.contratoid = int.Parse(fc["propostaid"].ToString()); } catch { }
            try { ViewBag.categoriaid = int.Parse(fc["categoriaid"].ToString()); } catch { }
            return View(conta);
        }

        public ActionResult ShowContasContrato(int id)
        {
            var result = repository.GetContasContrato(id);
            return PartialView("_Lancamentos", result);
        }

        public ActionResult PopulatePropostasCliente(int situacaoid, int clienteid)
        {
            var result = repository.GetPropostasCliente(situacaoid, clienteid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AlterarContrato(int id)
        {

            ContratoViewModel contratorenovacao = repository.GetContrato(id);
            if (contratorenovacao == null)
            {
                return HttpNotFound();
            }
            /* Passa id para posicionar dropdowns */
            
            ViewBag.clienteid = contratorenovacao.clienteid;
            ViewBag.tipocontrato = contratorenovacao.tipocontrato;
            ViewBag.indice = contratorenovacao.indice;
            ViewBag.prazo = contratorenovacao.prazo;
            return View(contratorenovacao);
        }
        [HttpPost]
        public ActionResult AlterarContrato([Bind(Include = "id,renovacaoid,dtcad,clienteid,tipocontrato,indice,descricao,observacao,dtinicio,dttermino,iniciosv,valor,prazo,proxreajuste,situacaoid,situacaorenovacaoid")] ContratoViewModel contrato, FormCollection fc)
        {
            contrato.clienteid = int.Parse(fc["clienteid"].ToString());
            contrato.tipocontrato = int.Parse(fc["tipocontrato"].ToString());
            contrato.indice = int.Parse(fc["indice"].ToString());
            contrato.prazo = int.Parse(fc["prazo"].ToString());

            if (ModelState.IsValid)
            {
               
                Boolean result = repository.AlterarContrato(contrato);
                if (result)
                {
                    return RedirectToAction("ListaContratos");
                }
            }
            ViewBag.clienteid = contrato.clienteid;
            ViewBag.tipocontrato = contrato.tipocontrato;
            ViewBag.indice = contrato.indice;
            ViewBag.prazo = contrato.prazo;
            return View(contrato);
        }
        public ActionResult ExcluirContrato(int id)
        {

            var result = repository.ExcluirContrato(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateTiposContratos()
        {
            var result = repository.TiposContratoAll();
           return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateIndices()
        {
            var result = repository.IndicesAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateClientesWithProposta()
        {
            
           var result = repository.GetClientesWithPropostas();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowHistorico(int id)
        {
            var result = repository.GetHistoricoContrato(id);
            return PartialView("_Historico", result);
        }
        //public ActionResult GetCliFor(string tipo = null)
        //{
        //    var result = repository.CliForAll(tipo);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

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
