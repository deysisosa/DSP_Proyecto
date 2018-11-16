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
    public class detalle_productosController : Controller
    {
        private inventarioEntities db = new inventarioEntities();

        // GET: detalle_productos
        public async Task<ActionResult> Index()
        {
            var detalle_productos = db.detalle_productos.Include(d => d.Pedido).Include(d => d.Productos);
            return View(await detalle_productos.ToListAsync());
        }

        // GET: detalle_productos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_productos detalle_productos = await db.detalle_productos.FindAsync(id);
            if (detalle_productos == null)
            {
                return HttpNotFound();
            }
            return View(detalle_productos);
        }

        // GET: detalle_productos/Create
        public ActionResult Create()
        {
            ViewBag.N_pedido = new SelectList(db.Pedido, "id_pedido", "Nombre_Usuario");
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "Nombre_producto");
            return View();
        }

        // POST: detalle_productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_detalle_producto,id_producto,Nombre_producto,Existencia,Precio_venta,Precio_compra,Descripcion,id_categoria,N_pedido,id_proveedor")] detalle_productos detalle_productos)
        {
            if (ModelState.IsValid)
            {
                db.detalle_productos.Add(detalle_productos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.N_pedido = new SelectList(db.Pedido, "id_pedido", "Nombre_Usuario", detalle_productos.N_pedido);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "Nombre_producto", detalle_productos.id_producto);
            return View(detalle_productos);
        }

        // GET: detalle_productos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_productos detalle_productos = await db.detalle_productos.FindAsync(id);
            if (detalle_productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.N_pedido = new SelectList(db.Pedido, "id_pedido", "Nombre_Usuario", detalle_productos.N_pedido);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "Nombre_producto", detalle_productos.id_producto);
            return View(detalle_productos);
        }

        // POST: detalle_productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_detalle_producto,id_producto,Nombre_producto,Existencia,Precio_venta,Precio_compra,Descripcion,id_categoria,N_pedido,id_proveedor")] detalle_productos detalle_productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle_productos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.N_pedido = new SelectList(db.Pedido, "id_pedido", "Nombre_Usuario", detalle_productos.N_pedido);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "Nombre_producto", detalle_productos.id_producto);
            return View(detalle_productos);
        }

        // GET: detalle_productos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalle_productos detalle_productos = await db.detalle_productos.FindAsync(id);
            if (detalle_productos == null)
            {
                return HttpNotFound();
            }
            return View(detalle_productos);
        }

        // POST: detalle_productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            detalle_productos detalle_productos = await db.detalle_productos.FindAsync(id);
            db.detalle_productos.Remove(detalle_productos);
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
