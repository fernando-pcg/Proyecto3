using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCrud3.Models;

namespace TestCrud3.Controllers
{
    public class VisitasController : Controller
    {
        private TercerTestEntities2 db = new TercerTestEntities2();

        // GET: Visitas
        public ActionResult Index()
        {
            var visitas = db.Visitas.Include(v => v.Area).Include(v => v.Persona);
            return View(visitas.ToList());
        }

        // GET: Visitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // GET: Visitas/Create
        public ActionResult Create()
        {
            ViewBag.IdArea = new SelectList(db.Area, "IdArea", "Nombre");
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre");
            return View();
        }

        // POST: Visitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdVisitas,Fecha,Hora_Entrada,Hora_Salida,IdPersona,Motivo_Visita,IdArea")] Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                db.Visitas.Add(visitas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArea = new SelectList(db.Area, "IdArea", "Nombre", visitas.IdArea);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", visitas.IdPersona);
            return View(visitas);
        }

        // GET: Visitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.Area, "IdArea", "Nombre", visitas.IdArea);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", visitas.IdPersona);
            return View(visitas);
        }

        // POST: Visitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVisitas,Fecha,Hora_Entrada,Hora_Salida,IdPersona,Motivo_Visita,IdArea")] Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.Area, "IdArea", "Nombre", visitas.IdArea);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", visitas.IdPersona);
            return View(visitas);
        }

        // GET: Visitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitas visitas = db.Visitas.Find(id);
            db.Visitas.Remove(visitas);
            db.SaveChanges();
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
