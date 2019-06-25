using System;
using Microsoft.AspNetCore.Mvc;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscoverController : ControllerBase
    {
        private IDiscoverRepository Repository { get; set; }

        public DiscoverController(IDiscoverRepository repository)
        {
            Repository = repository;
        }

        #region GET
        [HttpGet]
        public IActionResult Get()
        {
            var discovers = Repository.GetAll();
            return Ok(discovers);
        }

        [HttpGet]
        [Route("{discoverId}")]
        public IActionResult GetDiscoverById(Guid discoverId)
        {
            var discover = Repository.GetById(discoverId);
            return Ok(discover);
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult SaveDiscover([FromBody] Discover discover)
        {
            var r = Repository.Add(discover);
            return Ok(r);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteDiscover([FromBody] Discover discover)
        {
            Repository.Remove(discover);
            return Ok();
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult UpdateDiscover([FromBody] Discover discover)
        {
            var r = Repository.Update(discover);
            return Ok(r);
        }
        #endregion
    }
}