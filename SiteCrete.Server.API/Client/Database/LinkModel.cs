using ServiceStack.DataAnnotations;

namespace SiteCrete.Server.API.Client.Database
{
    [Alias("links")]
    public class LinkModel : BaseModel
    {
        public string NameFr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionFr { get; set; }
        public string DescriptionEn { get; set; }
        public string Link { get; set; }
    }
}