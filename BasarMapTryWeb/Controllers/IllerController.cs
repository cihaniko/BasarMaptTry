using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Xml.Serialization;
using BasarMapTryWeb.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace BasarMapTryWeb.Controllers
{
    public class IllerController : ApiController
    {
        private MongoClient client;
        private IMongoDatabase db;
        //private IMongoCollection<Il> mg;

        //[HttpGet]
        //public JsonResult<BsonDocument> GetAll(string IlAdi)
        //{
            
        //     var connectionString = "mongodb://127.0.0.1:27017";
        //     client = new MongoClient(connectionString);
        //     db = client.GetDatabase("test");
        //    //IMongoCollection<Il> mg = db.GetCollection<Il>("Iller");
        //    //List<Il> st = mg.Find(new BsonDocument()).ToList();

        //    //var list = await mg.Find(x => x.IlAdi == "Ankara").ToListAsync();

        //    IMongoCollection<BsonDocument> mg = db.GetCollection<BsonDocument>("Iller");
        //    var filter = Builders<BsonDocument>.Filter.Eq("IlAdi", IlAdi);
        //    //var result1 = await mg.FindAsync(new BsonDocument());
        //    var result = mg.Find(filter).ToBsonDocument();


        //    return


        //    //var result = await mg.Find(filter).ToListAsync();



        //}
        public string Get(Il param)
        {

            var connectionString = "mongodb://127.0.0.1:27017";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("test");
            //IMongoCollection<Il> mg = db.GetCollection<Il>("Iller");
            //List<Il> st = mg.Find(new BsonDocument()).ToList();

            //var list = await mg.Find(x => x.IlAdi == "Ankara").ToListAsync();
            IMongoCollection<BsonDocument> mg = db.GetCollection<BsonDocument>("Iller");
            var filter = Builders<BsonDocument>.Filter.Eq("IlAdi", param.IlAdi);

            //var result1 = await mg.FindAsync(new BsonDocument());


            var result =  mg.FindAsync(filter).ToString();

            return result;




            //var result = await mg.Find(filter).ToListAsync();



        }

        public async Task<Il> GetAll2(string IlAdi)
        {
            // var user = await collection.Find(x => x.UserName != userName).FirstAsync();

            var connectionString = "mongodb://127.0.0.1:27017";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("test");

            IMongoCollection<BsonDocument> mg = db.GetCollection<BsonDocument>("Iller");
            var filter = Builders<BsonDocument>.Filter.Eq("IlAdi", IlAdi);
            var result = await mg.FindAsync(filter);
            var sehir = new Il();

            while (await result.MoveNextAsync())
            {
                var listOfUsers = result.Current.ToList();
               
                sehir.Nufus = listOfUsers[0]["Nufus"].AsInt32;
                sehir.IlAdi = listOfUsers[0]["IlAdi"].AsString;
                sehir.Geo=new GeoPointBson()
                {
                    type = listOfUsers[0]["Geo"]["type"].AsString,
                    coordinates = listOfUsers[0]["Geo"]["coordinates"].AsBsonArray
                };


            }


           
            return sehir;

        }



    }

}
