
//************************************ start of program ***********************//

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Programming_7312_Part_1.Services;

namespace Programming_7312_Part_1
{
    public class Program
    {
        // start of main method and entry to the program 
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddControllersWithViews();
                        services.AddSingleton<IssueStorage>(); // Register IssueStorage
                    })
                    .Configure(app =>
                    {
                        var env = app.ApplicationServices.GetService<IWebHostEnvironment>();
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseStaticFiles();
                        app.UseRouting();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                        });
                    });
                });
    }
    //********************** end of main method 
}
//***************************** end of program **********************************//