﻿using System;
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
    public class ProveedoresController : Controller
    {
        private inventarioEntities db = new inventarioEntities();

        // GET: Proveedores
        public async Task<ActionResult> Index()
        {
            return View(await db.Proveedores.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = await db.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_proveedor,Nombre_Proveedor,Nombre_Contacto,Pais,telefono1,telefono2,correo1,correo2")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Proveedores.Add(proveedores);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = await db.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_proveedor,Nombre_Proveedor,Nombre_Contacto,Pais,telefono1,telefono2,correo1,correo2")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedores).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = await db.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Proveedores proveedores = await db.Proveedores.FindAsync(id);
            db.Proveedores.Remove(proveedores);
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
