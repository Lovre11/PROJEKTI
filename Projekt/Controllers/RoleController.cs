using Microsoft.AspNet.Identity.EntityFramework;
using Projekt.Models;
using System.Linq;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ListaRola()
        {

            return View(db.Roles.ToList());
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(string nazivRole)
        {
            try
            {
                IdentityRole rola = new IdentityRole(nazivRole);
                db.Roles.Add(rola);
                db.SaveChanges();
                return RedirectToAction("ListaRola");
            }
            catch 
            {
                return View();
            }
        }

        public ActionResult Delete(string nazivRole)
        {
            var rola = db.Roles.Where(r => r.Name == nazivRole).FirstOrDefault();
            db.Roles.Remove(rola);
            db.SaveChanges();
            return RedirectToAction("ListaRola");
        } 
    }
}