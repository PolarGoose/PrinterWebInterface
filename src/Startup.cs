using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrinterWebInterface.Logging;
using PrinterWebInterface.Logging.Syncs;
using PrinterWebInterface.Printer;
using PrinterWebInterface.Validation;
using System.Runtime.InteropServices;

namespace PrinterWebInterface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidatiomAttributeAdapterProvider>();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Log.SetLogger(new SystemdSync());
                services.AddSingleton<IPrinter, LinuxPrinter>();
            }
            else
            {
                Log.SetLogger(new ConsoleSync());
                services.AddSingleton<IPrinter, DummyPrinter>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}
