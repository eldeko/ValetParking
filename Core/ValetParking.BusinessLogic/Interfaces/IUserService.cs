using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;

namespace ValetParking.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        UserEntity Authenticate(string username, string password);
        bool CreateUser(UserRegisterEntity userDomain);
        List<UserDomain> GetAll();
        bool IsExistingUser(string Email);    
        void ChangePasswordByReset(string email, string password);
        UserDomain GetUserByEmail(string email);
    }
}
