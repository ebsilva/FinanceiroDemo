using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Metasoft.Models;
using Metasoft.Infrastructure;

namespace Metasoft.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class IndicesController : Controller
    {
        private readonly IRepository repository;
        public IndicesController(IRepository objIrepository) { repository = objIrepository; }

        // GET: Categorias
        public ActionResult ListaIndices()
        {
            
            var result = repository.IndicesAll();
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        public ActionResult NovoIndice() {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoIndice(Indice item)
        {
           
            if (repository.IndiceJaExiste(item.nome))
            {
                ViewBag.jaexiste = item.nome + " já esta cadastrada!";
                return View(item);
            }

            var result = repository.NovoIndice(item);
            return RedirectToAction( "ListaIndices", "Indices");
        }

        public ActionResult AlterarIndice(int id)
        {
            
            Indice item = repository.GetIndice(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarIndice([Bind(Include = "id,nome")] Indice item,FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {
                Boolean result = repository.AlterarIndice(item);
                return RedirectToAction("ListaIndices");
            }
            return View(item);
        }
        public ActionResult SaveNewIndice(string nome)
        {
            Indice item = new Indice { nome = nome };

            int id = repository.NovoIndice(item);
            if (id > 0)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error em SaveNewIndice", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ExcluirIndice(int id)
        {
           
            var result = repository.ExcluirIndice(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PopulateIndices()
        {
            var result = repository.IndicesAll();
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
