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
    public class TiposContratosController : Controller
    {
        private readonly IRepository repository;
        public TiposContratosController(IRepository objIrepository) { repository = objIrepository; }

        // GET: Categorias
        public ActionResult ListaTiposContrato()
        {
            
            var result = repository.TiposContratoAll();
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        public ActionResult NovoTipoContrato() {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoTipoContrato(TipoContrato item)
        {
           
            if (repository.TipoContratoJaExiste(item.nome))
            {
                ViewBag.jaexiste = item.nome + " já esta cadastrado!";
                return View(item);
            }

            var result = repository.NovoTipoContrato(item);
            return RedirectToAction("ListaTiposContrato", "TiposContratos");
        }

        public ActionResult AlterarTipoContrato(int id)
        {

            TipoContrato item = repository.GetTipoContrato(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarTipoContrato([Bind(Include = "id,nome")] TipoContrato item,FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {
                Boolean result = repository.AlterarTipoContrato(item);
                return RedirectToAction("ListaTiposContrato");
            }
            return View(item);
        }
        public ActionResult SaveNewTipoContrato(string nome)
        {
            TipoContrato item = new TipoContrato { nome = nome };

            int id = repository.NovoTipoContrato(item);
            if (id > 0)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error em SaveNewIndice", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ExcluirTipoContrato(int id)
        {
           
            var result = repository.ExcluirTipoContrato(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulateTiposContrato()
        {
            var result = repository.TiposContratoAll();
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
