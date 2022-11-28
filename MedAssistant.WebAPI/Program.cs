using Hangfire;
using Hangfire.SqlServer;
using MedAssistant.Buisness.Services;
using MedAssistant.Core.Abstractions;
using MedAssistant.Data.Abstractions.Repositories;
using MedAssistant.Data.Repositories;
using MedAssistant.Data.Repositories.Repositories;
using MedAssistant.DataBase;
using MedAssistant.DataBase.Entities;
using MedAssistant.WebAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;

namespace MedAssistant.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


           builder.Host.UseSerilog((ctx, lc) =>
           lc.WriteTo.File(@"D:\.Net\ASP.NET Core MVC\IT ACADEMY\Logs\data.log", LogEventLevel.Information)
           .WriteTo.Console(LogEventLevel.Verbose));

            // Add services to the container.

            builder.Services.AddControllers();


            var connectionString = builder.Configuration.GetConnectionString("Default");
             
            //dependency Injection DataBase
            builder.Services.AddDbContext<MedAssistantContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));


            // Add Hangfire services.
            builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connectionString,
            new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));


            // Add the processing server as IHostedService
            builder.Services.AddHangfireServer();

            //dependency Injection AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Dependency Injection Services 
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IVaccinationService, VaccinationService>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IMedicalInstitutionService, MedicalInstitutionService>();
            builder.Services.AddScoped<INoteSetvice, NoteService>();
            builder.Services.AddScoped<INoteTypeService, NoteTypeService>();
            builder.Services.AddScoped<IDoctorTypeService, DoctorTypeService>();
            builder.Services.AddScoped<IMedicineService, MedicineService>();
            builder.Services.AddScoped<IJWTUtil, JWTUtilSha256>();

            //Dependency Injection GenericRepository
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IRepository<Account>, Repository<Account>>();
            builder.Services.AddScoped<IRepository<Role>, Repository<Role>>();
            builder.Services.AddScoped<IRepository<Vaccination>, Repository<Vaccination>>();
            builder.Services.AddScoped<IRepository<VaccinationType>, Repository<VaccinationType>>();
            builder.Services.AddScoped<IRepository<Prescription>, Repository<Prescription>>();
            builder.Services.AddScoped<IRepository<Medicine>, Repository<Medicine>>();
            builder.Services.AddScoped<IRepository<MedicalInstitution>, Repository<MedicalInstitution>>();
            builder.Services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();
            builder.Services.AddScoped<IRepository<Note>, Repository<Note>>();
            builder.Services.AddScoped<IRepository<NoteType>, Repository<NoteType>>();
            builder.Services.AddScoped<IRepository<DoctorType>, Repository<DoctorType>>();

            //Dependency Injection UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(builder.Configuration["XmlDoc"]);
            } );

            //Configurating Authentication with JWT Token 
            builder.Services.AddAuthentication(options => 
            { 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt=>
            { 
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidAudience = builder.Configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:JWTSecret"])),
                    ClockSkew = TimeSpan.Zero
                }; 
            });

            var app = builder.Build();

            app.UseStaticFiles(); 
            app.UseHangfireDashboard();
            app.UseRouting();

            app.UseHttpsRedirection();
             
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapHangfireDashboard();


            
            app.UseAuthentication();
            app.UseAuthorization();
             
            app.MapControllers();
             
            app.Run();
        }
    }
}