using ValetParking.Persistence.Entities;
using System.Collections.Generic;

namespace ValetParking.Persistence.Repositories.Contracts
    {
    public interface IUserRepository : IRepository<UserEntity>
    {
      IEnumerable<UserEntity> GetAllWithVehicles();
    }
}
