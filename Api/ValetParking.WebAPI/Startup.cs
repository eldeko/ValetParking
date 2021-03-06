using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ValetParking.BusinessLogic.Helpers;
using ValetParking.Persistence.Repositories;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;
using ValetParking.WebApi.Controllers;
using ValetParking.WebApi.Exceptions;
using ValetParking.WebApi.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.BusinessLogic.Business;
using Microsoft.AspNetCore.Authorization;

namespace ValetParking.WebApi
{
    public class Startup
    {
        /// <summary>
        ///     Url constant used for allowing CORS to local env
        /// </summary>
        private const string LocalhostUrl = "http://localhost:54103";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllersWithViews();

            #region Swagger

            services.AddSwaggerGen(swagger =>
            {
                swagger.CustomSchemaIds(x => x.FullName);
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Valet Parking", Version = "V1" });
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            
        });

            #endregion Swagger

            #region JSON Web Token

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            #endregion JSON Web Token

            #region Dependency configuration

            //services.AddDbContext<PassDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OLDConfig???")));

            //TODO: Implement "For each interface, register its service" method
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();
            services.AddScoped<IPasswordRecoveryRepository, PasswordRecoveryRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();

            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
           
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IParkingSlotService, ParkingSlotService>();
            services.AddScoped<IPasswordRecoveryManager, PasswordRecoveryManager>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVehicleService, VehicleService>();


            #endregion Dependency configuration    

            #region Mapper

            var mapper = ManageMapper.SetupMapper();
            services.AddSingleton(mapper);

            #endregion Mapper

            #region Mailer

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailSender, EmailSender>();

            #endregion Mailer

           // Enable for api versioning
           // SetupApiVersion(services);
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
                app.UseCors(options => options.WithOrigins("*")
                   .AllowAnyMethod().AllowAnyHeader());

                app.UseDeveloperExceptionPage();
            

            // set up exception Middleware
            app.UseMiddleware<PassExceptionMiddleware>();
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
                c.RoutePrefix = string.Empty;
            });
          
        }

        #region Private methods

        ///// <summary>
        ///// Configure api version for each controller
        ///// </summary>
        ///// <param name="services"></param>
        //private void SetupApiVersion(IServiceCollection services)
        //{
        //    var apiVersion = new ApiVersion(1, 0);
        //    services.AddApiVersioning(options =>
        //    {
        //        options.ReportApiVersions = true;
        //        options.DefaultApiVersion = new ApiVersion(1, 0);
        //        options.Conventions.Controller<ConfigurationController>().HasApiVersion(apiVersion);
        //        options.Conventions.Controller<UserController>().HasApiVersion(apiVersion);
        //    });
        //}

        #endregion Private methods
    }
}
