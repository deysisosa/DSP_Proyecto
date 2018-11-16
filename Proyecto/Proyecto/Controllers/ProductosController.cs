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
    public class ProductosController : Controller
    {
        private inventarioEntities db = new inventarioEntities();

        // GET: Productos
        public async Task<ActionResult> Index()
        {
            var productos = db.Productos.Include(p => p.bodega).Include(p => p.Categoria).Include(p => p.estado1).Include(p => p.Proveedores);
            return View(await productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = await db.Productos.FindAsync(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.ubicacion = new SelectList(db.bodega, "id_bodega", "Nombre_Bodega");
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "Nombre_categoria");
            ViewBag.estado = new SelectList(db.estado, "Id_Estado", "Estado1");
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "id_proveedor", "Nombre_Proveedor");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_producto,Nombre_producto,Existencias,Precio_venta,Precio_compra,Descripcion,id_categoria,N_pedido,id_proveedor,ubicacion,estado")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ubicacion = new SelectList(db.bodega, "id_bodega", "Nombre_Bodega", productos.ubicacion);
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "Nombre_categoria", productos.id_categoria);
            ViewBag.estado = new SelectList(db.estado, "Id_Estado", "Estado1", productos.estado);
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "id_proveedor", "Nombre_Proveedor", productos.id_proveedor);
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = await db.Productos.FindAsync(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ubicacion = new SelectList(db.bodega, "id_bodega", "Nombre_Bodega", productos.ubicacion);
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "Nombre_categoria", productos.id_categoria);
            ViewBag.estado = new SelectList(db.estado, "Id_Estado", "Estado1", productos.estado);
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "id_proveedor", "Nombre_Proveedor", productos.id_proveedor);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_producto,Nombre_producto,Existencias,Precio_venta,Precio_compra,Descripcion,id_categoria,N_pedido,id_proveedor,ubicacion,estado")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ubicacion = new SelectList(db.bodega, "id_bodega", "Nombre_Bodega", productos.ubicacion);
            ViewBag.id_categoria = new SelectList(db.Categoria, "id_categoria", "Nombre_categoria", productos.id_categoria);
            ViewBag.estado = new SelectList(db.estado, "Id_Estado", "Estado1", productos.estado);
            ViewBag.id_proveedor = new SelectList(db.Proveedores, "id_proveedor", "Nombre_Proveedor", productos.id_proveedor);
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = await db.Productos.FindAsync(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Productos productos = await db.Productos.FindAsync(id);
            db.Productos.Remove(productos);
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
