using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

//1. the “assembly” attribute which states which class to fire on start-up.
[assembly: OwinStartup(typeof(AngularJSAuthentication.API.Startup))]
namespace AngularJSAuthentication.API
{
    public class Startup
    {

        //2. The “app” parameter is an interface which will be used to compose the application for our Owin server.
        public void Configuration(IAppBuilder app)
        {
            //3.The “HttpConfiguration” object is used to configure API routes, so we’ll pass this object to method “Register” in “WebApiConfig” class.
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //4. pass the “config” object to the extension method “UseWebApi” which will be responsible to wire up ASP.NET Web API to our Owin server pipeline.
            app.UseWebApi(config);

            {
                OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                    Provider = new SimpleAuthorizationServerProvider(),
                };

                // Token Generation
                app.UseOAuthAuthorizationServer(OAuthServerOptions);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            }
        }
    }
}

//5. Feel Free to delete the Global.asax file.
//6 . Add the Asp.net Identity System
//Install-Package Microsoft.AspNet.Identity.Owin -Version 2.0.1 (package will add support for ASP.NET Identity Owin)
//Install-Package Microsoft.AspNet.Identity.EntityFramework -Version 2.0.1 (support for using ASP.NET Identity with Entity Framework so we can save users to SQL Server database.)

//6. Add the Database context class 

//7. Add support for OAuth Bearer Tokens Generation()
//Install-Package Microsoft.Owin.Security.OAuth -Version 2.1.0

//Step 11: Allow CORS for ASP.NET Web API
//Install-Package Microsoft.Owin.Cors -Version 2.1.0