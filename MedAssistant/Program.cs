using MedAssistant.Core.Abstractions;
using MedAssistant.Data.Abstractions.Repositories;
using MedAssistant.Data.Repositories.Repositories;
using MedAssistant.Buisness.Services;

using Microsoft.EntityFrameworkCore;
using MedAssistant.Data.Repositories;
using MedAssistant.DataBase.Entities;
using MedAssistant.DataBase;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.Events;

namespace MedAssistant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((ctx, lc) =>
            lc.WriteTo.File(@"D:\.Net\ASP.NET Core MVC\IT ACADEMY\Logs\data.log",
            LogEventLevel.Information).WriteTo.Console(LogEventLevel.Verbose)
            );

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(1); 
                    options.AccessDeniedPath = new PathString("/Accounts/Authentication");
                    options.LoginPath = new PathString("/Accounts/Authentication");
                });
                


            var connectionString = "Server=.\\SQLEXPRESS;Database=MedAssistantDataBase;Trusted_Connection=True;";


            //dependency Injection DataBase
            builder.Services.AddDbContext<MedAssistantContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));


            //dependency Injection AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Dependency Injection Services 
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IVaccinationService, VaccinationService>();
            builder.Services.AddScoped<IVaccinationService, VaccinationService>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

            //Dependency Injection GenericRepository
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IRepository<Account>, Repository<Account>>();
            builder.Services.AddScoped<IRepository<Role>, Repository<Role>>();
            builder.Services.AddScoped<IRepository<Vaccination>, Repository<Vaccination>>();
            builder.Services.AddScoped<IRepository<VaccinationType>, Repository<VaccinationType>>();
            builder.Services.AddScoped<IRepository<Prescription>, Repository<Prescription>>();
            builder.Services.AddScoped<IRepository<Medicine>, Repository<Medicine>>();

            //Dependency Injection UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

           


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


            app.UseAuthentication(); // Set HttpContex.User
            app.UseAuthorization();  // Check Users Succes 


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}