using System;
using SiteCrete.Server.API.Client.Database;

namespace SiteCrete.Server.API.Client.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
         byte[] GetCategoryFile(Guid categoryId);
    }
}