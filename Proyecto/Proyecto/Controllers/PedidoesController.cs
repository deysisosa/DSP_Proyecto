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
    public class PedidoesController : Controller
    {
        private inventarioEntities db = new inventarioEntities();

        // GET: Pedidoes
        public async Task<ActionResult> Index()
        {
            var pedido = db.Pedido.Include(p => p.Usuarios);
            return View(await pedido.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = await db.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public ActionResult Create()
        {
            ViewBag.Nombre_Usuario = new SelectList(db.Usuarios, "Nombre_Usuario", "Nombre");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_pedido,Nombre_Usuario,fecha_corte,fecha_facturacion")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedido.Add(pedido);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Nombre_Usuario = new SelectList(db.Usuarios, "Nombre_Usuario", "Nombre", pedido.Nombre_Usuario);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = await db.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nombre_Usuario = new SelectList(db.Usuarios, "Nombre_Usuario", "Nombre", pedido.Nombre_Usuario);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_pedido,Nombre_Usuario,fecha_corte,fecha_facturacion")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Nombre_Usuario = new SelectList(db.Usuarios, "Nombre_Usuario", "Nombre", pedido.Nombre_Usuario);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = await db.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pedido pedido = await db.Pedido.FindAsync(id);
            db.Pedido.Remove(pedido);
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
