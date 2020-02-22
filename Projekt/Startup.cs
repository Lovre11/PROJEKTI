using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Projekt.Models;

[assembly: OwinStartupAttribute(typeof(Projekt.Startup))]
namespace Projekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


            
            if (!roleManager.RoleExists("Admin"))
            {

                // Kreiranje admin role    
                var rola = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                rola.Name = "Admin";
                roleManager.Create(rola);

                //Kreiranje administratora                   

                var korisnik = new ApplicationUser();
                korisnik.Ime = "Administrator";
                korisnik.Prezime = "Administrator";
                korisnik.UserName = "admin@admin.com";
                korisnik.Email = "admin@admin.com";

                string korisnikPWD = "Admin23!";

                var chkKorisnik = userManager.Create(korisnik, korisnikPWD);

                //Dodavanje role korisniku    
                if (chkKorisnik.Succeeded)
                {
                    var rezultat = userManager.AddToRole(korisnik.Id, "Admin");

                }                
            }

            //Kreiranje role Zaposlenik
            if (!roleManager.RoleExists("Zaposlenik"))
            {
                var rola = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                rola.Name = "Zaposlenik";
                roleManager.Create(rola);

            }
        }
    }

    
}
