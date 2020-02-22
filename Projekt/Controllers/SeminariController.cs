using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Projekt.Models;


namespace Projekt.Controllers
{
    [Authorize(Roles = "Zaposlenik,Admin")]
    public class SeminariController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        [HandleError]
        public ActionResult ListaSeminara(string rijec)
        {
            List<Seminar> LSeminara = db.Seminari.ToList();

            if (!String.IsNullOrEmpty(rijec))
            {
                LSeminara = (from a in LSeminara
                                 where
         a.Naziv.ToLower().Contains(rijec.ToLower()) ||
         a.Predavac.ToLower().Contains(rijec.ToLower())
                                 select a).ToList();
            }

            return View(LSeminara);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminari.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Create([Bind(Include = "IdSeminar,Naziv,Predavac,Opis,Datum,Popunjen")] Seminar seminar)
        {
            if (ModelState.IsValid)
            {
                db.Seminari.Add(seminar);
                db.SaveChanges();
                return RedirectToAction("ListaSeminara");
            }

            return View(seminar);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminari.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSeminar,Naziv,Predavac,Opis,Datum,BrojPolaznika,Popunjen")] Seminar seminar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seminar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaSeminara");
            }
            return View(seminar);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminari.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // POST: Seminari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seminar seminar = db.Seminari.Find(id);
            db.Seminari.Remove(seminar);
            db.SaveChanges();
            return RedirectToAction("ListaSeminara");
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
