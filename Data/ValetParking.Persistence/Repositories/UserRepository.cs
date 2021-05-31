using Microsoft.EntityFrameworkCore;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Persistence.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        public UserRepository(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<UserEntity> GetAllWithVehicles()
        {
            return _unitOfWorkFactory.Current.Context.Users.Include("Vehicles");
        }
    }
}
