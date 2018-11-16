using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto;

namespace Proyecto.Controllers
{
    public class bodegasController : Controller
    {
        private inventarioEntities db = new inventarioEntities();

        // GET: bodegas
        public async Task<ActionResult> Index()
        {
            var bodega = db.bodega.Include(b => b.estante);
            return View(await bodega.ToListAsync());
        }

        // GET: bodegas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = await db.bodega.FindAsync(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // GET: bodegas/Create
        public ActionResult Create()
        {
            ViewBag.id_estante = new SelectList(db.estante, "id_estante", "n_estante");
            return View();
        }

        // POST: bodegas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_bodega,Nombre_Bodega,id_estante")] bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.bodega.Add(bodega);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_estante = new SelectList(db.estante, "id_estante", "n_estante", bodega.id_estante);
            return View(bodega);
        }

        // GET: bodegas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = await db.bodega.FindAsync(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_estante = new SelectList(db.estante, "id_estante", "n_estante", bodega.id_estante);
            return View(bodega);
        }

        // POST: bodegas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_bodega,Nombre_Bodega,id_estante")] bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodega).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_estante = new SelectList(db.estante, "id_estante", "n_estante", bodega.id_estante);
            return View(bodega);
        }

        // GET: bodegas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = await db.bodega.FindAsync(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // POST: bodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            bodega bodega = await db.bodega.FindAsync(id);
            db.bodega.Remove(bodega);
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
