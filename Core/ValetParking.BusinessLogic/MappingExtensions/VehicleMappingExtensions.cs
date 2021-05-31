using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using System.Collections.Generic;

namespace ValetParking.BusinessLogic.MappingExtensions
{
    public static class VehicleMappingExtensions
    {
        public static List<VehicleDto> MapToVehicleDtoList(this List<VehicleDomain> vehicleDomainList)
        {
            var vehicleDtoList = new List<VehicleDto>();

            foreach (var vehicleDomain in vehicleDomainList)
            {
                vehicleDtoList.Add(vehicleDomain.MapToVehicleDto());
            }

            return vehicleDtoList;
        }

        public static VehicleDto MapToVehicleDto (this VehicleDomain vehicleDomain)
        {
            return new VehicleDto
            {
                IsActive = vehicleDomain.IsActive,
                Brand = vehicleDomain.Brand,
                Color = vehicleDomain.Color,
                LicensePlate = vehicleDomain.LicensePlate,
                Model = vehicleDomain.Model,
                VehicleType = vehicleDomain.VehicleType.ConvertEnum<Dto.Enums.VehicleTypes>()
            };
        }

        public static List<VehicleEntity> MapToVehicleEntityList(this List<VehicleDto> vehicleDtoList)
        {
            var vehicleEntityList = new List<VehicleEntity>();

            foreach (var vehicleDto in vehicleDtoList)
            {
                vehicleEntityList.Add(vehicleDto.MapToVehicleEntity());
            }

            return vehicleEntityList;
        }

        public static VehicleEntity MapToVehicleEntity(this VehicleDto vehicleDto)
        {
            return new VehicleEntity
            {
                IsActive = vehicleDto.IsActive,
                Brand = vehicleDto.Brand,
                Color = vehicleDto.Color,
                LicensePlate = vehicleDto.LicensePlate,
                Model = vehicleDto.Model,
                VehicleType = vehicleDto.VehicleType.ConvertEnum<Persistence.Enums.VehicleTypes>()                
            };
        }

        public static List<VehicleEntity> MapToVehicleEntityList(this List<VehicleDomain> vehicleDomainList)
        {
            var vehicleEntityList = new List<VehicleEntity>();

            foreach (var vehicleDto in vehicleDomainList)
            {
                vehicleEntityList.Add(vehicleDto.MapToVehicleEntity());
            }

            return vehicleEntityList;
        }

        public static VehicleEntity MapToVehicleEntity(this VehicleDomain vehicleDomain)
        {
            return new VehicleEntity
            {
                IsActive = vehicleDomain.IsActive,
                Brand = vehicleDomain.Brand,
                Color = vehicleDomain.Color,
                LicensePlate = vehicleDomain.LicensePlate,
                Model = vehicleDomain.Model,
                VehicleType = vehicleDomain.VehicleType.ConvertEnum<Persistence.Enums.VehicleTypes>()
            };
        }

        public static List<VehicleDomain> MapToVehicleDomainList(this List<VehicleEntity> vehicleEntityList)
        {
            var vehicleDomainList = new List<VehicleDomain>();

            foreach (var vehicleEntity in vehicleEntityList)
            {
                vehicleDomainList.Add(vehicleEntity.MapToVehicleDomain());
            }

            return vehicleDomainList;
        }

        public static VehicleDomain MapToVehicleDomain(this VehicleEntity vehicleEntity)
        {
            return new VehicleDomain
            {
                IsActive = vehicleEntity.IsActive,
                Brand = vehicleEntity.Brand,
                Color = vehicleEntity.Color,
                LicensePlate = vehicleEntity.LicensePlate,
                Model = vehicleEntity.Model,
                VehicleType = vehicleEntity.VehicleType.ConvertEnum<Persistence.Enums.VehicleTypes>()
            };
        }

        public static List<VehicleDomain> MapToVehicleDomainList(this List<VehicleDto> vehicleDtoList)
        {
            var vehicleDomainList = new List<VehicleDomain>();

            foreach (var vehicleDto in vehicleDtoList)
            {
                vehicleDomainList.Add(vehicleDto.MapToVehicleDomain());
            }

            return vehicleDomainList;
        }

        public static VehicleDomain MapToVehicleDomain(this VehicleDto vehicleDto)
        {
            return new VehicleDomain
            {
                IsActive = vehicleDto.IsActive,
                Brand = vehicleDto.Brand,
                Color = vehicleDto.Color,
                LicensePlate = vehicleDto.LicensePlate,
                Model = vehicleDto.Model,
                VehicleType = vehicleDto.VehicleType.ConvertEnum<Persistence.Enums.VehicleTypes>()
            };
        }
    }
}
