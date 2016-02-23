using IdentityServer3.Core.Configuration;
using Owin;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServer
{
    public class Startup
    {
        public object Clients { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServer(new IdentityServerOptions
            {
                RequireSsl = false,
                SiteName = "Embedded IdentityServer",
                SigningCertificate = LoadCertificate(),
                EnableWelcomePage = true,

                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(Users.Get())
                    .UseInMemoryClients(ClientConfig.Get())
                    .UseInMemoryScopes(Scopes.Get())
            });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Certificate\idsrv3test.pfx"), "idsrv3test");
        }
    }
}
