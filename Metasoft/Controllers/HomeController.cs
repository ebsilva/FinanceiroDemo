using Metasoft.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Metasoft.Infrastructure;

namespace Metasoft.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IRepository repository;
        public HomeController(IRepository objIrepository) { repository = objIrepository; }

        public ActionResult Index()
        {

            //CriaUsuario("administrador@metasoft.com", "123456", "Administrador", "administrador@metasoft.com");
            //CriaPerfis();
            return View();
        }
        public ActionResult AcessoNaoPermitido()
        {
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Ocorreu erro na aplicação";

            return View();
        }

        public ActionResult ResumoDiario()
        {

            string currentmonth = DateTime.Now.Month.ToString();
            string currentyear = DateTime.Now.Year.ToString();
            ViewBag.messelected = currentmonth;
            ViewBag.anoselected = currentyear;
            var result = repository.ResumoDiario();
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public ActionResult ResumoDiario(FormCollection fc)
        {

            string currentmonth = DateTime.Now.Month.ToString();
            string currentyear = DateTime.Now.Year.ToString();

            ViewBag.messelected = String.IsNullOrEmpty(fc["meses"].ToString()) ? currentmonth : fc["meses"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? currentyear : fc["anos"].ToString();

            var result = repository.ResumoDiario();
            if (result != null)
            {
                return View(result);
            }
            return View();
        }


        public ActionResult ResumoPorCategoria(string tipo, string mes, string ano)
        {
          var result = repository.ResumoPorCategoria(tipo,mes,ano);
          return Json(result, JsonRequestBehavior.AllowGet);
  
        }

        public ActionResult DrawGraph(ResumoDiarioViewModel resumo)
        {
            return View(resumo);
        }
    

        //private void CriaPerfis()
        //{
        //    ApplicationDbContext context = new Models.ApplicationDbContext();
        //    var RoleManager = new RoleManager<IdentityRole>(new Microsoft.AspNet.Identity.EntityFramework.RoleStore<IdentityRole>(context));

        //    if (!RoleManager.RoleExists("Reembolso")) { var roleresult = RoleManager.Create(new IdentityRole("Reembolso")); }
        //    if (!RoleManager.RoleExists("A Pagar")) { var roleresult = RoleManager.Create(new IdentityRole("A Pagar")); }
        //    if (!RoleManager.RoleExists("A Receber")) { var roleresult = RoleManager.Create(new IdentityRole("A Receber")); }
        //    if (!RoleManager.RoleExists("A Pagar/Receber")) { var roleresult = RoleManager.Create(new IdentityRole("A Pagar/Receber")); }
        //    if (!RoleManager.RoleExists("Propostas")) { var roleresult = RoleManager.Create(new IdentityRole("Propostas")); }
        //    if (!RoleManager.RoleExists("Propostas/A Receber")) { var roleresult = RoleManager.Create(new IdentityRole("Propostas/A Receber")); }
        //}
        //private void CriaUsuario(string nome, string senha, string perfil, string email)
        //{

        //    ApplicationDbContext context = new Models.ApplicationDbContext();
        //    var UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(context));
        //    var RoleManager = new RoleManager<IdentityRole>(new Microsoft.AspNet.Identity.EntityFramework.RoleStore<IdentityRole>(context));
        //    //private ApplicationDbContext db = new ApplicationDbContext();

        //    string usuario = nome;
        //    string password = senha;

        //    //Cria perfil adminsitrador se nao existir
        //    if (perfil == "Administrador")
        //    {
        //        if (!RoleManager.RoleExists("Administrador")) { var roleresult = RoleManager.Create(new IdentityRole(perfil)); }

        //        //Create User=Admin with password=123456
        //        var user = new ApplicationUser { UserName = email, Email = email };
        //        user.lastpwdupdate = DateTime.Now;
        //        var adminresult = UserManager.Create(user, senha);

        //        //Add User Admin to Role Admin
        //        if (adminresult.Succeeded)
        //        {
        //            var result = UserManager.AddToRole(user.Id, perfil);
        //        }

        //    }



            //    if (perfil == "UsuarioComun")
            //    {
            //        if (!RoleManager.RoleExists(perfil))
            //        {
            //            var roleresult = RoleManager.Create(new IdentityRole(perfil));
            //        }

            //        var user = new ApplicationUser { UserName = email, Email = email };
            //        var aprovador = UserManager.Create(user, senha);


            //        //var funcionarios = new List<Funcionario> { };

            //        //funcionarios.Add(new Funcionario { FuncionarioId = user.Id });

            //        //Add User ao perfil UsuarioComun
            //        if (aprovador.Succeeded)
            //        {
            //            var result = UserManager.AddToRole(user.Id, perfil);
            //        }
            //    }

            //}

           // }
        }
}