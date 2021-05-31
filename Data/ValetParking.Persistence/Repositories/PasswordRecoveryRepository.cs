using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Persistence.Repositories
{
    public class PasswordRecoveryRepository : Repository<PasswordRecoveryEntity>, IPasswordRecoveryRepository
    {
        public PasswordRecoveryRepository(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }
    }
}
