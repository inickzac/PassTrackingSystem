using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDBContext>(options =>
               options.UseSqlServer(connection).EnableSensitiveDataLogging());
            services.AddControllersWithViews();
            services.AddTransient<IGenericRepository<Visitor>, GenericRepository<Visitor>>();
            services.AddTransient<IGenericRepository<Document>, GenericRepository<Document>>();
            services.AddTransient<IGenericRepository<DocumentType>, GenericRepository<DocumentType>>();
            services.AddTransient<IGenericRepository<IssuingAuthority>, GenericRepository<IssuingAuthority>>();
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=VisitorForm}/{action=VisitorProcessing}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=OneItemData}/{action=ShowOneItemData}/{oneItemTypeName=DocumentType}");
            //});

        }
    }
}
