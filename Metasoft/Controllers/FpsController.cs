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

namespace Metasoft.Controllers
{
    public class FpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fps
        public async Task<ActionResult> Index()
        {
            return View(await db.Fps.ToListAsync());
        }

        // GET: Fps/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fp fp = await db.Fps.FindAsync(id);
            if (fp == null)
            {
                return HttpNotFound();
            }
            return View(fp);
        }

        // GET: Fps/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "fpid,nome")] Fp fp)
        {
            if (ModelState.IsValid)
            {
                db.Fps.Add(fp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fp);
        }

        // GET: Fps/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fp fp = await db.Fps.FindAsync(id);
            if (fp == null)
            {
                return HttpNotFound();
            }
            return View(fp);
        }

        // POST: Fps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "fpid,nome")] Fp fp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fp);
        }

        // GET: Fps/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fp fp = await db.Fps.FindAsync(id);
            if (fp == null)
            {
                return HttpNotFound();
            }
            return View(fp);
        }

        // POST: Fps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fp fp = await db.Fps.FindAsync(id);
            db.Fps.Remove(fp);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
