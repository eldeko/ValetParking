using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;

namespace ValetParking.Persistence.Repositories
{
    public class ConfigurationRepository : Repository<ConfigurationEntity>, IConfigurationRepository
    {
        public ConfigurationRepository(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }
    }
}
