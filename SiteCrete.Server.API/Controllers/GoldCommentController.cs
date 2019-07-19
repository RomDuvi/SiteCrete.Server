using System;
using Microsoft.AspNetCore.Mvc;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class GoldCommentController : ControllerBase
    {
        private IGoldCommentRepository Repository { get; set; }
        public GoldCommentController(IGoldCommentRepository repository) 
        {
            Repository = repository;
        }

        #region GET
        [HttpGet]
        public IActionResult GetAll()
        {
            var comments = Repository.GetAll();
            return Ok(comments);
        }

        [HttpGet]
        [Route("{linkId}")]
        public IActionResult GetById(Guid commentId)
        {
            var link = Repository.GetById(commentId);
            return Ok(link);
        }

        [HttpGet]
        [Route("evaluationAverage")]
        public IActionResult GetEvalutaionAverage()
        {
            var average = Repository.GetEvaluationAverage();
            return Ok(average);
        }

        #endregion        
        #region POST
        [HttpPost]
        public IActionResult Add([FromBody] GoldComment comment) 
        {
            var c = Repository.Add(comment);
            return Ok(c);    
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] GoldComment comment)
        {
            Repository.Remove(comment);
            return Ok(comment);
        }
        
        #endregion
        #region PUT
        [HttpPut]
        public IActionResult Update([FromBody] GoldComment comment)
        {
            var c = Repository.Update(comment);
            return Ok(c);
        }
        #endregion
    }
}