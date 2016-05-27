using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using BizManWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BizManWeb
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            services.AddAuthentication(options =>
            {
                options.SignInScheme = Constants.CookieMiddleWareIdentifier;
            });
            //Configuration["Data:DefaultConnection:ConnectionString"]
            services.AddDbContext<BMGContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            services.AddTransient<IJsonDataImporter, JsonImporter>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = Constants.CookieMiddleWareIdentifier,
                LoginPath = new PathString("/Account/Login"),
                AccessDeniedPath = new PathString("/Account/Forbidden"),
                LogoutPath = new PathString("/Account/Logout"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = Configuration["BizMan_GoogleAuth_ClientId"],
                ClientSecret = Configuration["BizMan_GoogleAuth_ClientSecret"]
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
