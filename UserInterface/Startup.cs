using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public void ConfigureServices(IServiceCollection services)
        {
            /* services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"])); */
            services.AddControllersWithViews();
            /* services.AddTransient<AppDb>(_ => new AppDb(Configuration["ConnectionStrings:Default"])); */
            /* services.AddTransient<IAppDb, AppDb>(_ => new AppDb(Configuration["ConnectionStrings:Default"])); */
            services.AddTransient<IAppDb, AppDb>(_ => new AppDb("server=localhost;user=testx;password=apa;Database=testdatabasex"));
            services.AddTransient<IUserRolesRepository, UserRolesRepository>();
            services.AddTransient<IPasswordManagementService, PasswordManagementService>();
            services.AddSingleton<IUserBaseService, UserBaseService>();
            services.AddTransient<ICreateUserService, CreateUserService>();
            services.AddTransient<ILoginUserService, LoginUserService>();
        }

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
