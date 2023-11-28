using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using todoList.Business.Interfaces;
using todoList.Business.MapperProfile;
using todoList.Business.Services;
using todoList.DataAccess.Data;
using todoList.DataAccess.Repositories;
using todoList.DataAccess.Repositories.Interfaces;

namespace todoList.Web
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
            var connectionString = Configuration.GetConnectionString("db");

            services.AddControllersWithViews();

            services.AddScoped<ITaskRepository, EFTaskRepository>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IListRepository, EFListRepository>();
            services.AddScoped<IUsersTasksRepository, EFUsersTasksRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IListService, ListService>();

            services.AddDbContext<TodoListDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddAutoMapper(typeof(MapProfile));
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/User/Login";
                    opt.AccessDeniedPath = "/AccessDenied";
                });
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            
            app.UseAuthentication();
            app.UseAuthorization();

            // TODO Configure endpoints
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "user",
                //    pattern: "User/{userId}",
                //    defaults:new
                //    {
                //        controller="User",
                //        action="Index"
                //    });

                //endpoints.MapControllerRoute(
                //    name: "list",
                //    pattern: "User{userId}/List{listId}/",
                //    defaults:new
                //    {
                //        controller="List",
                //        action="Index"
                //    });

                //endpoints.MapControllerRoute(
                //    name: "task",
                //    pattern: "User{userId}/List{listId}/{id?}/{action=Index}",
                //    defaults:new
                //    {
                //        controller="Tasks"
                //    });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

// TODO Change main page after login
// TODO Add lists
// TODO Add tasks
// TODO Add password encryption
// TODO Add DTOs (SECONDARY)
// TODO 