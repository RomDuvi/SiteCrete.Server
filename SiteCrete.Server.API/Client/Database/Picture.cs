using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using ServiceStack.DataAnnotations;

namespace SiteCrete.Server.API.Client.Database
{
    [Alias("pictures")]
    public class Picture : BaseModel
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string ThumbPath { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        [ForeignKey(typeof(Discover))]
        public Guid? DiscoverId { get; set; }
        [ForeignKey(typeof(Category))]
        public Guid? CategoryId { get; set; }
        [Ignore]
        public string FileName { get; set; }
        [Ignore]
        public IFormFile File { get; set; }
        
    }
}