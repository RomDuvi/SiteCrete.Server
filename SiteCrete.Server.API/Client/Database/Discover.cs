using System;
using ServiceStack.DataAnnotations;

namespace SiteCrete.Server.API.Client.Database
{
    [Alias("discover")]
    public class Discover : BaseModel
    {
        public string Title { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public string DistanceDuration { get; set; }
        public string DescriptionFr { get; set; }
        public string DescriptionEn { get; set; }
        public string UmapUrlFr { get; set; }
        public string UmapUrlEn { get; set; }
    }
}