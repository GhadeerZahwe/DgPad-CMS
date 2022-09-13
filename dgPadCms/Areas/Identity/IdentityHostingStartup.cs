using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(dgPadCms.Areas.Identity.IdentityHostingStartup))]
namespace dgPadCms.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}