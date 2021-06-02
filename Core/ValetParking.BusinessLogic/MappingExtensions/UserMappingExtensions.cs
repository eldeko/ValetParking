
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using System.Collections.Generic;

namespace ValetParking.BusinessLogic.MappingExtensions
{
    public static class UserMappingExtensions
    {
        public static List<UserEntity> MapToUserEntityList(this List<UserRegisterEntity> userRegisterEntityList)
        {
            var userEntityList = new List<UserEntity>();

            foreach (UserRegisterEntity userRegisterEntity in userRegisterEntityList)
            {
                userEntityList.Add(userRegisterEntity.MapToUserEntity());
            }

            return userEntityList;
        }    

        public static UserEntity MapToUserEntity(this UserRegisterEntity userRegisterEntity)
        {
            return new UserEntity
            {
                Email = userRegisterEntity.Email,
                IsActive = userRegisterEntity.IsActive,
                HasDisability = userRegisterEntity.HasDisability,
                Hash = null,
                Name = userRegisterEntity.Name,
                Password = userRegisterEntity.Password,
                Phone = userRegisterEntity.Phone,
                Pin = userRegisterEntity.Pin,
                PinType = userRegisterEntity.PinType,
                Salt = null,
                Surname = userRegisterEntity.Surname,
                Token = null,
                UserType = userRegisterEntity.UserType,
                Vehicles = userRegisterEntity.Vehicles.MapToVehicleEntityList()
            };
        }

        public static List<UserDto> MapToUserDtoList(this List<UserDomain> userDomainList)
        {
            var userDtoList = new List<UserDto>();

            foreach (UserDomain userDomain in userDomainList)
            {
                userDtoList.Add(userDomain.MapToUserDto());
            }

            return userDtoList;
        }

        public static UserDto MapToUserDto (this UserDomain userDomain)
        {
            return new UserDto
            {
                Email = userDomain.Email,
                IsActive = userDomain.IsActive,
                HasDisability = userDomain.HasDisability,
                Name = userDomain.Name,
                Pin = userDomain.Pin,
                PinType = userDomain.PinType.ConvertEnum<Dto.Enums.PinTypes>(),
                Surname = userDomain.Surname,
                UserType = userDomain.UserType.ConvertEnum<Dto.Enums.UserTypes>()
            };
        }

        public static List<UserDomain> MapToUserUserDomainList(this List<UserEntity> userEntityList)
        {
            var userDomainList = new List<UserDomain>();

            foreach (UserEntity userEntity in userEntityList)
            {
                userDomainList.Add(userEntity.MapToUserDomain());
            }

            return userDomainList;
        }

        public static UserDomain MapToUserDomain(this UserEntity userEntity)
        {
            return new UserDomain
            {
                IsActive = userEntity.IsActive,
                Email = userEntity.Email,
                HasDisability = userEntity.HasDisability,
                Name = userEntity.Name,
                Phone = userEntity.Phone,
                Pin = userEntity.Pin,
                PinType = userEntity.PinType,
                Surname = userEntity.Surname,
                Token = userEntity.Token,
                UserType = userEntity.UserType
            };
        }
        public static LoggedUserDto MapToLoggedUserDto (this UserEntity userEntity)
        {
            return new LoggedUserDto
            {               
                Email = userEntity.Email,            
                FullName = userEntity.Name + " " + userEntity.Surname,               
                Token = userEntity.Token,
                AvatarURL = userEntity.AvatarURL,
                UserType = userEntity.UserType.ConvertEnum<Dto.Enums.UserTypes>()
            };
        }
    }
}

