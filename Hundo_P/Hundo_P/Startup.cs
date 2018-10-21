using Hundo_P.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hundo_P.Startup))]
namespace Hundo_P
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private static void CreateRoles()
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Free"))
            {
                var role = new IdentityRole("Free");
                roleManager.Create(role);
            }
        }
    }
}
