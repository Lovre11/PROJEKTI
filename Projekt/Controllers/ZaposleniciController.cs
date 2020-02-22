using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Projekt.Models;
using Projekt.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ZaposleniciController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ZaposleniciIRole()
        {
            var zaposleniciRole = (from user in db.Users 
                                   select new
                                   {
                                       Ime = user.Ime,
                                       Prezime = user.Prezime,
                                       UserId = user.Id,
                                       Username = user.UserName,
                                       Email = user.Email,
                                       RoleNames = (from userRole in user.Roles
                                                    join role in db.Roles on userRole.RoleId
                                                    equals role.Id 
                                                    select role.Name).ToList()
                                   }).ToList().Select(p => new ZaposleniciRoleViewModel()

                                   {
                                       Ime = p.Ime,
                                       Prezime = p.Prezime,
                                       UserId = p.UserId,
                                       Username = p.Username,
                                       Email = p.Email,
                                       Rola = string.Join(",", p.RoleNames)
                                   });


            return View(zaposleniciRole);
        }
        public ActionResult DodajRolu(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            ZaposleniciRoleViewModel ur = new ZaposleniciRoleViewModel() { UserId = user.Id };

            return View(ur);
        }

        [HttpPost]
        public ActionResult DodajRolu(ZaposleniciRoleViewModel zr)
        {

            try
            {
                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                um.AddToRole(zr.UserId, zr.Rola);
            }
            catch 
            {
                ViewBag.Poruka = "Dogodila se greška! Provjerite da li unosite ispravan naziv role.";
            } 
            
            return RedirectToAction("ZaposleniciIRole");
        }
    }
}


