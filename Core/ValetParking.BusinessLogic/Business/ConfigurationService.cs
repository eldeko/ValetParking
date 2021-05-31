using AutoMapper;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Dto;
using ValetParking.Models.Domain;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ValetParking.BusinessLogic.Business
{
   
    public class ConfigurationService : IConfigurationService
    {
       //private readonly IUnitOfWorkFactory _factory;

        private readonly IConfigurationRepository _configurationRepository;
        private readonly IMapper _mapper;

        public ConfigurationService(IConfigurationRepository configurationRepository, IMapper mapper)
        {
            _configurationRepository = configurationRepository;
            _mapper = mapper;
           // _factory = unitOfWorkFactory;
        }

        public void CreateConfiguration(ConfigurationDomain configurationDomain)
        {
            var configurationEntity = _mapper.Map<ConfigurationEntity>(configurationDomain);
            _configurationRepository.Add(configurationEntity);
        
        }

        public void DeleteConfiguration(ConfigurationDomain configurationDomain)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConfigurationEntity> GetAll()
        {
            var configs = _configurationRepository.GetAll();
           
            return configs;
        }   

        public ConfigurationEntity GetById(int configurationId)
        {
            var config = _configurationRepository.GetById(configurationId);
            return config;
        }

        public void UpdateConfiguration(ConfigurationDomain configurationDomain)
        {
            var configurationEntity = _mapper.Map<ConfigurationEntity>(configurationDomain);
            var configToUpdate = _configurationRepository.GetAll().Where(x => x.ConfigurationKey == configurationEntity.ConfigurationKey)?.First() ;
            if (configToUpdate == null) throw new Exception("ConfigKey Not Found");
            configToUpdate.JsonData = configurationDomain.JsonData;
            _configurationRepository.Update(configToUpdate);
        }
    }
}
