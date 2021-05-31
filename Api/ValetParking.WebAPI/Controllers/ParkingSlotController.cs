using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.Dto;
using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ValetParking.WebApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/parkingSlot")]
    public class ParkingSlotController : BaseApiController
    {
        private readonly IParkingSlotService _parkingSlotService;
        private readonly IMapper _mapper;

        public ParkingSlotController(IParkingSlotService parkingSlotService, IMapper mapper)
        {
            _parkingSlotService = parkingSlotService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("GetFreeFor24Hs")]
        [ProducesResponseType(typeof(IList<ParkingSlotEntity>), (int)HttpStatusCode.OK)]
        public IActionResult GetCurrentFreeParkingSlots()
        {
            var currentTime = DateTime.Now;
            var tillNExtDay = currentTime.AddHours(24);

            var todayFreeSlots = _parkingSlotService.GetFreeParkingSlotsByDate(currentTime, tillNExtDay);
            return Ok(new List<ParkingSlotEntity>());
        }

        [AllowAnonymous]
        [HttpGet("GetAllSlots")]
        [ProducesResponseType(typeof(IList<ParkingSlotDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllParkingSlots()
        {
            var allSlots = _parkingSlotService.GetAll();
            var allSlotsDto = _mapper.Map<IList<ParkingSlotDto>>(allSlots);

            return Ok(allSlotsDto);
        }

        [AllowAnonymous]
        [HttpGet("GetCompleteParkingLot")]
        [ProducesResponseType(typeof(IList<ParkingSlotDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetCompleteParkingLot()
        {
            var completeParkingLot = _parkingSlotService.GetAll();
            var allSlotsDto = _mapper.Map<IList<ParkingSlotDto>>(completeParkingLot);

            return Ok(allSlotsDto);
        }



        [AllowAnonymous]
        [HttpGet("GetReservedSlots")]
        [ProducesResponseType(typeof(List<ParkingSlotDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetReservedSlots(DateTime fromDate, DateTime toDate)
        {
            var reservedParkingSlots = _parkingSlotService.GetReservedParkingSlotsByDate(fromDate, toDate);

            var responseDto = _mapper.Map<List<ParkingSlotDto>>(reservedParkingSlots);

            return Ok(responseDto);
        }

        //[AllowAnonymous]
        //[HttpPost("GetFreeParkingSlotsByHour")]
        //[ProducesResponseType(typeof(IList<ParkingSlotEntity>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.BadRequest)]
        //public IActionResult GetFreeParkingSlotsByHour([FromBody] ParkingSearchParamsDto parkingSerachParams)
        //{
        //    IList<string> validationMessages = parkingSerachParams.Validate();
        //    if (validationMessages.Count > 0)
        //    {
        //        return BadRequest(validationMessages);
        //    }

        //    List<(DateTime, DateTime)> dateTimeRanges = GetDateTimeRanges(parkingSerachParams.Day, parkingSerachParams.HourRanges).ToList();
        //    var dayFreeSlots = _parkingSlotService.GetFreeParkingSlotsByHour(dateTimeRanges);

        //    var allSlotsDto = _mapper.Map<IList<ParkingSlotDto>>(dayFreeSlots);

        //    return Ok(allSlotsDto);
        //}

        [AllowAnonymous]
        [HttpGet("GetFreeParkingSlotsByDay")]
        [ProducesResponseType(typeof(IList<ParkingSlotEntity>), (int)HttpStatusCode.OK)]
        public IActionResult GetFreeParkingSlotsByDay(DateTime day)
        {
            DateTime toDate = new DateTime(day.Year, day.Month, day.Day, 23, 59, 59);
            List<(DateTime, DateTime)> dateTimeRanges = new List<(DateTime, DateTime)> { (day.Date, toDate) };
            var dayFreeSlots = _parkingSlotService.GetFreeParkingSlotsByHour(dateTimeRanges);

            var allSlotsDto = _mapper.Map<IList<ParkingSlotDto>>(dayFreeSlots);

            return Ok(allSlotsDto);
        }

        private (int hour, int minutes) GetHourAndMinutes(string time)
        {
            int hour = int.Parse(time.Split(':').FirstOrDefault());
            int minutes = int.Parse(time.Split(':').LastOrDefault());

            return (hour, minutes);
        }
    }
}