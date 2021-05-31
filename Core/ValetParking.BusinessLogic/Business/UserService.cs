using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ValetParking.BusinessLogic.Helpers;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ValetParking.BusinessLogic.MappingExtensions;
using Validator = ValetParking.BusinessLogic.Helpers.Validator;

namespace ValetParking.BusinessLogic.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPasswordRecoveryRepository _passRepo;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings,
                           IPasswordRecoveryRepository passRepo, IVehicleRepository vehicleRepository)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
            _passRepo = passRepo;
            _vehicleRepository = vehicleRepository;
        }

        public bool IsExistingUser(string email)
        {
            if
                (!Validator.IsValidEmail(email))
            {
                throw new Exception("Invalid email format");
            }

            if (_userRepository.GetAll().Any(x => x.Email == email))
                return true;

            return false;
        }

        public UserEntity Authenticate(string username, string password)
        {
            if (!Helpers.Validator.IsValidEmail(username))
                throw new Exception("Invalid email format");

            var user = _userRepository.GetAll().Where(x => x.Email == username)?.First();
           
            if (user == null) return null;

            if (!VerifyPasswordHash(password, user.Hash, user.Salt)) return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public bool CreateUser(UserRegisterEntity user)
        {
            if (IsExistingUser(user.Email))
                return false;

            var userEntity = user.MapToUserEntity();

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            userEntity.Hash = passwordHash;
            userEntity.Salt = passwordSalt;

            _userRepository.Add(userEntity);

            if (user.Vehicles?.Count != 0)
            {                
                foreach (VehicleDto vehicle in user.Vehicles)
                {
                    CreateUserVehicles(vehicle, user.Email);
                }
            }

            return true;
        }

        public List<UserDomain> GetAll()
        {
            var userEntities = _userRepository.GetAll().ToList();
            var userDomainList = userEntities.MapToUserUserDomainList();

            return userDomainList;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }

        public UserDomain GetUserByEmail(string email)
        {
            var userEntity = _userRepository.GetAll().Where(x => x.Email == email)?.First();
            
            var userDomain = userEntity.MapToUserDomain();
            return userDomain;
        }

        public void ChangePasswordByReset(string email, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            UserEntity userToChangePassword = _userRepository.GetAll().Where(x => x.Email == email)?.First();

            userToChangePassword.Hash = passwordHash;
            userToChangePassword.Salt = passwordSalt;

            _userRepository.Update(userToChangePassword);
        }

        private void CreateUserVehicles(VehicleDto vehicle, string userEmail)
        {
            var vehicleEntity = vehicle.MapToVehicleEntity();

            var userToAddVehicle = _userRepository.GetAll().Where(x => x.Email == userEmail)?.First();            
            userToAddVehicle.Vehicles.Add(vehicleEntity);
        }
    }
}
