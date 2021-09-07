using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ValetParking.BusinessLogic.Dependency;
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

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Valet Parking", Version = "V1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Scheme = "Bearer",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Id = "Bearer", //The name of the previously defined security scheme.
				Type = ReferenceType.SecurityScheme
            }
        },new List<string>()
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

            #endregion JSON Web Token

            #region Dependency configuration

            //services.AddDbContext<PassDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OLDConfig???")));
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();
            services.AddScoped<IPasswordRecoveryRepository, PasswordRecoveryRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();

            //LogicDependency.RegistryDependency(services, Configuration);

            #endregion Dependency configuration

            //#region Security
            //SetupSecurity(services);
            //#endregion

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(options => options.WithOrigins("*")
                  .AllowAnyMethod().AllowAnyHeader());
                app.UseDeveloperExceptionPage();
            }

            // set up exception Middleware
            app.UseMiddleware<PassExceptionMiddleware>();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "post API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Private methods

        /// <summary>
        /// Configure api version for each controller
        /// </summary>
        /// <param name="services"></param>
        private void SetupApiVersion(IServiceCollection services)
        {
            var apiVersion = new ApiVersion(1, 0);
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.Conventions.Controller<ConfigurationController>().HasApiVersion(apiVersion);
                options.Conventions.Controller<UserController>().HasApiVersion(apiVersion);
            });
        }

        #endregion Private methods
    }
}
