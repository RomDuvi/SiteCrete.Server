using ServiceStack.Data;
using ServiceStack.OrmLite;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Client.Repositories
{
    public class DiscoverRepository : BaseRepository<Discover>, IDiscoverRepository
    {
        private readonly IPictureRepository PictureRepository;

        public DiscoverRepository(IDbConnectionFactory connectionFactory, IPictureRepository pictureRepository) : base(connectionFactory)
        {
            PictureRepository = pictureRepository;
        }

        public override void Remove(Discover discover)
        {
            using (var connection = ConnectionFactory.Open())
            {
                foreach(var picture in connection.Select<Picture>(x => x.DiscoverId == discover.Id))
                {
                    PictureRepository.Remove(picture);
                }
                base.Remove(discover);
            }
        }
    }
}