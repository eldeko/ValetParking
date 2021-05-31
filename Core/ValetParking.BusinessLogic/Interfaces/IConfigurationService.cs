using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;

namespace ValetParking.BusinessLogic.Interfaces
{
    public interface IConfigurationService
    {
        IEnumerable<ConfigurationEntity> GetAll();
        ConfigurationEntity GetById(int configurationId);       
        void CreateConfiguration(ConfigurationDomain configurationDomain);
        void DeleteConfiguration(ConfigurationDomain configurationDomain);
        void UpdateConfiguration(ConfigurationDomain configurationDomain);
    }
}
