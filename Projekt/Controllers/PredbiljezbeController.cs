using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace Projekt.Controllers
{
    [Authorize(Roles = "Zaposlenik,Admin")]
    public class PredbiljezbeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        [HandleError]
        public ActionResult ListaPredbiljezbi(string rijec)
        {
            List<Predbiljezba> LPredbiljezbi = db.Predbiljezbe.ToList();

            if (!String.IsNullOrEmpty(rijec))
            {
                LPredbiljezbi = (from a in LPredbiljezbi
                                 where
         a.Seminar.Naziv.ToLower().Contains(rijec.ToLower()) ||
         a.Ime.ToLower().Contains(rijec.ToLower()) ||
         a.Prezime.ToLower().Contains(rijec.ToLower())
                                 select a).ToList();
            }
            return View(LPredbiljezbi);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predbiljezba predbiljezba = db.Predbiljezbe.Find(id);
            if (predbiljezba == null)
            {
                return HttpNotFound();
            }          
            Session["StatusPredbiljezbe"] = predbiljezba.Status;
            return View(predbiljezba);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPredbiljezba,IdSeminar,Datum,Ime,Prezime,Adresa,Email,KontaktBroj,Status")] Predbiljezba predbiljezba)
        {
            string StatusPredbiljezbe = (string)Session["StatusPredbiljezbe"];
            if (ModelState.IsValid)
            {
                if (StatusPredbiljezbe != predbiljezba.Status)
                {
                    if (predbiljezba.Status == "Odbijen")
                    {
                        Seminar seminar = db.Seminari.Find(predbiljezba.IdSeminar);
                        seminar.BrojPolaznika -= 1;
                        db.Entry(seminar).State = EntityState.Modified;
                    }

                    if (StatusPredbiljezbe == "Odbijen" && predbiljezba.Status == "Prihvaćen")
                    {
                        Seminar seminar = db.Seminari.Find(predbiljezba.IdSeminar); 
                        seminar.BrojPolaznika += 1;
                        db.Entry(seminar).State = EntityState.Modified;
                    }

                    db.Entry(predbiljezba).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ListaPredbiljezbi");
                }
                
            }
            return View(predbiljezba);
        }

        public ActionResult PrihvacenePredbiljezbe()
        {

            List<Predbiljezba> LPredbiljezbi = (from p in db.Predbiljezbe
                                                where p.Status == "Prihvaćen"
                                                select p).ToList();

            return View("ListaPredbiljezbi", LPredbiljezbi);
        }

        public ActionResult OdbijenePredbiljezbe()
        {

            List<Predbiljezba> LPredbiljezbi = (from p in db.Predbiljezbe
                                                where p.Status == "Odbijen"
                                                select p).ToList();

            return View("ListaPredbiljezbi", LPredbiljezbi);
        }

        public ActionResult NeobradjenePredbiljezbe()
        {

            List<Predbiljezba> LPredbiljezbi = (from p in db.Predbiljezbe
                                                where p.Status == null
                                                select p).ToList();

            return View("ListaPredbiljezbi", LPredbiljezbi);
        }
    }
}


    