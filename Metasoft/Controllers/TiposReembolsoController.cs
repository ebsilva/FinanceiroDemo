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

namespace Metasoft.Controllers
{
    public class TiposReembolsoController : Controller
    {
        private readonly IRepository repository;
        public TiposReembolsoController(IRepository objIrepository) { repository = objIrepository; }

        // GET: Reembolsoes1
        public ActionResult ListaTiposReembolso()
        {
            
            return View(repository.TiposReembolsosAll());
        }

  
        // GET: Reembolsoes1/Create
        public ActionResult NovoTipoReembolso()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoTipoReembolso([Bind(Include = "tiporeembolsoid,nome")] TiposReembolso item)
        {
           
            if (ModelState.IsValid)
            {
                if (repository.TipoReembolsoJaExiste(item.nome))
                {
                    ViewBag.jaexiste = item.nome + " já esta cadastrada!";
                    return View(item);
                }

                var result = repository.NovoTipoReembolso(item);
                return RedirectToAction("ListaTiposReembolso");
            }

            return View(item);
        }

        // GET: Reembolsoes1/Edit/5
        public ActionResult AlterarTipoReembolso(int id)
        {
           
            TiposReembolso item = repository.GetTipoReembolso(id);
            if (ModelState.IsValid)
            {
                
                if (item == null)
                {
                    return HttpNotFound();
                }
                return View(item);
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarTipoReembolso([Bind(Include = "tiporeembolsoid,nome")] TiposReembolso item)
        {
           
            if (ModelState.IsValid)
            {
                Boolean result = repository.AlterarTipoReembolso(item);
                return RedirectToAction("ListaTiposReembolso");
            }
            return View(item);
        }


        public ActionResult ExcluirTipoReembolso(int id)
        {
            
            var result = repository.ExcluirTipoReembolso(id);
            return Json(result, JsonRequestBehavior.AllowGet);
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
