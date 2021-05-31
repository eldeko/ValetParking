using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ValetParking.BusinessLogic.Business;
using ValetParking.BusinessLogic.Helpers;
using ValetParking.BusinessLogic.Interfaces;

namespace ValetParking.BusinessLogic.Dependency
{
    public static class LogicDependency
    {
        public static void RegistryDependency(IServiceCollection services, IConfiguration configuration)
        {

            #region set dependency net core

           // PersistenceDependency.RegistryDependency(services, configuration);

            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordRecoveryManager, PasswordRecoveryManager>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IParkingSlotService, ParkingSlotService>();
            services.AddScoped<IReservationService, ReservationService>();
            #endregion
            }
    }
}
