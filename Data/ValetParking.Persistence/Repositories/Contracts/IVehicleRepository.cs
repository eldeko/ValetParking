using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Persistence.Repositories.Contracts
{
    public interface IVehicleRepository : IRepository<VehicleEntity>
    {
    }
}