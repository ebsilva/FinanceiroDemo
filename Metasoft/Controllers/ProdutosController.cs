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
    public class ProdutosController : Controller
    {
        private readonly IRepository repository;

        public ProdutosController(IRepository objIrepository) { repository = objIrepository; }
        // GET: Produtos
        public ActionResult ListaProdutos()
        {
            
            var result = repository.ProdutosAll();
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        public ActionResult PopulateProdutos()
        {
           var result = repository.ProdutosAll();
           return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NovoProduto() {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoProduto(Produto produto)
        {
           
            if (repository.ProdutoJaExiste(produto.nome,0))
            {
                ViewBag.jaexiste = produto.nome + " já esta cadastrado!";
                return View(produto);
            }

            var result = repository.NovoProduto(produto);
            return RedirectToAction("ListaProdutos", "Produtos");
        }

        public ActionResult AlterarProduto(int id)
        {
            
            Produto item = repository.GetProduto(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarProduto([Bind(Include = "produtoid,nome,tipo,valor")] Produto produto)
        {
           
            if (ModelState.IsValid)
            {
                if (repository.ProdutoJaExiste(produto.nome, produto.produtoid))
                {
                    ViewBag.jaexiste = produto.nome + " já esta cadastrada!";
                    return View();
                }
                Boolean result = repository.AlterarProduto(produto);
                return RedirectToAction("ListaProdutos");
            }
            return View(produto);
        }


        public ActionResult ExcluirProduto(int id)
        {
           
            var result = repository.ExcluirProduto(id);
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
