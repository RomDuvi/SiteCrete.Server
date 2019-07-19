using System.Linq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Client.Repositories
{
    public class GoldCommentRepository : BaseRepository<GoldComment>, IGoldCommentRepository
    {
        public GoldCommentRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public int GetEvaluationAverage() 
        {
            using(var connection = ConnectionFactory.Open()) 
            {
                var comments = connection.Select<GoldComment>();
                var average = comments.Select(x => x.Evaluation).Sum() / comments.Count;
                return average;
            }
        }
    }
}