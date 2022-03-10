using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WaterProject.Models;

namespace Bookstore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //configuration --> gets settings from JSON file
        //needed for the DB stuff!
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //addthe dbcontext!
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);
            });


            //add the ability to reach the repositories we built!!
            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            //this is wehre the BuyBook will go or whatever it is!
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();


            services.AddRazorPages();


            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseSession();





            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "CategoryPage",     //name
                    "{category}/Page{pageNum}",     //pattern
                    new { Controller = "Home", action = "Index" }); //default

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute(
                    "category",     //name
                    "{category}",     //pattern
                    new { Controller = "Home", action = "Index" , pageNum = 1 }); //defaul

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();


            });
        }
    }
}
