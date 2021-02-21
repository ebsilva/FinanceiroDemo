using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Net;
using System.Text;
using Metasoft.Models;
using Metasoft;
using Metasoft.Infrastructure;
using System.Security.Claims;

namespace MetaSoft.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {

        private readonly IRepository repository;
        public UsuariosController(IRepository objIrepository) { repository = objIrepository; }

        private const int PASSWORD_HISTORY_LIMIT = 10;
        public static ApplicationUserManager usermanager;

        public ActionResult ListaUsuarios()
        {
            var result = repository.UsuariosAll();
            return View(result);
        }

        public ActionResult NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoUsuario(CreateUserModel model, FormCollection fc)
        {

            int errors = 0;

            string perfil = fc["perfis"].ToString();
            try
            {
                /* Check valid email */
                if (!EmailIsValid(model.Email)) { ModelState.AddModelError("", "Formato de email inválido"); errors += 1; }
                /* Check Alpha */
                if (string.IsNullOrEmpty(fc["perfis"])) { ModelState.AddModelError("", "Perfil não pode estar em branco"); errors += 1; }
                if (errors == 0)
                {

                    ApplicationDbContext context = new ApplicationDbContext();
                    var UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(context));
                    var RoleManager = new RoleManager<IdentityRole>(new Microsoft.AspNet.Identity.EntityFramework.RoleStore<IdentityRole>(context));

                    string pwd = ConfigurationManager.AppSettings["defaultpwd"].ToString();
                    //var user = new ApplicationUser { UserName = email, Email = email };
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, nome = model.Name, lastpwdupdate = Convert.ToDateTime("1900-01-01 00:00:00.000") };
                    try {
                        var adduserresult = UserManager.Create(user, pwd);

                        //Add User Admin to Role 
                        if (adduserresult.Succeeded)
                        {
                            var result = UserManager.AddToRole(user.Id, perfil);
                            return RedirectToAction("ListaUsuarios");
                        }


                    }
                    catch (Exception exc)
                    {
                        var msg = exc.Message;
                    }
                  
                    return View();



                    //var RoleManager = new RoleManager<IdentityRole>(new Microsoft.AspNet.Identity.EntityFramework.RoleStore<IdentityRole>(repository.Dbcontext()));
                    //var user = new ApplicationUser { UserName = model.Email, Email = model.Email, nome = model.Name,lastpwdupdate = Convert.ToDateTime("1900-01-01 00:00:00.000") };
                    //string pwd = ConfigurationManager.AppSettings["defaultpwd"].ToString();
                    //var useresult = UserManager.Create(user, pwd);

                    //var result = UserManager.AddToRole(user.Id, perfil);

                    //perfil = fc["perfis"].ToString();
                    //var addresult = await UserManager.AddToRoleAsync(user.Id, perfil);
                    //if (addresult.Succeeded)
                    //{
                    //    var roleresult = UserManager.AddToRole(user.Id, perfil);
                    //}
                    //

                }
            }
            catch (Exception createexception)
            {
                ModelState.AddModelError("", createexception.Message);
            }
            //PopulatePerfis(perfil);
            return View(model);

        }

        public ActionResult AlterarPerfilUsuario(string id)
        {
            AlterarPerfilViewModel userperil = repository.GetUserRoleInfo(id);

            return View(userperil);

            ///var roles = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            //foreach(var role in roles) {
            //    System.Web.Security.Roles.RemoveUserFromRole(id, role);
            //}
            //System.Web.Security.Roles.AddUserToRole(id, newperfil);
            //return RedirectToAction("ListaUsuarios");

        }

        [HttpPost]
        public ActionResult AlterarPerfilUsuario([Bind(Include = "id,nome,roleid,rolename")] AlterarPerfilViewModel alterarperfil,FormCollection fc)
        {
          
                string rolename = fc["perfis"].ToString();
                string newroleid = repository.GetRoleIdByName(rolename);
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(context));
              
                try{

                    var resultremove = UserManager.RemoveFromRole(alterarperfil.id, alterarperfil.rolename);
                    if (resultremove.Succeeded) {
                        var resultadd = UserManager.AddToRole(alterarperfil.id, rolename);
                        if (resultadd.Succeeded)
                        {
                            return RedirectToAction("ListaUsuarios");
                        }
                     }
                    else 
                    {
                         return View(alterarperfil);
                    }
                }  catch (Exception exc) {
                var msg = exc.Message;
                // do nothing
            }

               return View(alterarperfil);
        }

        public ActionResult ResetPwd(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            string pwd = ConfigurationManager.AppSettings["defaultpwd"].ToString();
            user.lastpwdupdate = DateTime.Now;
            var token =  UserManager.GeneratePasswordResetToken(user.Id);
            var result =  UserManager.ResetPassword(user.Id, token, pwd);
            return RedirectToAction("ListaUsuarios");
        }

        public ActionResult ExcluirUsuario(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var _userManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(context));
            ApplicationUser user = UserManager.FindById(id);
           string errormsg = "Ocorreu um erro ao tentar excluir usuário " + user.nome;
            if (user != null)
            {
                IdentityResult result = UserManager.Delete(user);
                if (!result.Succeeded) { ViewBag.deletefailed = errormsg; }
            }
            else { ViewBag.deletefailed = errormsg; }

            return RedirectToAction("ListaUsuarios");

        }

         public ActionResult Bloquear(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(context));
            ApplicationUser user = UserManager.FindById(id);

            string newLockdate = "2050-01-01 13:46:27.467";
            DateTime dt = Convert.ToDateTime(newLockdate);
            string errormsg = "Ocorreu um erro ao tentar bloquer usuário " + user.nome;
            if (user != null)
            {
                user.LockoutEndDateUtc = dt;
                UserManager.Update(user);
            }
            else
            {
                ViewBag.deletefailed = errormsg;
            }
            return RedirectToAction("ListaUsuarios", "Usuarios");
        }

        public ActionResult Desbloquear(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(context));
            ApplicationUser user = UserManager.FindById(id);
            string errormsg = "Ocorreu um erro ao tentar bloquer usuário " + user.nome;
            if (user != null)
            {
                if (user != null)
                {
                    user.LockoutEndDateUtc = null;
                    UserManager.Update(user);

                }
                else
                {
                    ViewBag.deletefailed = errormsg;
                }
            }
            return RedirectToAction("ListaUsuarios", "Usuarios");
        }

        private bool EmailIsValid(string eaddress)
        {
            string[] emailsTo = { "" };
            bool eaddressIsValid = true;
            if (!String.IsNullOrEmpty(eaddress))
            {
                if (!String.IsNullOrEmpty(eaddress))
                {
                    bool checkemail = IsValidEmailAddress(eaddress.Trim());
                    if (checkemail == false)
                    {
                        eaddressIsValid = false;
                    }
                }

            }
            return eaddressIsValid;
        }
    
        private static bool IsValidEmailAddress(string emailAddress)
        {
            return new System.ComponentModel.DataAnnotations
                                .EmailAddressAttribute()
                                .IsValid(emailAddress);
        }
 
        public ActionResult GetUsuarios()
        {
            var result = repository.GetUsuarios();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopulatePerfis(string tipo = "")
        {
             var result = repository.PerfisAll(true);
             return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetResponsaveis()
        {
            var result = repository.UsuariosAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ApplicationUserManager UserManager
        {
            get
            {

                return usermanager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

        }

    }
}

