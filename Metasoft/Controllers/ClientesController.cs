using System;
using System.Net;
using System.Web.Mvc;
using Metasoft.Models;
using Metasoft.Infrastructure;

namespace Metasoft.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ClientesController : Controller
    {
        private readonly IRepository repository;
        public ClientesController(IRepository objIrepository) { repository = objIrepository; }
        // GET: Clientes
        public ActionResult ListaClientes()
        {
            
            ViewBag.situacaoselected = "";
            var result = repository.ClientesAll("C","");
            if(result !=null)
            {
                return View(result);
            }
            return View();
        }
        [HttpPost]
        public ActionResult ListaClientes(FormCollection fc)
        {
            try { ViewBag.situacaoselected = fc["situacao"].ToString(); } catch { ViewBag.situacaoselected = "T"; }
            var result = repository.ClientesAll("C",ViewBag.situacaoselected);
            return View(result);
        }
        public ActionResult ListaFornecedores()
        {
           
            ViewBag.situacaoselected = "";
            var result = repository.ClientesAll("F","");
            if (result != null)
            {
                return View(result);
            }
            return View();
        }
        [HttpPost]
        public ActionResult ListaFornecedores(FormCollection fc)
        {
            ViewBag.situacaoselected = String.IsNullOrEmpty(fc["situacao"].ToString()) ? "T" : fc["situacao"].ToString();
            var result = repository.ClientesAll("F", ViewBag.situacaoselected);
            return View(result);
        }
        public ActionResult NovoCliente()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoCliente([Bind(Include = "clienteid,nome,cnpj,ie,endereco,complemento,bairro,cep,cidade,estado,fone,f0800,email,site,tipo")] Cliente cliente, FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {

                ViewBag.nomeexite = "";
                if (repository.FindClienteByName(cliente.nome))
                {
                    ViewBag.jaexiste = cliente.nome + " já esta cadastrado!";
                    return View();
                }

                if (repository.FindClienteByCnpj(cliente.cnpj))
                {
                    ViewBag.jaexiste = "CNPJ: "+ cliente.cnpj + " já esta cadastrado!";
                    return View();
                }
                int clienteid = repository.NovoCliente(cliente, "C");
                if (clienteid > 0) { return RedirectToAction("ListaClientes"); }

            }
            return View();
        }

        public ActionResult AlterarCliente(int? id)
        {
           
            if (id == null) {return new HttpStatusCodeResult(HttpStatusCode.BadRequest);}
            Cliente cliente = repository.GetCliFor(id);
            if (cliente == null) { return HttpNotFound(); }
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult AlterarCliente([Bind(Include = "clienteid,nome,cnpj,ie,endereco,complemento,bairro,cep,cidade,estado,fone,email,site,tipo")] Cliente cliente, FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {
                Boolean result = repository.AlterarCliente(cliente);
                if (result)
                {
                    return RedirectToAction("ListaClientes");
                }
                else
                {
                    return View(cliente);
                }
            }
            return View(cliente);
        }
        public  ActionResult ExcluirCliente(int id)
        {
            
            Boolean result = repository.ExcluirCliente(id);
            if (!result)
            {
                return HttpNotFound();
            }
            return RedirectToAction("ListaClientes", "Clientes");
        }
        public ActionResult ExcluirContato(int id)
        {
            Boolean result = repository.ContatoDelete(id);
            if (!result)
            {
                return HttpNotFound();
            }
            return RedirectToAction("ListaClientes", "Clientes");
        }
        /* Fornecedores*/
        public ActionResult NovoFornecedor()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoFornecedor([Bind(Include = "clienteid,nome,cnpj,ie,endereco,complemento,bairro,cidade,estado,cep,fone,f0800,email,site,tipo")] Cliente cliente, FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {

                ViewBag.nomeexite = "";
                Boolean jaexiste = false;
                if (repository.FindClienteByName(cliente.nome))
                {
                    ViewBag.jaexiste = cliente.nome + " já esta cadastrado!";
                    jaexiste = true;
                }

                if (repository.FindClienteByCnpj(cliente.cnpj))
                {
                    ViewBag.jaexiste = "CNPJ: " + cliente.cnpj + " já esta cadastrado!";
                    jaexiste = true;
                }
                if (jaexiste){return View(cliente);}

                int clienteid = repository.NovoCliente(cliente,"F");
                if (clienteid > 0) { return RedirectToAction("ListaFornecedores");}

            }

            return View();
        }
        public ActionResult AlterarFornecedor(int? id)
        {
           
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Cliente cliente = repository.GetCliFor(id);
            if (cliente == null) { return HttpNotFound(); }
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarFornecedor([Bind(Include = "clienteid,nome,cnpj,ie,endereco,complemento,bairro,cidade,estado,cep,fone,email,site,tipo")] Cliente cliente, FormCollection fc)
        {
           
            if (ModelState.IsValid)
            {
                Boolean result = repository.AlterarCliente(cliente);
                if (result)
                {
                    return RedirectToAction("ListaFornecedores");
                }
                else
                {
                    return View(cliente);
                }
            }
            return View(cliente);
        }
        public ActionResult ExcluirFornecedor(int id)
        {
            
            Boolean result = repository.ExcluirCliente(id);
            if (!result)
            {
                return HttpNotFound();
            }
            return RedirectToAction("ListaFornecedores", "Clientes");
        }
        public ActionResult SaveNewContato(int clientid, string nome, string email, string fone, string celular ,string sexo)
        {
            Contato contato = new Contato();

            contato.clienteid = clientid;
            contato.nome = nome;
            contato.email = email;
            contato.fone = fone;
            contato.celular = celular;
            contato.sexo = sexo;

            int contatoid = repository.NovoContato(contato);
            if (contatoid > 0)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error em SaveNewContato", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ShowContatos(int clienteid)
        {
            var result = repository.GetContatos(clienteid);
            return PartialView("_Contatos", result);
        }
        public ActionResult ShowContas(int clienteid, string tipo,string atrasadas)
        {
            var result = repository.GetContas(clienteid, tipo,atrasadas);
            return PartialView("_Lancamentos", result);
        }
        /* Others*/
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
