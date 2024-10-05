using BLL.interfaces;
using BLL.Repository;
using DAL.Data.Context;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_PL_.Mapping;
using System;

namespace Project_PL_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DI to DbContext

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeRepository, EmployeeRepository>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingEmployee()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingDepartment()));

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
						   .AddEntityFrameworkStores<AppDbContext>()
                           .AddDefaultTokenProviders();

			builder.Services.ConfigureApplicationCookie(config =>
            config.LoginPath="/Account/SignIn");

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
