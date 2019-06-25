using ServiceStack.Data;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Client.Repositories
{
    public class GoldCommentRepository : BaseRepository<GoldComment>, IGoldCommentRepository
    {
        public GoldCommentRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}