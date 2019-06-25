using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;
using SiteCrete.Server.API.Client.Repositories;
using Newtonsoft.Json;
using SiteCrete.Server.API.Client.Models;

namespace SiteCrete.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private ITextRepository Repository{ get; set; }
        public TextController(ITextRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        [Route("{textId}/{lang}")]
        public IActionResult GetById(string textId, string lang)
        {
            var text = Repository.GetTextById(textId, lang);

            return Ok(text);
        }
        
        #region POST
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult SaveText([FromBody] TextModel text)
        {
            var p = Repository.SaveText(text);
            
            return Ok(p);
        }
        #endregion
    }
}