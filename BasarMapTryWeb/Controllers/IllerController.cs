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



        public async Task<Il> GetSearch(string IlAdi)
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
                sehir.Geo = new GeoPointBson()
                {
                    type = listOfUsers[0]["Geo"]["type"].AsString,
                    coordinates = listOfUsers[0]["Geo"]["coordinates"].AsBsonArray
                };


            }



            return sehir;

        }

        public async Task<List<Illergelsin>> GetAll()
        {
            // var user = await collection.Find(x => x.UserName != userName).FirstAsync();

            var connectionString = "mongodb://127.0.0.1:27017";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("test");

            IMongoCollection<BsonDocument> mg = db.GetCollection<BsonDocument>("Iller");


            var documents = await mg.Find(new BsonDocument()).ToListAsync();
            var cursor = await mg.Find(new BsonDocument()).ToCursorAsync();
            var sehir = new List<Illergelsin>();

            foreach (var document in documents)
            {
                var gonder = new Illergelsin();

                gonder.IlAdi = document["IlAdi"].AsString;
                gonder.IdariId = document["IdariId"].AsInt32;
                sehir.Add(gonder);


            }


            return sehir;
        }

        public async Task<List<Ilcegelsin>> GetIlce(int UstIdariId)
        {
            // var user = await collection.Find(x => x.UserName != userName).FirstAsync();

            var connectionString = "mongodb://127.0.0.1:27017";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("test");

            IMongoCollection<BsonDocument> mg = db.GetCollection<BsonDocument>("Ilceler");


            var filter = Builders<BsonDocument>.Filter.Eq("UstIdariId", UstIdariId);
            var result = await mg.Find(filter).ToListAsync();
            //var cursor = await mg.Find(new BsonDocument()).ToCursorAsync();
            var ilce = new List<Ilcegelsin>();

            foreach (var document in result)
            {
                var gonder = new Ilcegelsin();

                gonder.IlceAdi = document["IlceAdi"].AsString;
                gonder.IdariId = document["IdariId"].AsInt32;
                ilce.Add(gonder);


            }


            return ilce;
        }

        public async Task<List<Mahallegelsin>> GetMahalle(int UstIdariId)
        {
            // var user = await collection.Find(x => x.UserName != userName).FirstAsync();

            var connectionString = "mongodb://127.0.0.1:27017";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("test");

            IMongoCollection<BsonDocument> mg = db.GetCollection<BsonDocument>("Mahalleler");



            var filter = Builders<BsonDocument>.Filter.Eq("UstIdariId", UstIdariId);
            var result = await mg.Find(filter).ToListAsync();
            //var cursor = await mg.Find(new BsonDocument()).ToCursorAsync();
            var mahalle = new List<Mahallegelsin>();

            foreach (var document in result)
            {
                var gonder = new Mahallegelsin();

                gonder.Adi = document["Adi"].AsString;
                gonder.IdariId = document["IdariId"].AsInt32;
                mahalle.Add(gonder);


            }


            return mahalle;
        }

        public async Task<MahalleCiz> GetMahalleDraw(int IdariId)
        {
            var connectionString = "mongodb://127.0.0.1:27017";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("test");

            IMongoCollection<BsonDocument> mg = db.GetCollection<BsonDocument>("Mahalleler");
            var filter = Builders<BsonDocument>.Filter.Eq("IdariId", IdariId);
            var result = await mg.FindAsync(filter);
            var mahalle = new MahalleCiz();

            while (await result.MoveNextAsync())
            {
                var listOfUsers = result.Current.ToList();

                mahalle.Nufus = listOfUsers[0]["Nufus"].AsInt32;
                mahalle.IlAdi = listOfUsers[0]["IlAdi"].AsString;
                mahalle.IlceAdi = listOfUsers[0]["IlceAdi"].AsString;
                mahalle.Adi = listOfUsers[0]["Adi"].AsString;
                mahalle.AdiAdr = listOfUsers[0]["AdiAdr"].AsString;
                mahalle.Tip = listOfUsers[0]["Tip"].AsString;
                mahalle.PostaKodu = listOfUsers[0]["PostaKodu"].AsInt32;
                mahalle.Geo = new GeoPointBson()
                {
                    type = listOfUsers[0]["Geo"]["type"].AsString,
                    coordinates = listOfUsers[0]["Geo"]["coordinates"].AsBsonArray
                };


            }



            return mahalle;
        }

        public async Task<DenemeGeoMah> GetDenemeGeo(string sehir)
        {
            var connectionString = "mongodb://127.0.0.1:27017";
            client = new MongoClient(connectionString);
            db = client.GetDatabase("test");

            IMongoCollection<BsonDocument> mg = db.GetCollection<BsonDocument>("neighborhoods");

            var filter = Builders<BsonDocument>.Filter.Eq("name", sehir);
            var result = await mg.FindAsync(filter);


            //var cursor = await mg.Find(new BsonDocument()).ToCursorAsync();
            var mahalledeneme = new DenemeGeoMah();

            while (await result.MoveNextAsync())
            {
                var listOfUsers = result.Current.ToList();


                mahalledeneme.name = listOfUsers[0]["name"].AsString;


                mahalledeneme.geometry = new GeoPointBson()
                {
                    type = listOfUsers[0]["geometry"]["type"].AsString,
                    coordinates = listOfUsers[0]["geometry"]["coordinates"].AsBsonArray
                };


            }

            return mahalledeneme;
        }





    }
}
