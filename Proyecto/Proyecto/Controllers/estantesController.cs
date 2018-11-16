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
    public class estantesController : Controller
    {
        private inventarioEntities db = new inventarioEntities();

        // GET: estantes
        public async Task<ActionResult> Index()
        {
            var estante = db.estante.Include(e => e.Categoria);
            return View(await estante.ToListAsync());
        }

        // GET: estantes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estante estante = await db.estante.FindAsync(id);
            if (estante == null)
            {
                return HttpNotFound();
            }
            return View(estante);
        }

        // GET: estantes/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "Nombre_categoria");
            return View();
        }

        // POST: estantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_estante,n_estante,id_categoria")] estante estante)
        {
            if (ModelState.IsValid)
            {
                db.estante.Add(estante);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "Nombre_categoria", estante.id_categoria);
            return View(estante);
        }

        // GET: estantes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estante estante = await db.estante.FindAsync(id);
            if (estante == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "Nombre_categoria", estante.id_categoria);
            return View(estante);
        }

        // POST: estantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_estante,n_estante,id_categoria")] estante estante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estante).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "Nombre_categoria", estante.id_categoria);
            return View(estante);
        }

        // GET: estantes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            estante estante = await db.estante.FindAsync(id);
            if (estante == null)
            {
                return HttpNotFound();
            }
            return View(estante);
        }

        // POST: estantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            estante estante = await db.estante.FindAsync(id);
            db.estante.Remove(estante);
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
