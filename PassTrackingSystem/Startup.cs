using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
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
            string identityConnection = Configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<ApplicationDBContext>(options =>
               options.UseSqlServer(connection).EnableSensitiveDataLogging());
            services.AddDbContext<AppidentityDbContext>(options =>
              options.UseSqlServer(identityConnection).EnableSensitiveDataLogging());
            services.AddIdentity<AppUser, IdentityRole>(op => { op.Password.RequireDigit = false; 
                op.Password.RequiredLength = 1;
                op.Password.RequireLowercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireLowercase = false;
            })
                
                .AddEntityFrameworkStores<AppidentityDbContext>()
                .AddDefaultTokenProviders();
 
            services.AddControllersWithViews();
            services.AddScoped<IGenericRepository<Visitor>, GenericRepository<Visitor>>();
            services.AddScoped<IGenericRepository<Document>, GenericRepository<Document>>();
            services.AddScoped<IGenericRepository<DocumentType>, GenericRepository<DocumentType>>();
            services.AddScoped<IGenericRepository<IssuingAuthority>, GenericRepository<IssuingAuthority>>();
            services.AddScoped<IGenericRepository<TemporaryPass>, GenericRepository<TemporaryPass>>();
            services.AddScoped<IGenericRepository<StationFacility>, GenericRepository<StationFacility>>();
            services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
            services.AddScoped<IGenericRepository<SinglePass>, GenericRepository<SinglePass>>();
            services.AddScoped<IGenericRepository<ShootingPermission>, GenericRepository<ShootingPermission>>();
            services.AddScoped<IGenericRepository<CarPass>, GenericRepository<CarPass>>();
            services.AddScoped<IGenericRepository<Car>, GenericRepository<Car>>();
            services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
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
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=VisitorForm}/{action=VisitorProcessing}/{id?}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=OneItemData}/{action=ShowOneItemData}/{oneItemTypeName=DocumentType}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=TemporaryPass}/{action=ShowAll}/{id?}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=TemporaryPass}/{action=TemporaryPassProcessing}/{id?}");
            //});


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=CarPass}/{action=CarPassProcessing}/{id=4}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Admin}/{action=Show}/{id=0}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Admin}/{action=ShowAll}/{id?}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=ShootingPermission}/{action=ShootingPermissionProcessing}/{id=100}");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Account}/{action=Logout}/{id?}");
            //});
        }
    }
}
