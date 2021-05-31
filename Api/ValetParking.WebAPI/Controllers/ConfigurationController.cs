namespace ValetParking.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using ValetParking.BusinessLogic.Interfaces;
    using ValetParking.Dto;
    using ValetParking.Models.Domain;
    using ValetParking.Persistence.Entities;
    using System.Collections.Generic;
    using System.Net;

    [Route("api/v{version:apiVersion}/configuration")]
    public class ConfigurationController : BaseApiController
    {

        private IConfigurationService _configurationService;
        private readonly IMapper _mapper;

        public ConfigurationController(IConfigurationService configurationService, IMapper mapper)
        {
            _configurationService = configurationService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IList<ConfigurationEntity>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            var configurations = _configurationService.GetAll();
           // var configurationDto = _mapper.Map<IList<ConfigurationDto>>(configurations);

            return Ok(configurations);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ConfigurationEntity> GetById(int id)
        {
           return _configurationService.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] ConfigurationDto configuration)
        {
            var configurationDomain = _mapper.Map<ConfigurationDomain>(configuration);
            _configurationService.CreateConfiguration(configurationDomain);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] ConfigurationDto configuration)
        {
            var configurationDomain = _mapper.Map<ConfigurationDomain>(configuration);
            _configurationService.UpdateConfiguration(configurationDomain);
            }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}