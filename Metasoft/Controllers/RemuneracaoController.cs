using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Metasoft.Models;
using Metasoft.Infrastructure;
using System.IO;
using System.Web.UI;

namespace Metasoft.Controllers
{
    public class RemuneracaoController : Controller
    {
        private readonly IRepository repository;

        public RemuneracaoController(IRepository objIrepository) { repository = objIrepository; }
        public ActionResult ListaRemuneracao()
        {
            return View(repository.RemuneracaoAll(DateTime.Now.Month,DateTime.Now.Year));
        }
        [HttpPost]
        public ActionResult ListaRemuneracao(FormCollection fc)
        {
            ViewBag.messelected = String.IsNullOrEmpty(fc["meses"].ToString()) ? DateTime.Now.Month.ToString() : fc["meses"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? DateTime.Now.Year.ToString() : fc["anos"].ToString();
            string download = String.IsNullOrEmpty(fc["download"].ToString()) ? "" : fc["download"].ToString();

            if (download == "true")
            {
                DownloadExcel(int.Parse(ViewBag.messelected), int.Parse(ViewBag.anoselected));
            }

            return View(repository.RemuneracaoAll(int.Parse(ViewBag.messelected), int.Parse(ViewBag.anoselected)));
        }

        public void DownloadExcel(int mes, int ano)
        {
            var result = repository.RemuneracaoAll(mes,ano);
            /* Convert para ViewModel Excel mais simples */

            var grid = new System.Web.UI.WebControls.GridView();
            var lista = result;
            grid.DataSource = lista;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Remuneracoes_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        public ActionResult NovaRemuneracao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaRemuneracao([Bind(Include = "proposta,comercial,insumos,percentual")] NovaRemuneracaoViewModel remuneracao)
        {
            if (ModelState.IsValid)
            {
                Remuneracao r = new Remuneracao
                {
                    dtcad = DateTime.Now,
                    propostaid = remuneracao.proposta,
                    comercialid = remuneracao.comercial,
                    insumos = (remuneracao.insumos != null) ? remuneracao.insumos : 0,
                    percentual = remuneracao.percentual,
                    dtlan = DateTime.Now
                };

                int result = repository.NovaRemuneracao(r);
                
                return RedirectToAction("ListaRemuneracao");
            }

            //return View(comercial);
            return View();
        }

        public ActionResult PopulatePropostasRemuneracao(string dropdown)
        {
            var result = repository.GetPropostasRemuneracao();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateComercialRemuneracao(string dropdown)
        {
            var result = repository.GetComercialRemuneracao();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AlterarRemuneracao(int id)
        {

            Remuneracao item = repository.GetRemuneracao(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.propostaid = item.propostaid;
            ViewBag.comercialid = item.comercialid;
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarRemuneracao([Bind(Include = "id,dtlan,dtcad,propostaid,comercialid,insumos,percentual")] Remuneracao item, FormCollection fc)
        {

            if (ModelState.IsValid)
            {
                Boolean result = repository.AlterarRemuneracao(item);
                return RedirectToAction("ListaRemuneracao");
            }
            return View(item);
        }

        public ActionResult ExcluirComercial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Comercial comercial = db.Comercials.Find(id);
            //if (comercial == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(comercial);
            return View();
        }

        public void ExcluirRemuneracao(int id)
        {
            var result = repository.ExcluirRemuneracao(id);
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
