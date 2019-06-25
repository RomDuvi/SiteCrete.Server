using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {

        private ILinkRepository Repository{get;set;}
        public LinkController(ILinkRepository repository){
            Repository = repository;
        }

        #region GET
        [HttpGet]
        public IActionResult GetAll()
        {
            var links = Repository.GetAll();
            return Ok(links);
        }

        [HttpGet]
        [Route("{linkId}")]
        public IActionResult GetById(Guid linkId)
        {
            var link = Repository.GetById(linkId);
            return Ok(link);
        }

        #endregion        
        #region POST
        [HttpPost]
        public IActionResult Add([FromBody] LinkModel link) 
        {
            var c = Repository.Add(link);
            return Ok(c);    
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] LinkModel link)
        {
            Repository.Remove(link);
            return Ok(link);
        }
        
        #endregion
        #region PUT
        [HttpPut]
        public IActionResult UpdateLink([FromBody] LinkModel link)
        {
            var c = Repository.Update(link);
            return Ok(c);
        }
        #endregion
    }
}