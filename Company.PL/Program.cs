using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Data.Contexts;
using Company.PL.Maping;
using Microsoft.EntityFrameworkCore;

namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Register Built-in MVC services

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Register DI for DepartmentRepository
            builder.Services.AddScoped<IEmployeeReposoitory, EmployeeRepository>(); // Register DI for EmployeeRepository
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
             //options.UseLazyLoadingProxies();
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Register DI for CompanyDbContext
            builder.Services.AddAutoMapper(E=>E.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(D => D.AddProfile(new DepartmentProfile()));



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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
