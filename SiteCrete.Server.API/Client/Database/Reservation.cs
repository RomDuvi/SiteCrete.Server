using System;
using ServiceStack.DataAnnotations;

namespace SiteCrete.Server.API.Client.Database
{
    [Alias("reservations")]
    public class Reservation : BaseModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public ReservationType Type { get; set; }
        public String Name { get; set; }
    }

    public enum ReservationType {
        EntireVilla = 0,
        Upstairs = 1,
        DownStairs1 = 2,
        DownStairs2 = 3
    }
}