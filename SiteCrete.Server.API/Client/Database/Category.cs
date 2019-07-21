using Microsoft.AspNetCore.Http;
using ServiceStack.DataAnnotations;

namespace SiteCrete.Server.API.Client.Database
{
    [Alias("categories")]
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        [Ignore]
        public IFormFile File { get; set; }

    }
}