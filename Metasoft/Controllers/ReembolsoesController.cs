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
using System.Globalization;
using System.Security.Claims;

namespace Metasoft.Controllers
{
    public class ReembolsoesController : Controller
    {
        private readonly IRepository repository;
        public ReembolsoesController(IRepository objIrepository) { repository = objIrepository; }

        // GET: Reembolsoes
        public ActionResult ListaReembolso()
        {
           
            return View(repository.ReembolsosAll(DateTime.Now.Month, DateTime.Now.Year));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult ListaReembolso(FormCollection fc)
        {
            
            ViewBag.usuarioselected = String.IsNullOrEmpty(fc["usuarios"].ToString()) ? "" : fc["usuarios"].ToString();
            ViewBag.anoselected = String.IsNullOrEmpty(fc["anos"].ToString()) ? DateTime.Now.Year.ToString() : fc["anos"].ToString();
            ViewBag.messelected = String.IsNullOrEmpty(fc["meses"].ToString()) ? DateTime.Now.Year.ToString() : fc["meses"].ToString();
            return View(repository.ReembolsosUsuario(int.Parse(ViewBag.messelected), int.Parse(ViewBag.anoselected),ViewBag.usuarioselected));
        }


        public ActionResult ReembolsoUsuario()
        {
           
            ViewBag.mes = "";  ViewBag.ano = "";  ViewBag.tipo = "";
            return View(repository.ReembolsosUsuario(DateTime.Now.Month, DateTime.Now.Year,GetUserId()));
        }

        [HttpPost]
        public ActionResult ReembolsoUsuario(FormCollection fc)
        {
            
            ViewBag.mes = String.IsNullOrEmpty(fc["meses"].ToString()) ? DateTime.Now.Month.ToString(): fc["meses"].ToString();
            ViewBag.ano = String.IsNullOrEmpty(fc["anos"].ToString()) ? DateTime.Now.Year.ToString() : fc["anos"].ToString();
            ViewBag.tipo = String.IsNullOrEmpty(fc["tipo"].ToString()) ? "" : fc["tipo"].ToString();
            return View(repository.ReembolsosUsuario(int.Parse(ViewBag.mes), int.Parse(ViewBag.ano), GetUserId(), ViewBag.tipo));
        }

        public ActionResult AlterarReembolso(int id)
        {
            
            Reembolso reembolso = repository.GetReembolso(id);
            if (reembolso == null){return HttpNotFound();}
            ViewBag.tiporeembolsoid = reembolso.tipoid;
            ViewBag.referencia = reembolso.vencimento.ToString().Substring(3,7);
            return View(reembolso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarReembolso([Bind(Include = "reembolsoid,dtcad,tipoid,userid,valor,vencimento,dtpagto,situacao,descricao")] Reembolso reembolso, FormCollection fc)
        {
           
            if (ModelState.IsValid)
            {
             
                int reembolsotipo = int.Parse(fc["tiporeembolso"].ToString());
                reembolso.tipoid = reembolsotipo;
                Boolean result = repository.AlterarReembolso(reembolso);
                
                return RedirectToAction("ReembolsoUsuario");
            }
            return View(reembolso);
        }

        private string GetUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return userIdClaim.Value;
        }

        public ActionResult CadastrarReembolso(int mes = 0, int ano= 0)
        {
            
            mes = (mes == 0) ? DateTime.Now.Month : mes;
            ano = (ano == 0) ? DateTime.Now.Year : ano;

           
            ReembolsoItem  reembolsosdousuario = new Models.ReembolsoItem(new Repository());
            reembolsosdousuario.dtcad = DateTime.Now;
            reembolsosdousuario.situacao = "A";
            reembolsosdousuario.GetReembLancados(mes,ano, GetUserId());

            return View(reembolsosdousuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarReembolso([Bind(Include = "tipoid,userid,valor,vencimento,descricao")] ReembolsoItem reembolso, FormCollection fc)
        {
            
            if (ModelState.IsValid)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    // the principal identity is a claims identity.
                    // now we need to find the NameIdentifier claim
                    var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                    if (userIdClaim != null)
                    {

                        Reembolso reembolsoreg = new Reembolso();
                        DateTime vencimento = DateTime.Parse(fc["vencimento"].ToString());
                        try{
                            reembolsoreg.tipoid = int.Parse(fc["tipoid"].ToString());
                            var userid = userIdClaim.Value;
                            reembolsoreg.userid = userid;
                            string svalor = reembolso.valor.ToString();
                            svalor.Replace(",", "#").Replace(".", "").Replace("#", ".");
                            reembolsoreg.valor = decimal.Parse(svalor, NumberStyles.AllowDecimalPoint);
                            reembolsoreg.situacao = "A";
                            reembolsoreg.vencimento = vencimento;
                            reembolsoreg.descricao = reembolso.descricao;
                            int reembolsoid = repository.NovoReembolso(reembolsoreg);

                            ViewBag.mes = reembolsoreg.vencimento.Month;
                            ViewBag.ano = reembolsoreg.vencimento.Year;

                            if (reembolsoid > 0)
                            {
                                ViewBag.cadastramento = "Reembolso cadastrado com sucesso";
                                return RedirectToAction("CadastrarReembolso", new { mes= reembolsoreg.vencimento.Month, ano=reembolsoreg.vencimento.Year});
                            }
                        }
                        catch
                        {
                            /*Do nothing */
                        }
  
                    }
                 }

            }

            ViewBag.mes = DateTime.Now.Month;
            ViewBag.ano = DateTime.Now.Year;
            ViewBag.vencimento = (reembolso.vencimento != null) ? reembolso.vencimento.ToShortDateString() : "";
            ViewBag.tipoid = (reembolso.tipoid > 0) ? reembolso.tipoid.ToString() : "0";
            ViewBag.descricao = reembolso.descricao;
            return View(reembolso);
        }
        public ActionResult PopulateTiposReembolso()
        {
            var result = repository.TiposReembolsosAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExcluirReembolso(int id)
        {
           
            Boolean result = repository.ExcluirReembolso(id);
            if (!result)
            {
                return HttpNotFound();
            }
            return RedirectToAction("ListaReembolso", "Reembolsoes");
        }

        //// GET: Reembolsoes/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Reembolso reembolso = await db.Reembolsos.FindAsync(id);
        //    if (reembolso == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(reembolso);
        //}

        //// POST: Reembolsoes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "reembolsoid,dtcad,tipoid,userid,valor,vencimento,dtpagto,situacao")] Reembolso reembolso)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(reembolso).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(reembolso);
        //}

        //// GET: Reembolsoes/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Reembolso reembolso = await db.Reembolsos.FindAsync(id);
        //    if (reembolso == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(reembolso);
        //}

        //// POST: Reembolsoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Reembolso reembolso = await db.Reembolsos.FindAsync(id);
        //    db.Reembolsos.Remove(reembolso);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
