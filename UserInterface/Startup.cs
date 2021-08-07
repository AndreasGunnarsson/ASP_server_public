using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using MySqlConnector.Authentication.Ed25519;
using Application;
using Core.Interfaces;
using Infrastructure;

namespace UserInterface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Ed25519AuthenticationPlugin.Install();
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: Se över om man ska använda AddControllersWithViews eller om det finns något mer lightweight.
            // TODO: Tror det finns något sätt att bunta ihop de services man själv skapat för att det ska se mer städat ut..
            Console.WriteLine("ConnectionString: " + Configuration["ConnectionStrings:Default"]);           // Debug.
            /* Ed25519AuthenticationPlugin.Install(); */
            /* services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"])); */
            /* services.AddTransient<AppDb>(_ => new AppDb(Configuration["ConnectionStrings:Default"])); */
            services.AddTransient<IAppDb, AppDb>(_ => new AppDb(Configuration["ConnectionStrings:Default"]));
            services.AddControllersWithViews();
            services.AddTransient<IUserRolesRepository, UserRolesRepository>();
            services.AddSingleton<IUserBaseService, UserBaseService>();
            services.AddTransient<ICreateUserService, CreateUserService>();
            /* services.AddScoped<IMyDependency, MyDependency>(); */
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            /* app.UseHttpsRedirection(); */
            app.UseStaticFiles();

            app.UseRouting();

            /* app.UseAuthorization(); */

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
