using Microsoft.Owin;
using Owin;
using ContosoUniversity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(ContosoUniversity.Startup))]
namespace ContosoUniversity
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRole();
            CreateUser();
        }


        public void CreateRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;

            if (!roleManager.RoleExists("Admins"))
            {
                role = new IdentityRole();
                role.Name = "Admins";
                roleManager.Create(role);
            }

        }
        public void CreateUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = new ApplicationUser();

            user.Email = "di.hamdan55@gmail.com";
            user.UserName = "Daif";

            var check = userManager.Create(user, "@Dd39261330h");
            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admins");
            }











        }


    }
}
