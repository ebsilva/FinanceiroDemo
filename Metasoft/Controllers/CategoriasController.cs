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
    public class  CategoriasController : Controller
    {
        private readonly IRepository repository;
        public CategoriasController(IRepository objIrepository) { repository = objIrepository; }

        // GET: Categorias
        public ActionResult ListaCategorias()
        {
            
            var result = repository.CategoriasAll();
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        public ActionResult NovaCategoria() {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaCategoria(Categoria categoria)
        {
           
            if (repository.CategoriaJaExiste(categoria.nome))
            {
                ViewBag.jaexiste = categoria.nome + " já esta cadastrada!";
                return View(categoria);
            }

            var result = repository.NovaCategoria(categoria);
            return RedirectToAction( "ListaCategorias", "Categorias");
        }

        public ActionResult AlterarCategoria(int id)
        {
            
            Categoria categoria = repository.GetCategoria(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarCategoria([Bind(Include = "categoriaid,nome,tipo")] Categoria categoria,FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {
                string categoriatipo = fc["tipo"].ToString();
                categoria.tipo = categoriatipo;
                Boolean result = repository.AlterarCategoria(categoria);
                return RedirectToAction("ListaCategorias");
            }
            return View(categoria);
        }
        public ActionResult SaveNewCategoria(string nome, string tipo)
        {
            Categoria categoria = new Categoria();
            categoria.nome = nome;
            categoria.tipo = tipo;



            int contatoid = repository.NovaCategoria(categoria);
            if (contatoid > 0)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error em SaveNewContato", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ExcluirCategoria(int id)
        {
           
            var result = repository.ExcluirCategoria(id);
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
