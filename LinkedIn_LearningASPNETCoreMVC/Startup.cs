using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia
{
    public class Startup
    {

        #region User custom configuration
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FeatureToggles>( x => {
                return new FeatureToggles { DeveloperExceptions = _configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions") };
            });


            services.AddDbContext<BlogDataContext>(options => {
                var connectionString = _configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connectionString);
            } );
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FeatureToggles features)
        {
            #region handling and diagnostics
            // app.UseExceptionHandler("/error.html");
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region DependencyInjection
            //if (_configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions"))
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //if (features.DeveloperExceptions)
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            #endregion

            #region Respond to HTTP Requests
            // context represents request
            // next is the next middleware function regisered in pipeline
            //app.Use(async (context, next) =>
            //{

            //    // Look for a pattern that starts with /hello
            //    if (context.Request.Path.Value.StartsWith("/hello"))
            //    {
            //        await context.Response.WriteAsync("Hello ASP.NET Core!");
            //    }

            //    await next();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(" How are you?!");
            //});
            #endregion

            #region Error handling and diagnostics demo
            app.Use(async (context, next) =>
            {
                               
                if (context.Request.Path.Value.Contains("invalid"))
                {
                    throw new Exception("ERROR!");
                }

                await next();
            });

            #endregion

            #region Customize Application URL's
            app.UseMvc( routes => {
                routes.MapRoute("Default",
                                             "{controller=Home}/{action=Index}/{id?}");
            });

            

            #endregion
            #region Serving Static Files
            // looks at any url and tries to locate in wwwroot 
            // registers static files
            app.UseFileServer();
            #endregion

           

        }
    }
}
