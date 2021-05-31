using AutoMapper;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using ValetParking.BusinessLogic.MappingExtensions;

namespace ValetParking.BusinessLogic.Business
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;       

        public VehicleService(IVehicleRepository vehicleRepository, IUserRepository userRepository)
        {
            _vehicleRepository = vehicleRepository;
            _userRepository = userRepository;           
        }

        public List<VehicleDomain> GetAllByUserId(string email)
        {
            var user = _userRepository.GetAllWithVehicles().Where(x => x.Email == email)?.First();
            var userVehicles = user.Vehicles;
            var userVehiclesToDomain = userVehicles.MapToVehicleDomainList();

            return userVehiclesToDomain;
        }

        public void CreateVehicle(VehicleDto vehicle, string userEmail)
        {
            var vehicleDomain = vehicle.MapToVehicleDomain();

            var vehicleEntity = vehicleDomain.MapToVehicleEntity();
            _vehicleRepository.Add(vehicleEntity); 
        }
    }
}
