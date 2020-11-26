using Microsoft.AspNetCore.Hosting;
using ModbusMaster.Client.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace ModbusMaster.Client.Areas.Identity
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