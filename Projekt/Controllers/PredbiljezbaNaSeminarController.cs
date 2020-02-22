using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class PredbiljezbaNaSeminarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Lista nepopunjenih seminara
        public ActionResult ListaDostupnihSeminara(string rijec)
        {
            List<Seminar> ListaSeminara = (from p in db.Seminari
                                           where p.Popunjen != true && p.Datum >= DateTime.Now
                                           select p).ToList();

            if (!String.IsNullOrEmpty(rijec))
            {
                ListaSeminara = (from a in ListaSeminara where
                                 a.Naziv.ToLower().Contains(rijec.ToLower()) ||
                                 a.Predavac.ToLower().Contains(rijec.ToLower()) 
                                 select a).ToList();
            }

            return View(ListaSeminara);
        }

        public ActionResult PredbiljezbaNaSeminar(int? id)
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
            ViewBag.NazivSem = seminar.Naziv;

            Predbiljezba predbiljezba = new Predbiljezba { IdSeminar = seminar.IdSeminar };

            return View(predbiljezba);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PredbiljezbaNaSeminar([Bind(Include = "IdPredbiljezba,IdSeminar,Datum,Ime,Prezime,Adresa,Email,KontaktBroj,Status")] Predbiljezba predbiljezba)
        {
            if (ModelState.IsValid)
            {
                Seminar seminar = db.Seminari.Find(predbiljezba.IdSeminar);
                seminar.BrojPolaznika += 1;
                db.Entry(seminar).State = EntityState.Modified;
                db.Predbiljezbe.Add(predbiljezba);
                db.SaveChanges();
                return RedirectToAction("ListaDostupnihSeminara");
            }

            return View(predbiljezba);
        }
    }
}