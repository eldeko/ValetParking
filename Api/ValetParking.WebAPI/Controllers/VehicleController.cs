using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Dto;
using System.Collections.Generic;
using System.Net;
using ValetParking.BusinessLogic.MappingExtensions;

namespace ValetParking.WebApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/vehicle")]
    public class VehicleController : BaseApiController
    {
        private readonly IVehicleService _vehicleService;
        
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;           
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<VehicleDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetVehiclesByUserId(string email)
        {
            var userVehicles = _vehicleService.GetAllByUserId(email);
            var dtoUserVehicles = userVehicles.MapToVehicleDtoList();
            return Ok(dtoUserVehicles);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateVehicle(VehicleDto vehicle, string email)
        {
            _vehicleService.CreateVehicle(vehicle, email);

            return Ok();
        }
    }
}