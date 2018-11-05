using Microsoft.Owin;
using Owin;
using comment_system_01.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using comment_system_01.DAL;
using comment_system_01.Models.Account;

[assembly: OwinStartup(typeof(comment_system_01.Startup))]

namespace comment_system_01
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // настраиваем контекст и менеджер
            app.CreatePerOwinContext<MovieDBContext>(MovieDBContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}