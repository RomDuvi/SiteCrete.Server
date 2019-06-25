using System;
using ServiceStack.Data;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Client.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {

        public ReservationRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
    }
}