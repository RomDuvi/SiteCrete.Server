using System;
using ServiceStack.DataAnnotations;

namespace SiteCrete.Server.API.Client.Database
{
    [Alias("gold_comments")]
    public class GoldComment : BaseModel 
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime StayStartDate { get; set; }
        public DateTime StayEndDate { get; set; }
        public int Evaluation { get; set; }
        public string Answer { get; set; }
    }
}