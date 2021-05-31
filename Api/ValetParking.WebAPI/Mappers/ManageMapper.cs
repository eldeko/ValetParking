using AutoMapper;

namespace ValetParking.WebApi.Mappers
{
    public class ManageMapper
    {
        /// <summary>
        /// Set up all mapper that application needs
        /// </summary>
        /// <returns></returns>
        public static IMapper SetupMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ConfigurationMapper()); // mappers DTO
                mc.AddProfile(new BusinessLogic.Mappers.ConfigurationMapper()); // mappers Domain
                mc.AddProfile(new UserMapper()); // mappers DTO
                mc.AddProfile(new BusinessLogic.Mappers.UserMapper()); // mappers Domain
                mc.AddProfile(new BusinessLogic.Mappers.VehicleMapper());
                mc.AddProfile(new VehicleMapper());
                mc.AddProfile(new BusinessLogic.Mappers.ParkingSlotMapper());
                mc.AddProfile(new ParkingSlotMapper());
                mc.AddProfile(new BusinessLogic.Mappers.ReservationMapper());
                mc.AddProfile(new ReservationMapper());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
