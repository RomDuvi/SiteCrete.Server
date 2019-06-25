using ServiceStack.Data;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Client.Repositories
{
    public class LinkRepository : BaseRepository<LinkModel>, ILinkRepository
    {
        public LinkRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}