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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Metasoft.Controllers
{
    public class PerfisController : Controller
    {
        private readonly IRepository repository;
        public PerfisController(IRepository objIrepository) { repository = objIrepository; }

        // GET: Perfils
        public ActionResult ListaPerfis()
        {
            return View(repository.PerfisAll());
        }

        public ActionResult NovoPerfil()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NovoPerfil([Bind(Include = "roleid,name,propostas,receber,pagar,relatorios,reembolso,projetos,contratos,remuneracoes")] CreatePerfilViewModel perfil, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (repository.PerfilJaExiste(perfil.name))
                {
                    ViewBag.jaexiste = perfil.name + " já esta cadastrado!";
                    return View(perfil);
                }
                /* Recupera valores dos radio buttos*/

                int propostas = int.Parse(fc["propostas"].ToString());
                int receber = int.Parse(fc["receber"].ToString());
                int pagar = int.Parse(fc["pagar"].ToString());
                int cadastros = int.Parse(fc["cadastros"].ToString());
                int relatorios = int.Parse(fc["relatorios"].ToString());
                int reembolso = int.Parse(fc["reembolso"].ToString());
                
                int projetos = int.Parse(fc["projetos"].ToString()); /* ALTERADO EM 31/03/2017 */
                int contratos = int.Parse(fc["contratos"].ToString()); /* ALTERADO EM 31/03/2017 */
                int remuneracoes = int.Parse(fc["remuneracoes"].ToString()); /* ALTERADO EM 31/03/2017 */

                Permissao permissoes = new Permissao();
                switch (propostas)
                {
                    case 3:
                        permissoes.pro_edit = false; permissoes.pro_view = false; break;
                    case 2:
                        permissoes.pro_edit = true; permissoes.pro_view = false; break;
                    case 1:
                        permissoes.pro_edit = false; permissoes.pro_view = true; break;
                    default:
                        permissoes.pro_edit = false; permissoes.pro_view = false; break;
                }

                switch (receber)
                {
                    case 3:
                        permissoes.rec_edit = false; permissoes.rec_view = false; break;
                    case 2:
                        permissoes.rec_edit = true; permissoes.rec_view = false; break;
                    case 1:
                        permissoes.rec_edit = false; permissoes.rec_view = true; break;
                    default:
                        permissoes.rec_edit = false; permissoes.rec_view = false; break;
                }
                switch (pagar)
                {
                    case 3:
                        permissoes.pag_edit = false; permissoes.pag_view = false; break;
                    case 2:
                        permissoes.pag_edit = true; permissoes.pag_view = false; break;
                    case 1:
                        permissoes.pag_edit = false; permissoes.pag_view = true; break;
                    default:
                        permissoes.pag_edit = false; permissoes.pag_view = false; break;
                }
                switch (cadastros)
                {
                    case 3:
                        permissoes.cad_edit = false; permissoes.cad_view = false; break;
                    case 2:
                        permissoes.cad_edit = true; permissoes.cad_view = false; break;
                    case 1:
                        permissoes.cad_edit = false; permissoes.cad_view = true; break;
                    default:
                        permissoes.cad_edit = false; permissoes.cad_view = false; break;
                }

                switch (relatorios)
                {
                    case 3:
                        permissoes.rep_edit = false; permissoes.rep_view = false; break;
                    case 2:
                        permissoes.rep_edit = true; permissoes.rep_view = false; break;
                    case 1:
                        permissoes.rep_edit = false; permissoes.rep_view = true; break;
                    default:
                        permissoes.rep_edit = false; permissoes.rep_view = false; break;
                }

                switch (reembolso)
                {
                    case 3:
                        permissoes.ree_edit = false; permissoes.ree_view = false; break;
                    case 2:
                        permissoes.ree_edit = true; permissoes.ree_view = false; break;
                    case 1:
                        permissoes.ree_edit = false; permissoes.ree_view = true; break;
                    default:
                        permissoes.ree_edit = false; permissoes.ree_view = false; break;
                }

                /* ALTERADO EM 31/03/2017 */
                switch (projetos)
                {
                    case 3:
                        permissoes.prj_edit = false; permissoes.prj_view = false; break;
                    case 2:
                        permissoes.prj_edit = true; permissoes.prj_view = false; break;
                    case 1:
                        permissoes.prj_edit = false; permissoes.prj_view = true; break;
                    default:
                        permissoes.prj_edit = false; permissoes.prj_view = false; break;
                }
                /* ALTERADO EM 30/05/2017 */
                switch (contratos)
                {
                    case 3:
                        permissoes.ctt_edit = false; permissoes.ctt_view = false; break;
                    case 2:
                        permissoes.ctt_edit = true; permissoes.ctt_view = false; break;
                    case 1:
                        permissoes.ctt_edit = false; permissoes.ctt_view = true; break;
                    default:
                        permissoes.ctt_edit = false; permissoes.ctt_view = false; break;
                }
                switch (remuneracoes)
                {
                    case 3:
                        permissoes.rem_edit = false; permissoes.rem_view = false; break;
                    case 2:
                        permissoes.rem_edit = true; permissoes.rem_view = false; break;
                    case 1:
                        permissoes.rem_edit = false; permissoes.rem_view = true; break;
                    default:
                        permissoes.rem_edit = false; permissoes.rem_view = false; break;
                }

                ApplicationDbContext context = new Models.ApplicationDbContext();
                var RoleManager = new RoleManager<IdentityRole>(new Microsoft.AspNet.Identity.EntityFramework.RoleStore<IdentityRole>(context));

         
                if (!RoleManager.RoleExists(perfil.name)) {
                    var roleresult = RoleManager.Create(new IdentityRole(perfil.name));
                    if (roleresult.Succeeded)
                    {
                        var role = RoleManager.FindByName(perfil.name);
                        if (role != null)
                        {
                            /* Cria registro na tabela permissoes*/
                            try
                            {
                                permissoes.roleid = role.Id;
                                context.Permissoes.Add(permissoes);
                                context.SaveChanges();
                                return RedirectToAction("ListaPerfis");
                            }
                            catch (Exception exc)
                            {
                                // do nothing
                                ViewBag.failmsg = "Erro ao cadastrar perfil";
                                var deleteresult = RoleManager.Delete(role);
                                context.SaveChanges();
                                var msg = exc.Message;

                            }
                        }
                    }

                }
                else
                {
                    ViewBag.jaexiste = "Perfil " + perfil.name + " já exite.";
                }
            }
             return View(perfil);
        }

        public ActionResult AlterarPerfil(string id)
        {

            PerfilPermissaoViewModel perfil = repository.GetPerfilPermissoes(id);

            ViewBag.propostas = 3; ViewBag.receber = 3; ViewBag.pagar = 3; ViewBag.cadastros = 3; ViewBag.relatorios = 3; ViewBag.reembolsos = 3; 
            /* ALTERADO EM 31/03/2017 */
            ViewBag.projetos = 3;
            /* ALTERADO EM 30/05/2017 */
            ViewBag.contratos = 3; ViewBag.remuneracoes = 3;

            if (perfil.pro_edit){ViewBag.propostas = 2;}
            if (!perfil.pro_edit && perfil.pro_view) {ViewBag.propostas = 1;}

            if(perfil.rec_edit){ViewBag.receber = 2;}
            if (!perfil.rec_edit && perfil.rec_view) {ViewBag.receber = 1;}

            if(perfil.pag_edit){ViewBag.pagar = 2;}
            if (!perfil.pag_edit && perfil.pag_view) {ViewBag.pagar = 1;}

            if(perfil.cad_edit){ViewBag.cadastros = 2;}
            if (!perfil.cad_edit && perfil.cad_view) {ViewBag.cadastros = 1;}

            if(perfil.rep_edit) {ViewBag.relatorios = 2;}
            if (!perfil.rep_edit && perfil.rep_view) {ViewBag.relatorios = 1;}

            if(perfil.ree_edit) {ViewBag.reembolsos = 2;}
            if (!perfil.ree_edit && perfil.ree_view) {ViewBag.reembolsos = 1;}

            /* ALTERADO EM 31/03/2017 */
            if (perfil.prj_edit) { ViewBag.projetos = 2; }
            if (!perfil.prj_edit && perfil.prj_view) { ViewBag.projetos = 1; }

            /* ALTERADO EM 30/05/2017 */
            if (perfil.ctt_edit) { ViewBag.contratos = 2; }
            if (!perfil.ctt_edit && perfil.ctt_view) { ViewBag.contratos = 1; }

            if (perfil.rem_edit) { ViewBag.remuneracoes = 2; }
            if (!perfil.rem_edit && perfil.rem_view) { ViewBag.remuneracoes = 1; }


            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }
        [HttpPost]
        public ActionResult AlterarPerfil(PerfilPermissaoViewModel perfilpermissao, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                if (repository.PerfilJaExiste(perfilpermissao.name, perfilpermissao.roleid))
                {
                    ViewBag.jaexiste = perfilpermissao.name + " já esta cadastrada!";
                    return View();
                }

                /* Recupera valores dos radio buttons*/

                int propostas = int.Parse(fc["propostas"].ToString());
                int receber = int.Parse(fc["receber"].ToString());
                int pagar = int.Parse(fc["pagar"].ToString());
                int cadastros = int.Parse(fc["cadastros"].ToString());
                int relatorios = int.Parse(fc["relatorios"].ToString());
                int reembolso = int.Parse(fc["reembolso"].ToString());
                /* ALTERADO EM 31/03/2017 */
                int projetos = int.Parse(fc["projetos"].ToString());
                /* ALTERADO EM 30/05/2017 */
                int contratos = int.Parse(fc["contratos"].ToString());
                int remuneracoes = int.Parse(fc["remuneracoes"].ToString());

                Permissao permissoes = new Permissao();
                permissoes.roleid = perfilpermissao.roleid;
                switch (propostas)
                {
                    case 3:
                        permissoes.pro_edit = false; permissoes.pro_view = false; break;
                    case 2:
                        permissoes.pro_edit = true; permissoes.pro_view = false; break;
                    case 1:
                        permissoes.pro_edit = false; permissoes.pro_view = true; break;
                    default:
                        permissoes.pro_edit = false; permissoes.pro_view = false; break;
                }

                switch (receber)
                {
                    case 3:
                        permissoes.rec_edit = false; permissoes.rec_view = false; break;
                    case 2:
                        permissoes.rec_edit = true; permissoes.rec_view = false; break;
                    case 1:
                        permissoes.rec_edit = false; permissoes.rec_view = true; break;
                    default:
                        permissoes.rec_edit = false; permissoes.rec_view = false; break;
                }
                switch (pagar)
                {
                    case 3:
                        permissoes.pag_edit = false; permissoes.pag_view = false; break;
                    case 2:
                        permissoes.pag_edit = true; permissoes.pag_view = false; break;
                    case 1:
                        permissoes.pag_edit = false; permissoes.pag_view = true; break;
                    default:
                        permissoes.pag_edit = false; permissoes.pag_view = false; break;
                }
                switch (cadastros)
                {
                    case 3:
                        permissoes.cad_edit = false; permissoes.cad_view = false; break;
                    case 2:
                        permissoes.cad_edit = true; permissoes.cad_view = false; break;
                    case 1:
                        permissoes.cad_edit = false; permissoes.cad_view = true; break;
                    default:
                        permissoes.cad_edit = false; permissoes.cad_view = false; break;
                }

                switch (relatorios)
                {
                    case 3:
                        permissoes.rep_edit = false; permissoes.rep_view = false; break;
                    case 2:
                        permissoes.rep_edit = true; permissoes.rep_view = false; break;
                    case 1:
                        permissoes.rep_edit = false; permissoes.rep_view = true; break;
                    default:
                        permissoes.rep_edit = false; permissoes.rep_view = false; break;
                }

                switch (reembolso)
                {
                    case 3:
                        permissoes.ree_edit = false; permissoes.ree_view = false; break;
                    case 2:
                        permissoes.ree_edit = true; permissoes.ree_view = false; break;
                    case 1:
                        permissoes.ree_edit = false; permissoes.ree_view = true; break;
                    default:
                        permissoes.ree_edit = false; permissoes.ree_view = false; break;
                }
                /* ALTERADO EM 31/03/2017 */
                switch (projetos)
                {
                    case 3:
                        permissoes.prj_edit = false; permissoes.prj_view = false; break;
                    case 2:
                        permissoes.prj_edit = true; permissoes.prj_view = false; break;
                    case 1:
                        permissoes.prj_edit = false; permissoes.prj_view = true; break;
                    default:
                        permissoes.prj_edit = false; permissoes.prj_view = false; break;
                }

                /* ALTERADO EM 30/05/2017 */
                switch (contratos)
                {
                    case 3:
                        permissoes.ctt_edit = false; permissoes.ctt_view = false; break;
                    case 2:
                        permissoes.ctt_edit = true; permissoes.ctt_view = false; break;
                    case 1:
                        permissoes.ctt_edit = false; permissoes.ctt_view = true; break;
                    default:
                        permissoes.ctt_edit = false; permissoes.ctt_view = false; break;
                }
                switch (remuneracoes)
                {
                    case 3:
                        permissoes.rem_edit = false; permissoes.rem_view = false; break;
                    case 2:
                        permissoes.rem_edit = true; permissoes.rem_view = false; break;
                    case 1:
                        permissoes.rem_edit = false; permissoes.rem_view = true; break;
                    default:
                        permissoes.rem_edit = false; permissoes.rem_view = false; break;
                }


                if (repository.UpdateTable("Update AspNetRoles set name ='" + perfilpermissao.name + "' where id='" + perfilpermissao.roleid + "'")){

                    /* Atualizar permissoes */
                    Boolean result = repository.AlterarPermissoes(permissoes);

                    return RedirectToAction("ListaPerfis");

                }
              
                
            }
            return View(perfilpermissao);
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
