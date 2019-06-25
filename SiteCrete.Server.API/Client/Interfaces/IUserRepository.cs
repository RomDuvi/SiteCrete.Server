using SiteCrete.Server.API.Client.Database;

namespace SiteCrete.Server.API.Client.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
         User Login(User user);
         User GetUserByUsername(string username);
    }
}