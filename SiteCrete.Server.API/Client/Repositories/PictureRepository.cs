using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SiteCrete.Server.API.Client.Database;
using SiteCrete.Server.API.Client.Interfaces;

namespace SiteCrete.Server.API.Client.Repositories
{
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;

        public PictureRepository(IDbConnectionFactory connectionFactory, ICategoryRepository categoryRepository, IConfiguration configuration) : base(connectionFactory)
        {
            _categoryRepository = categoryRepository;
            _configuration = configuration;
        }
        
        #region ADD
        public override Picture Add(Picture picture)
        {
            picture.Id = Guid.NewGuid();
            using (var connection = ConnectionFactory.Open())
            {
                var lastPic = connection.Select<Picture>().Where(x => x.CategoryId == picture.CategoryId).OrderByDescending(p => p.Order).FirstOrDefault();
                if (lastPic == null)
                {
                    picture.Order = 1;
                } 
                else 
                {
                    picture.Order = lastPic.Order + 1;
                }
            }
            
            var date = DateTime.Now;

            picture.Path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Pictures", date.Ticks + "_" + picture.FileName);
            picture.ThumbPath = CreateThumbnail(picture);

            using(var outStream = File.Create(picture.Path))
            {
                picture.File.CopyTo(outStream);
                var image = new Bitmap(outStream);
            }
            return base.Add(picture);            
        }

        private string CreateThumbnail(Picture picture)
        {
            var photoByte = picture.File;
            var path = Regex.Replace(picture.Path, @"(\.[\w\d_-]+)$", "_thumb$1");

            
            using(var inStream = new MemoryStream())
            using(var outStream = File.Create(path))
            {
                picture.File.CopyTo(inStream);
                var img = Image.FromStream(inStream);
                var nWidth = img.Width/7;
                var nHeight = img.Height/7;
                
                inStream.Position = 0;
                //Resize
                using (Image dest = new Bitmap(nWidth, nHeight))
                {
                    Graphics graphic = Graphics.FromImage(dest);
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;  
                    graphic.SmoothingMode = SmoothingMode.HighQuality;  
                    graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;  
                    graphic.CompositingQuality = CompositingQuality.HighQuality;
                    graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(img, 0, 0, nWidth, nHeight);
                    dest.Save(outStream, ImageFormat.Png);
                }
            }
            return path;
        }

        #endregion


        #region UPDATE

        public override Picture Update(Picture entity) 
        {
            using(var connection = ConnectionFactory.Open()) 
            {
                var currentOrder = connection.Select<Picture>().Where(x => x.Id == entity.Id).Select(x => x.Order).First();
                var newOrder = entity.Order;
                var query = "";

                if(currentOrder > newOrder) 
                {
                    query = $@"
                                UPDATE pictures p
                                SET p.`order` = p.`order` + 1
                                WHERE p.categoryId = '{entity.CategoryId.ToString()}'
                                AND p.`order` >= {newOrder}
                                AND p.`order` < {currentOrder}
                            ";
                } 
                else if (currentOrder < newOrder)
                {
                    query = $@"
                                UPDATE pictures p
                                SET p.`order` = p.`order` - 1
                                WHERE p.categoryId = '{entity.CategoryId.ToString()}'
                                AND p.`order` <= {newOrder}
                                AND p.`order` > {currentOrder}
                            ";
                }

                if (currentOrder != newOrder)
                {
                    connection.ExecuteSql(query);
                }
            }
            return base.Update(entity);
        }

        #endregion

        public byte[] GetPictureFile(Guid pictureId)
        {
            var picture = GetById(pictureId);
            if(picture == null)
            {
                throw new ArgumentException("Can't find picture");
            }
            return File.ReadAllBytes(picture.Path);
        }

        public byte[] GetPictureThumbFile(Guid pictureId)
        {
            var picture = GetById(pictureId);
            if(picture == null)
            {
                throw new ArgumentException("Can't find picture");
            }
            return File.ReadAllBytes(picture.ThumbPath);
        }

        public override void Remove(Picture p)
        {
            var picture = GetById(p.Id);
            if(picture == null)
            {
                throw new ArgumentException("Can't find picture");
            }

            using (var connection = ConnectionFactory.Open())
            {
                connection.DeleteById<Picture>(picture.Id);
                File.Delete(picture.Path);

                if (p.CategoryId != null)
                {
                    connection.ExecuteSql($@"UPDATE pictures p
                                        SET p.order = p.order - 1
                                        WHERE p.order > {p.Order} 
                                        AND p.categoryId = '{p.CategoryId.ToString()}'");
                }
            }
        }

        public IEnumerable<Picture> GetPicturesByCategory(Guid categoryId)
        {
            using (var connection = ConnectionFactory.Open())
            {
                var pictures = connection.Select<Picture>(x => x.CategoryId == categoryId);
                return pictures;
            }
        }

        public IEnumerable<Picture> GetPicturesForDiscover(Guid discoverId)
        {
            using(var connection = ConnectionFactory.Open())
            {
                return connection.Select<Picture>(x => x.DiscoverId == discoverId);
            }
        }
    }
}