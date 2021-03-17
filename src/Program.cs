using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PrinterWebInterface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();

            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>()
                                     .UseConfiguration(config);
                       });
        }
    }
}
