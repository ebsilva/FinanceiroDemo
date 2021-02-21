using System;
using System.Net;
using System.Web.Mvc;
using Metasoft.Models;
using Metasoft.Infrastructure;
using System.Globalization;
using Metasoft.Utilities;
using System.IO;
using System.Web.UI;
using Metasoft.ViewModels;
using System.Collections.Generic;

namespace Metasoft.Controllers
{
    public class ContaPRsController : Controller
    {
        private readonly IRepository repository;
        public ContaPRsController(IRepository objIrepository) { repository = objIrepository; }
        #region Apagar

        public ActionResult ApagarAnalitico(string fromresumodiario = "", string sort = "")
        {

            ViewBag.categoriaselected = "0"; ViewBag.fornecedorselected = "0"; ViewBag.deselected = ""; ViewBag.tipodataselected = "vencimento";
            ViewBag.ateselected = ""; ViewBag.situacaoselected = ""; ViewBag.sortorder = ""; ViewBag.anoselected = "0";

            var result = repository.ContaPRsAll("P", "vencimento", "", "", "0", 0, 0, "", "", sort.TrimEnd());
            if (result != null)
            {
                return View(result);
            }

            return View();

        }
        [HttpPost]
        public ActionResult ApagarAnalitico(FormCollection fc, string sortcol = "")
        {

            ViewBag.tipodataselected = String.IsNullOrEmpty(fc["tipodata"].ToString()) ? "0" : fc["tipodata"].ToString();
            ViewBag.situacaoselected = String.IsNullOrEmpty(fc["situacao"].ToString()) ? "0" : fc["situacao"].ToString();
            ViewBag.categoriaselected = String.IsNullOrEmpty(fc["categorias"].ToString()) ? "0" : fc["categorias"].ToString();
            ViewBag.fornecedorselected = String.IsNullOrEmpty(fc["fornecedores"].ToString()) ? "0" : fc["fornecedores"].ToString();
            ViewBag.deselected = String.IsNullOrEmpty(fc["de"].ToString()) ? "" : fc["de"].ToString();
            ViewBag.ateselected = String.IsNullOrEmpty(fc["ate"].ToString()) ? "" : fc["ate"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? "0" : fc["anos"].ToString();

            string download = String.IsNullOrEmpty(fc["download"].ToString()) ? "" : fc["download"].ToString();

            if (download == "true")
            {
                DownloadExcel("P", ViewBag.deselected, ViewBag.ateselected, int.Parse(ViewBag.categoriaselected), int.Parse(ViewBag.fornecedorselected), ViewBag.situacaoselected);
            }


            var result = repository.ContaPRsAll("P", ViewBag.tipodataselected, ViewBag.deselected, ViewBag.ateselected, ViewBag.anoselected, int.Parse(ViewBag.categoriaselected), int.Parse(ViewBag.fornecedorselected), ViewBag.situacaoselected, "");
            if (result != null) { return View(result); }
            return View();
        }

        public ActionResult ApagarPorCategoria()
        {

            string mes = DateTime.Now.Month.ToString();
            string ano = DateTime.Now.Year.ToString();
            ViewBag.messelected = mes;
            ViewBag.anoselected = ano;
            var result = repository.ContaPRsPorCategoria("P", mes, ano);

            if (result != null) { return View(result); }
            return View();
        }
        [HttpPost]
        public ActionResult ApagarPorCategoria(FormCollection fc)
        {

            ViewBag.messelected = String.IsNullOrEmpty(fc["meses"].ToString()) ? DateTime.Now.Year.ToString() : fc["meses"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? DateTime.Now.Year.ToString() : fc["anos"].ToString();

            var result = repository.ContaPRsPorCategoria("P", ViewBag.messelected, ViewBag.anoselected);

            if (result != null) { return View(result); }
            return View();
        }
        public ActionResult NovaContaApagar()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaContaApagar([Bind(Include = "contaprid,cliforid,propostaid,categoriaid,descricao,noordem,npar,valor,vencimento,situacao,recorrente")] ContaPR contapr, FormCollection fc)
        {


            if (ModelState.IsValid)
            {
                contapr.tipo = "P";
                contapr.cliforid = int.Parse(fc["cliforid"].ToString());
                contapr.archive = false;

                /* remove commas with tokem remove dots */
                string svalor = contapr.valor.ToString();
                svalor.Replace(",", "#").Replace(".", "").Replace("#", ".");
                contapr.valor = decimal.Parse(svalor, NumberStyles.AllowDecimalPoint);
                int contaprid = repository.NovaContaPR(contapr);
                if (contaprid > 0)
                {
                    AddNewHistory(contaprid, "P", "Conta cadastrada em " + DateTime.Now.ToString());
                    return RedirectToAction("ApagarAnalitico");
                }
                else
                {

                    SaveCurrentValuesToViewBag(contapr);
                }
            }
            else
            {
                SaveCurrentValuesToViewBag(contapr);
            }

            return View(contapr);
        }
        public ActionResult AlterarContaApagar(int id)
        {

            ContaPR conta = repository.GetConta(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoriaid = conta.categoriaid;
            ViewBag.cliforid = conta.cliforid;
            ViewBag.recorrente = conta.recorrente;
            return View(conta);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarContaApagar([Bind(Include = "contaprid,dtcad,categoriaid,npar,cliforid,propostaid,valor,vencimento,tipo,dtpagto,situacao,recorrente,descricao,noordem,contagrupo,contratoid")] ContaPR contapr, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                contapr.cliforid = int.Parse(fc["cliforid"].ToString());
                string svalor = contapr.valor.ToString();
                svalor.Replace(",", "#").Replace(".", "").Replace("#", ".");
                contapr.valor = decimal.Parse(svalor, NumberStyles.AllowDecimalPoint);
                Boolean result = repository.AlterarConta(contapr);
                if (result) { return RedirectToAction("ApagarAnalitico"); }
                else { return Json("error", JsonRequestBehavior.AllowGet); }
            }

            return View(contapr);
        }
        public ActionResult NovaParcelaApagar(int? id)
        {

            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            ContaPR contaanterior = repository.FindConta(id);
            if (contaanterior != null)
            {
                string noordem = contaanterior.noordem;
                string[] parcelas = noordem.Split('/');
                string proximaparcela = (Int32.Parse(parcelas[0]) + 1).ToString().PadLeft(2, '0') + "/" + parcelas[1];
                ContaPR proximaconta = new ContaPR();

                proximaconta.categoriaid = contaanterior.categoriaid;
                proximaconta.cliforid = contaanterior.cliforid;
                proximaconta.descricao = contaanterior.descricao;
                proximaconta.noordem = proximaparcela;
                proximaconta.situacao = "A";
                proximaconta.contagrupo = contaanterior.contagrupo;

                ViewBag.cliforid = contaanterior.cliforid;
                Cliente cliente = repository.GetCliFor(contaanterior.cliforid);
                ViewBag.clifornome = cliente.nome;
                ViewBag.categoriaid = contaanterior.categoriaid;
                Categoria categoria = repository.CategoriaById(contaanterior.categoriaid);
                ViewBag.categorianome = categoria.nome;
                return View(proximaconta);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaParcelaApagar([Bind(Include = "contaprid,cliforid,propostaid,categoriaid,descricao,noordem,npar,valor,vencimento,situacao,recorrente,contagrupo")] ContaPR contapr, FormCollection fc)
        {

            if (ModelState.IsValid)
            {

                int cliforid = int.Parse(fc["cliforid"].ToString());
                if (AddParcela("P", cliforid, contapr)) { return RedirectToAction("ApagarAnalitico"); }
                else { return View(contapr); }
            }
            return View();
        }
        #endregion

        #region Areceber
        public ActionResult AreceberAnalitico(string fromresumodiario = "", string sort = "")
        {
            ViewBag.tipodataselected = "vencimento";
            ViewBag.categoriaselected = "0"; ViewBag.cliforselected = "0"; ViewBag.deselected = ""; ViewBag.ateselected = ""; ViewBag.situacaoselected = "";
            ViewBag.responsavelselected = ""; ViewBag.sortorder = ""; ViewBag.anoselected = "0";
            var result = repository.ContaPRsAll("R", "vencimento", "", "", "0", 0, 0, "", "", sort.TrimEnd());

            if (result != null) { return View(result); }
            return View();
        }
        [HttpPost]
        public ActionResult AreceberAnalitico(FormCollection fc)
        {
            ViewBag.tipodataselected = String.IsNullOrEmpty(fc["tipodata"].ToString()) ? "tipodata" : fc["tipodata"].ToString();
            ViewBag.situacaoselected = String.IsNullOrEmpty(fc["situacao"].ToString()) ? "0" : fc["situacao"].ToString();
            ViewBag.categoriaselected = String.IsNullOrEmpty(fc["categorias"].ToString()) ? "0" : fc["categorias"].ToString();
            ViewBag.cliforselected = String.IsNullOrEmpty(fc["clientes"].ToString()) ? "0" : fc["clientes"].ToString();
            ViewBag.deselected = String.IsNullOrEmpty(fc["de"].ToString()) ? "" : fc["de"].ToString();
            ViewBag.ateselected = String.IsNullOrEmpty(fc["ate"].ToString()) ? "" : fc["ate"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? "" : fc["anos"].ToString();
            string download = String.IsNullOrEmpty(fc["download"].ToString()) ? "" : fc["download"].ToString();

            if (download == "true")
            {
                DownloadExcel("R", ViewBag.deselected, ViewBag.ateselected, int.Parse(ViewBag.categoriaselected), int.Parse(ViewBag.cliforselected), ViewBag.situacaoselected);

            }

            var result = repository.ContaPRsAll("R", ViewBag.tipodataselected, ViewBag.deselected, ViewBag.ateselected, ViewBag.anoselected, int.Parse(ViewBag.categoriaselected), int.Parse(ViewBag.cliforselected), ViewBag.situacaoselected, "", ViewBag.sortorder, "");
            if (result != null) { return View(result); }
            return View();
        }
        public void DownloadExcel(string tipo, string de, string ate, int categoria, int clifor, string situacao)
        {
            var result = repository.ContaPRExcel(tipo, de, ate, categoria, int.Parse(ViewBag.cliforselected), situacao, "");
            var grid = new System.Web.UI.WebControls.GridView();
            var lista = result;
            grid.DataSource = lista;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Lancamento_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();



        }
        public ActionResult AreceberPorCategoria()
        {

            string mes = DateTime.Now.Month.ToString();
            string ano = DateTime.Now.Year.ToString();
            ViewBag.messelected = mes;
            ViewBag.anoselected = ano;

            var result = repository.ContaPRsPorCategoria("R", mes, ano);
            if (result != null) { return View(result); }
            return View();
        }
        [HttpPost]
        public ActionResult AreceberPorCategoria(FormCollection fc)
        {

            ViewBag.messelected = String.IsNullOrEmpty(fc["meses"].ToString()) ? DateTime.Now.Year.ToString() : fc["meses"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? DateTime.Now.Year.ToString() : fc["anos"].ToString();

            var result = repository.ContaPRsPorCategoria("R", ViewBag.messelected, ViewBag.anoselected);
            if (result != null) { return View(result); }
            return View();
        }
        public ActionResult AlterarContaAreceber(int id)
        {

            ContaPR conta = repository.GetConta(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoriaid = conta.categoriaid;
            ViewBag.cliforid = conta.cliforid;
            ViewBag.propostaid = conta.propostaid;
            ViewBag.recorrente = conta.recorrente;
            return View(conta);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarContaAreceber([Bind(Include = "contaprid,dtcad,categoriaid,npar,cliforid,valor,vencimento,tipo,dtpagto,situacao,recorrente,descricao,noordem,contagrupo,contratoid,nf")] ContaPR contapr, FormCollection fc)
        {

            if (ModelState.IsValid)
            {

                contapr.cliforid = int.Parse(fc["cliforid"].ToString());
                try { contapr.propostaid = int.Parse(fc["propostaid"].ToString()); } catch (Exception exc) { contapr.propostaid = 0; var msg = exc.Message; }
                string svalor = contapr.valor.ToString();
                svalor.Replace(",", "#").Replace(".", "").Replace("#", ".");
                contapr.valor = decimal.Parse(svalor, NumberStyles.AllowDecimalPoint);
                Boolean result = repository.AlterarConta(contapr);
                if (result) { return RedirectToAction("AreceberAnalitico"); }
                else { return Json("error", JsonRequestBehavior.AllowGet); }
            }

            return View(contapr);
        }
        public ActionResult NovaContaAreceber()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaContaAreceber([Bind(Include = "contaprid,cliforid,categoriaid,descricao,noordem,npar,valor,vencimento,situacao,recorrente,nf")] ContaPR contapr, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                contapr.tipo = "R";
                contapr.cliforid = int.Parse(fc["cliforid"].ToString());
                try { contapr.propostaid = int.Parse(fc["propostaid"].ToString()); } catch (Exception exc) { contapr.propostaid = 0; var msg = exc.Message; }
                try { contapr.nf = fc["nf"].ToString(); } catch (Exception exc) { contapr.nf = ""; var msg = exc.Message; }
                /* remove commas with tokem remove dots */
                string svalor = contapr.valor.ToString();
                svalor.Replace(",", "#").Replace(".", "").Replace("#", ".");
                contapr.valor = decimal.Parse(svalor, NumberStyles.AllowDecimalPoint);
                int contaprid = repository.NovaContaPR(contapr);
                if (contaprid > 0)
                {
                    AddNewHistory(contaprid, "P", "Conta cadastrada em " + DateTime.Now.ToString());
                    return RedirectToAction("AreceberAnalitico");
                }

            }
            try { ViewBag.cliforselected = int.Parse(fc["cliforid"].ToString()); } catch { }
            try { ViewBag.propostaselected = int.Parse(fc["propostaid"].ToString()); } catch { }
            try { ViewBag.categoriaselected = int.Parse(fc["categoriaid"].ToString()); } catch { }


            return View(contapr);
        }
        public ActionResult NovaParcelaAreceber(int? id)
        {

            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            ContaPR contaanterior = repository.FindConta(id);
            if (contaanterior != null)
            {
                string noordem = contaanterior.noordem;
                string[] parcelas = noordem.Split('/');
                string proximaparcela = (Int32.Parse(parcelas[0]) + 1).ToString().PadLeft(2, '0') + "/" + parcelas[1];
                ContaPR proximaconta = new ContaPR();

                proximaconta.categoriaid = contaanterior.categoriaid;
                proximaconta.cliforid = contaanterior.cliforid;
                proximaconta.descricao = contaanterior.descricao;
                proximaconta.noordem = proximaparcela;
                proximaconta.situacao = "A";
                proximaconta.contagrupo = contaanterior.contagrupo;

                ViewBag.cliforid = contaanterior.cliforid;
                Cliente cliente = repository.GetCliFor(contaanterior.cliforid);
                ViewBag.clifornome = cliente.nome;
                ViewBag.categoriaid = contaanterior.categoriaid;
                Categoria categoria = repository.CategoriaById(contaanterior.categoriaid);
                ViewBag.categorianome = categoria.nome;
                return View(proximaconta);
            }


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaParcelaAreceber([Bind(Include = "contaprid,cliforid,propostaid,categoriaid,descricao,noordem,npar,valor,vencimento,situacao,recorrente,contagrupo")] ContaPR contapr, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                int cliforid = int.Parse(fc["cliforid"].ToString());
                if (AddParcela("R", cliforid, contapr)) { return RedirectToAction("AreceberAnalitico"); }
                else { return View(contapr); }
            }

            return View();
        }
        private Boolean AddParcela(string tipo, int cliforid, ContaPR contapr)
        {
            contapr.tipo = tipo;
            contapr.cliforid = cliforid;
            string svalor = contapr.valor.ToString();
            svalor.Replace(",", "#").Replace(".", "").Replace("#", ".");
            contapr.valor = decimal.Parse(svalor, NumberStyles.AllowDecimalPoint);
            int contaprid = repository.NovaParcelaPR(contapr);
            if (contaprid > 0) { return true; }
            else { return false; }
        }
        public ActionResult ConfirmaPagamento(int id, string dataquitacao)
        {
            //string sqldate = dtpagto.Substring(6, 4) + "-" + dtpagto.Substring(0, 2) + "-" + dtpagto.Substring(3, 2);
            //var result = repository.UpdateTable("update contaprs set situacao = 'P',dtpagto=CONVERT(DateTime, '" + sqldate + "', 104) where contaprid=" + id.ToString());
            var result = repository.ConfirmaPagamento(id, dataquitacao);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ConfirmaRecebimento(int id, string dataquitacao)
        {
            //string sqldate = dtpagto.Substring(6, 4) + "-" + dtpagto.Substring(0, 2) + "-" + dtpagto.Substring(3, 2);
            var result = repository.ConfirmaRecebimento(id, dataquitacao);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ArquivarPagamento(int id)
        {
            var result = repository.UpdateTable("update contaprs set archive = 1 where contaprid=" + id.ToString());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExcluirPagamento(int id)
        {
            var result = repository.ExcluirPagamento(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NotaFiscalAreceber(int id)
        {
            ClienteContaNota notafiscal= repository.ImprimirNotaFiscal(id);
            return View(notafiscal);

        }
        [HttpPost]
        public ActionResult NotaFiscalAreceber([Bind(Include = "contaprid,cliforid,dtcad,vencimento,emissao,noordem")] NfReceberViewModel notavm, FormCollection fc)
        {
            //NfReceber nota = new NfReceber()
            //{
            //    NumeroOrdem = notavm.NumeroOrdem,
            //    ContaId = notavm.ContaId,
            //    CliForId = notavm.CliForId,
            //    Emissao = DateTime.Now,
            //    Vencimento =notavm.Vencimento,
            //    NumPrint = 0,
            //    Situacao = "A"
            // };

            //return View(notvm);
            return null;
        }
        public void ImprimirNotaFiscalAreceber(int id)
        {

            string[] meses = { ",", "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
            ClienteContaNota ccn = repository.ImprimirNotaFiscal(id);

            string html = "";
            // string openrow = "<div class='row redb'>";
            // string closediv = "</div>";


            html = "<table>" +
                  "<tr>" +
                  "<td>Inscrição Municipal</td><td>3.700.319-4</td><td>NOTA DE LOCAÇÃO DE BENS MÓVEIS</td>" +
                  "</tr>" +
                   "<tr>" +
                  "<td>CNPJ</td><td>09.235.470/0001-09</td><td>1º VIA CLIENTE&nbsp;&nbsp;&nbsp;&nbsp;Nota Fical nº&nbsp;" + ccn.Nota.NfId.ToString().PadLeft(6, '0') + "</td>" +
                  "</tr>" +
                  "<tr><td>Emissão: " + ccn.Nota.Emissao.ToString() + "</td><td>NATUREZA DA OPERAÇÃO:</td><td>LOCAÇÃO DE BENS MÓVEIS</td></tr>" +
                  "</table>";
            /* Nº ORDEM VENCIMENTO DESTINARIO ENDERECO BAIRRO*/
            html += "<table>" +
                    "<tr><td>Nº ordem&nbsp;" + ccn.Conta.noordem + "</td><td>Vencimento&nbsp;" +ccn.Conta.vencimento.ToString() + "</td></tr>" +
                    "<tr style=border-top:none;><td colspan='2' style='text-align:left;border-top:none;'>Destinatário:&nbsp;" + ccn.Cliente.nome + "</td></tr>" +
                    "</table>" +
                    "<table>" +
                    "<tr><td style='text-align:left;'>Endereço:&nbsp;" + ccn.Cliente.endereco + "</td><td style='text-align:left;'>Bairro:&nbsp;" + ccn.Cliente.bairro + "</td></tr>" +
                    "</table>";
            /*  CIDADE , ESTADO , CEP*/
            html += "<table>" +
                    "<tr><td style='text-align:left;'>Municípo:&nbsp;" + ccn.Cliente.cidade + "</td><td style='text-align:left;'>Estado:&nbsp;" + ccn.Cliente.estado + "</td><td style='text-align:left;'>CEP:&nbsp;" + ccn.Cliente.cep + "</td></tr>" +
                    "</table>";
            /* CNPJ , IE*/
            html += "<table>" +
                    "<tr><td style='text-align:left;'>CNPJ:&nbsp;" + ccn.Cliente.cnpj + "</td><td style='text-align:left;'>INSCR. ESTADUAL:&nbsp;" + ccn.Cliente.ie + "</td></tr>" +
                    "</table>";
            html += "<table>";
            html += "<tr><td style='width:11%'>Quantidade</td><td style='width:63%'>Descrição</td><td style='width:10%'>R$ Unitário</td><td style='width:15%'>R$ Total</td></tr>";
            Decimal totalnf = 0;
            int counter = 1; string pdescricao = "";
            foreach (var item in ccn.Detalhes)
            {
                switch (counter)
                {
                    case 13:
                        pdescricao = "P/ DEPÓSITO BRADESCO";
                        break;
                    case 14:
                        pdescricao = "AGENCIA 0197-0";

                        break;
                    case 15:
                        pdescricao = "CONTA CORRENTE 092200-5";
                        break;
                    case 17:
                        pdescricao = "OBSERVAÇÕES";
                        break;
                    case 18:
                        pdescricao = "'Dispensado de emissão de Documento fiscal, por não constar na lista de";
                        break;
                    case 19:
                        pdescricao = "serviços da Lei 13.701/03'Locação de bens móveis/Não-incidência de ICMS''";
                        break;
                    default:
                        pdescricao = (String.IsNullOrEmpty(item.Descricao)) ? "&nbsp;" : item.Descricao;
                        break;
                }

                var qtd = (item.Quantidade == 0) ? "&nbsp;" : item.Quantidade.ToString();
                var unitario = (item.Unitario == 0) ? "&nbsp;" : item.Unitario.ToString();
                decimal total = (item.Quantidade == 0) ? 0 :  (item.Quantidade * item.Unitario);
                totalnf += total;
                html += "<tr><td style='width:11%'>" + qtd + "</td><td style='width:63%;text-align:center;font-size:9px;'>" + pdescricao + "</td><td style='width:10%'>" + unitario + "</td><td style='width:15%'>" + total + "</td></tr>";
                counter += 1;
            }
            html += "<tr><td colspan='3'>VALOR TOTAL DA NOTA</td><td style='width:15%'>R$&nbsp;" + totalnf.ToString() + "</td></tr>";
            html += "<tr><td colspan='4'  style='text-align:left;'>São Paulo, " + DateTime.Now.Day.ToString() + " de" + meses[DateTime.Now.Month] + " de " + DateTime.Now.Year.ToString() + "</td></tr>";
            html += "<tr><td rowspan='2'>" + ccn.Nota.NfId.ToString() + "</td><td colspan='3'>Identificação e Assinatura do Recebedor</td></tr>";
            html += "<tr><td colspan='3'>&nbsp;</td></tr>";
            html += "</table>";

            string reportname = "Nf_" + Tools.Mdy() + ".pdf";
            //PDFHelper.Export(html, reportname, "~/Content/PdfNf.css");
        }
        public ActionResult CancelarNotaFiscalAreceber(int id)
        {
            var result = repository.ExcluirNotaFiscalAreceber(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region Called by Js
        public ActionResult NotaFiscalUpdateItemQtd(int nfid, int line, int qtd)
        {
           repository.NotaFiscalUpdateItemQtd(nfid, line, qtd);
           return null;
         
        }
        public ActionResult NotaFiscalUpdateItemDescricao(int nfid, int line, string descricao)
        {
            repository.NotaFiscalUpdateItemDescricao(nfid, line, descricao);
            return null;

        }
        public ActionResult NotaFiscalUpdateItemUnitario(int nfid, int line, string unitario)
        {
            repository.NotaFiscalUpdateItemUnitario(nfid, line, unitario);
            return null;

        }
        public ActionResult NotaFiscalGeraNumero(int nfid)
        {
            var nfIdGerado = repository.GetNumeroNotaFiscal(nfid);
            
            if (nfIdGerado == 0)
            {

                nfIdGerado = repository.CriaProximoNumeroNotaFiscal(nfid);
            }

            return Json(nfIdGerado, JsonRequestBehavior.AllowGet);
        }
        public ActionResult  Extrato()
        {
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            var _extrato = repository.Extrato(mes,ano);
            return View(_extrato);
        }
        public ActionResult SaveNewDataInicio(int ano, int id, string datainicio)
        {
            var result = repository.SaveNewDataInicio(ano,id, datainicio);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion js


        #endregion

        #region Relatorios
        public ActionResult DiarioConsolidado()
        {
            string messelected = DateTime.Now.Month.ToString();
            string anoselected = DateTime.Now.Year.ToString();
            ViewBag.messelected = messelected;
            var result = repository.DiarioConsolidado(messelected, anoselected);
            return View(result);
        }
        [HttpPost]
        public ActionResult DiarioConsolidado(FormCollection fc)
        {
            ViewBag.messelected = String.IsNullOrEmpty(fc["meses"].ToString()) ? DateTime.Now.Month.ToString() : fc["meses"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? DateTime.Now.Year.ToString() : fc["anos"].ToString();

            var result = repository.DiarioConsolidado(ViewBag.messelected.ToString(), ViewBag.anoselected.ToString());
            return View(result);
        }
        public ActionResult MensalConsolidado()
        {

            string anoselected = DateTime.Now.Year.ToString();
            ViewBag.anoselected = anoselected;
            var result = repository.MensalConsolidado(anoselected);
            return View(result);
        }
        [HttpPost]
        public ActionResult MensalConsolidado(FormCollection fc)
        {
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? DateTime.Now.Year.ToString() : fc["anos"].ToString();
            var result = repository.MensalConsolidado(ViewBag.anoselected.ToString());
            return View(result);
        }
        public ActionResult OrcamentoAnual()
        {

            ViewBag.anoselected = DateTime.Now.Year.ToString(); ;
            ViewBag.tiposelected = "R";
            var result = repository.OrcamentoAnual(ViewBag.anoselected.ToString(), ViewBag.tiposelected.ToString());
            return View(result);
        }
        [HttpPost]
        public ActionResult OrcamentoAnual(FormCollection fc)
        {
            ViewBag.anoselected = Tools.NotNullString(fc["anos"]);
            ViewBag.tiposelected = String.IsNullOrEmpty(fc["tipo"].ToString()) ? DateTime.Now.Year.ToString() : fc["tipo"].ToString();
            var result = repository.OrcamentoAnual(ViewBag.anoselected.ToString(), ViewBag.tiposelected.ToString());
            return View(result);
        }
        public ActionResult OrcamentoContasMes(string tipo, string ano, int mes, int id, bool all= false)
        {
            var result = repository.OrcamentoContasMes(tipo, ano, mes, id,all);
            return PartialView("_OrcamentoContas", result);
        }

        public ActionResult OrcamentoUpdateCaixa(string ano, string mes, string id, string valor)
        {
            var result = repository.OrcamentoUpdateCaixa( ano, mes, id, valor);
            return PartialView("_OrcamentoCaixaItem", result);
        }
        public ActionResult ContasUpdateSaldoMes(int id, string valor)
        {
            var result = repository.ContasUpdateSaldoMes(id, valor);
            return PartialView("_OrcamentoCaixaItem", result);
        }

        #endregion

        #region Others
        private void SaveCurrentValuesToViewBag(ContaPR conta)
        {
            ViewBag.cliforselected = conta.cliforid;
            ViewBag.categoriaselected = conta.categoriaid;
            ViewBag.descricao = conta.descricao;
            ViewBag.valor = conta.valor;
            ViewBag.vencimento = conta.vencimento;
        }
        public ActionResult ShowLancamentos(string dia, string mes, string ano, string tipo)
        {
            var result = repository.GetLancamentos(dia, mes, ano, tipo);
            return PartialView("_Lancamentos", result);
        }
        public ActionResult ShowLancamentosMes(string mes, string ano, string tipo)
        {

            var result = repository.GetLancamentos(mes, ano, tipo);
            return PartialView("_Lancamentos", result);
        }
        public ActionResult ShowHistory(int fid, string tipo)
        {
            var result = repository.GetHistory(fid, tipo);
            return PartialView("_Historico", result);
        }
        private void PopulateClienteFornecedor(int? selected, string filter = null)
        {
            string chave = ""; string valor = "";
            chave = "cliforid"; valor = "nome";
            var clifor = repository.CliForAll(filter);

            ViewBag.cliforid = new SelectList(clifor, chave, valor, selected.ToString());
        }
        public ActionResult PopulateCategorias(string tipo = "", int id = 0)
        {

            if (String.IsNullOrEmpty(tipo))
            {

                if (id == 0)
                {
                    var result = repository.CategoriasAll();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = repository.CategoriaById(id);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = repository.CategoriasByTipo(tipo);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult PopulatePropostasCliente(int situacaoid, int clienteid)
        {
            var result = repository.GetPropostasCliente(situacaoid, clienteid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCliFor(string tipo = null)
        {
            var result = repository.CliForAll(tipo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPropostasCliente(int clienteid)
        {
            var result = repository.GetPropostasCliente(clienteid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNewHistory(int fid, string tipo, string texto)
        {

            Historico historico = new Historico();
            historico.fid = fid;
            historico.tipo = tipo;
            historico.data = DateTime.Now;
            historico.texto = texto;

            Boolean result = repository.AddNewHistory(historico);
            if (result)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error em SaveNewContato", JsonRequestBehavior.AllowGet);
            }
        } 
        #endregion


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
