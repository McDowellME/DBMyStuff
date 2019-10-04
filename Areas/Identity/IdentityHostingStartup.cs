using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalPropertyApp.Models;

[assembly: HostingStartup(typeof(PersonalPropertyApp.Areas.Identity.IdentityHostingStartup))]
namespace PersonalPropertyApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<PersonalPropertyAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PersonalPropertyAppContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<PersonalPropertyAppContext>();
            });
        }
    }
}