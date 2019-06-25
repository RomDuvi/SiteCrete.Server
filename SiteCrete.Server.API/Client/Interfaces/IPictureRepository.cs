using System;
using System.Collections.Generic;
using SiteCrete.Server.API.Client.Database;

namespace SiteCrete.Server.API.Client.Interfaces
{
    public interface IPictureRepository : IBaseRepository<Picture>
    {
        byte[] GetPictureFile(Guid pictureId);
        byte[] GetPictureThumbFile(Guid pictureId);
        IEnumerable<Picture> GetPicturesByCategory(Guid categoryId);
        IEnumerable<Picture> GetPicturesForDiscover(Guid categoryId);
    }
}