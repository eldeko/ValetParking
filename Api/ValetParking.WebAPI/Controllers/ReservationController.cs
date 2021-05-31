using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValetParking.BusinessLogic.Helpers;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Dto;
using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;

namespace ValetParking.WebApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/reservation")]
    public class ReservationController : BaseApiController
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet("GetUnavailableDays")]
        [ProducesResponseType(typeof(IList<DateTime>), (int)HttpStatusCode.OK)]
        public IActionResult GetUnavailableDaysForParking()
        {
            var unavailableParkingDays = _reservationService.GetUnavailableDaysForParking();
            var result = new { unavailableDays = unavailableParkingDays };

            return Ok(result);
        }

        //[AllowAnonymous]
        //[HttpGet("GetReservationsByUser")]
        //[ProducesResponseType(typeof(IList<ReservationDto>), (int)HttpStatusCode.OK)]
        //public IActionResult GetReservationsByUser(string email)
        //{
        //    var userReservations = _reservationService.GetReservationsByUser(email);
        //    var reservationsDto = _mapper.Map<IList<ReservationDto>>(userReservations);

        //    return Ok(reservationsDto);
        //}

        //[AllowAnonymous]
        //[HttpGet("GetReservationsByDateRange")]
        //[ProducesResponseType(typeof(IList<ReservationDto>), (int)HttpStatusCode.OK)]
        //public IActionResult GetReservationsByDateRange(DateTime fromDate, DateTime toDate)
        //{
        //    var reservations = _reservationService.GetReservationsByDateRange(fromDate, toDate);
        //    var reservationsDto = _mapper.Map<IList<ReservationDto>>(reservations);

        //    return Ok(reservationsDto);
        //}
    }
}