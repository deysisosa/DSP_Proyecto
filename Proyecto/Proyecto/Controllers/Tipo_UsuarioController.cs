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
    public class Tipo_UsuarioController : Controller
    {
        private inventarioEntities db = new inventarioEntities();

        // GET: Tipo_Usuario
        public async Task<ActionResult> Index()
        {
            return View(await db.Tipo_Usuario.ToListAsync());
        }

        // GET: Tipo_Usuario/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = await db.Tipo_Usuario.FindAsync(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_tipo,Tipo_Usuario1")] Tipo_Usuario tipo_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Usuario.Add(tipo_Usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = await db.Tipo_Usuario.FindAsync(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Usuario);
        }

        // POST: Tipo_Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_tipo,Tipo_Usuario1")] Tipo_Usuario tipo_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = await db.Tipo_Usuario.FindAsync(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Usuario);
        }

        // POST: Tipo_Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tipo_Usuario tipo_Usuario = await db.Tipo_Usuario.FindAsync(id);
            db.Tipo_Usuario.Remove(tipo_Usuario);
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
