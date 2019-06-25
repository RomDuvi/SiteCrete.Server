using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationRepository Repository { get; set; }
        public ReservationController(IReservationRepository repository)
        {
            Repository = repository;
        }

        #region GET
        [HttpGet]
        public IActionResult Get()
        {
            var reservations = Repository.GetAll();
            return Ok(reservations);
        }

        [HttpGet]
        [Route("{reservationId}")]
        public IActionResult GetReservationById(Guid reservationId)
        {
            var reservation = Repository.GetById(reservationId);
            return Ok(reservation);
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult SaveReservation([FromBody] Reservation reservation)
        {
            var r = Repository.Add(reservation);
            return Ok(r);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteReservation([FromBody] Reservation reservation)
        {
            Repository.Remove(reservation);
            return Ok();
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult UpdateReservation([FromBody] Reservation reservation)
        {
            var r = Repository.Update(reservation);
            return Ok(r);
        }
        #endregion
    }
}