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
    public class UsuariosController : Controller
    {
        private inventarioEntities db = new inventarioEntities();

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Tipo_Usuario);
            return View(await usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.TipoU = new SelectList(db.Tipo_Usuario, "id_tipo", "Tipo_Usuario1");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Nombre,Apellido,Nombre_Usuario,Contraseña,TipoU")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TipoU = new SelectList(db.Tipo_Usuario, "id_tipo", "Tipo_Usuario1", usuarios.TipoU);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoU = new SelectList(db.Tipo_Usuario, "id_tipo", "Tipo_Usuario1", usuarios.TipoU);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Nombre,Apellido,Nombre_Usuario,Contraseña,TipoU")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TipoU = new SelectList(db.Tipo_Usuario, "id_tipo", "Tipo_Usuario1", usuarios.TipoU);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            db.Usuarios.Remove(usuarios);
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
