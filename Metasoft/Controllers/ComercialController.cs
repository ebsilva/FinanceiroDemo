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
    public class ComercialController : Controller
    {
        private readonly IRepository repository;

        public ComercialController(IRepository objIrepository) { repository = objIrepository; }

        // GET: Comercial
        public ActionResult ListaComercial()
        {
            return View(repository.ComercialAll(DateTime.Now.Month, DateTime.Now.Year));
        }

        [HttpPost]
        public ActionResult ListaComercial(FormCollection fc)
        {
            ViewBag.messelected = String.IsNullOrEmpty(fc["meses"].ToString()) ? DateTime.Now.Month.ToString() : fc["meses"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? DateTime.Now.Year.ToString() : fc["anos"].ToString();
            string download = String.IsNullOrEmpty(fc["download"].ToString()) ? "" : fc["download"].ToString();

            if (download == "true")
            {
                DownloadExcel(int.Parse(ViewBag.messelected), int.Parse(ViewBag.anoselected));
            }

            return View(repository.ComercialAll(int.Parse(ViewBag.messelected), int.Parse(ViewBag.anoselected)));
        }

        public void DownloadExcel(int mes, int ano)
        {
            var result = repository.ComercialAll(mes,ano);
            var grid = new System.Web.UI.WebControls.GridView();
            var lista = result;
            grid.DataSource = lista;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Comercial_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }


        // GET: Comercial/Create
        public ActionResult NovoComercial()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoComercial([Bind(Include = "id,nome,email")] Comercial comercial)
        {
            if (ModelState.IsValid)
            {
                /* Verifica se já existe */
                if(!repository.ComercialExists(comercial))
                {
                    int result = repository.NovoComercial(comercial);
                }
                else
                {
                    ViewBag.jaexiste = "Nome ou email já Cadastrado!";
                    return View(comercial);
                }
               
                return RedirectToAction("ListaComercial");
            }

            return View(comercial);
        }

        // GET: Comercial/Edit/5
        public ActionResult AlterarComercial(int id)
        {
            Comercial item = repository.GetComercial(id);
            if (item == null) {return HttpNotFound();}
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarComercial([Bind(Include = "id,nome,email")] Comercial comercial)
        {
            if (ModelState.IsValid)
            {
                var result = repository.AlterarComercial(comercial);
                if (result) { 
                    return RedirectToAction("ListaComercial");
                }
            }
            return View(comercial);
        }


        public ActionResult ExcluirComercial(int id)
        {
            var result = repository.ExcluirComercial(id);
            return RedirectToAction("ListaComercial");
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
