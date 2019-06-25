using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;
using SiteCrete.Server.API.Client.Repositories;
using Newtonsoft.Json;

namespace SiteCrete.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private IPictureRepository Repository{ get; set; }
        public PictureController(IPictureRepository repository)
        {
            Repository = repository;
        }

        #region GET
        [HttpGet]
        public IActionResult Get()
        {
            var pictures = Repository.GetAll();

            return Ok(pictures);
        }

        [HttpGet]
        [Route("{pictureId}")]
        public IActionResult GetById(Guid id)
        {
            var pictures = Repository.GetById(id);

            return Ok(pictures);
        }

        [HttpGet]
        [Route("file/{pictureId}")]
        public IActionResult GetPictureFile(Guid pictureId)
        {
            var res = Repository.GetPictureFile(pictureId);

            return Ok(res);
        }

        [HttpGet]
        [Route("thumb/{pictureId}")]
        public IActionResult GetThumbFile(Guid pictureId)
        {
            var res = Repository.GetPictureThumbFile(pictureId);

            return Ok(res);
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public IActionResult GetPicturesByCategory(Guid categoryId)
        {
            var res = Repository.GetPicturesByCategory(categoryId);
            return Ok(res);
        }

        [HttpGet]
        [Route("discover/{discoverId}")]
        public IActionResult GetPicturesByDiscover(Guid discoverId)
        {
            var res = Repository.GetPicturesForDiscover(discoverId);
            return Ok(res);
        }
        
        #endregion
        #region POST
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult SavePicture()
        {
            var files = Request.Form.Files;
            var file = files[0];
            if (file.Length == 0)
            {
                return BadRequest();
            }

            var picture = JsonConvert.DeserializeObject<Picture>(Request.Form["picture"]);
            picture.File = file;
            picture.FileName = file.FileName;
            picture.Type = file.ContentType;
            var p = Repository.Add(picture);
            
            return Ok(p);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeletePicture([FromBody]Picture picture)
        {
            Repository.Remove(picture);
            return Ok(picture);
        }

        #endregion
        #region PUT
        [HttpPut]
        public IActionResult UpdatePicture([FromBody]Picture picture)
        {
            var res = Repository.Update(picture);
            return Ok(res);
        }

        #endregion
    }
}