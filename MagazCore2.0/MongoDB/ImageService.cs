using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MagazCore2._0.MongoDB
{
    public class ImageService
    {
        IGridFSBucket gridFS;
        IMongoCollection<Image> Images; // коллекция в базе данных


        public ImageService()
        {
            // строка подключения
            string connectionString = "mongodb://192.168.4.130:27017/ComputerStore";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);
           Images = database.GetCollection<Image>("Images");
        }

        // получение изображения
        public async Task<byte[]> GetImage(string id)
        {
            var prikol = await Images.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
            byte[] result = await gridFS.DownloadAsBytesAsync(new ObjectId(prikol.ImageId));

            return result;
        }
        // сохранение изображения
        public async Task StoreImage(string id, Stream imageStream,string imagename)
        {
            
            Image i = new Image() { Id = id, ImageId = id };
            /*if (i.HasImage())
            {
               
                // если ранее уже была прикреплена картинка, удаляем ее
                await gridFS.DeleteAsync(new ObjectId(id));
            }*/
           
            await Images.InsertOneAsync(i);
            // сохраняем изображение
            ObjectId imageId = await gridFS.UploadFromStreamAsync(imagename,imageStream);
            i.ImageId = imageId.ToString();
            // обновляем данные по документу         
            var filter = Builders<Image>.Filter.Eq("_id", new ObjectId(i.Id));
            var update = Builders<Image>.Update.Set("ImageId", i.ImageId);
            await Images.UpdateOneAsync(filter, update);
        }
        public async Task DeleteImage(string id) {

            var prikol = await Images.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
             await gridFS.DeleteAsync(new ObjectId(prikol.ImageId));

        }



    }
}
