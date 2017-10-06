using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasarMapTry.models;
using gtLib2.MitabLib;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BasarMapTry
{
    class Program
    {
        static void Main(string[] args)
        {


            // iller();
            // ilceler();
            // mahalleler();
            // kapiNo();
            Console.ReadLine();

        }

        private static void iller()
        {

            var h = Mitab.mitab_c_open(@"C:\Users\Hkn\Documents\visual studio 2015\Projects\BasarMapTry\BasarMapTry\data\IL.TAB");
            var c = new MitabColumns(h);
            var featureId = Mitab.mitab_c_next_feature_id(h, -1);

            var client = new MongoClient();
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Il>("Iller");
            collection.Indexes.CreateOneAsync("{ \"Geo\" : \"2dsphere\"}");

            var s = 0;
            var hata = 0;

            while (featureId != -1)
            {
                var feature = Mitab.mitab_c_read_feature(h, featureId);
                var polygonCount = Mitab.mitab_c_get_parts(feature);
                var ilAdi = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ILADI").index);
                var idariId = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("IDARIID").index);
                var nufus = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("NUFUS").index);
                var polygons = new BsonArray();
                var type = "";
                for (var i = 0; i < polygonCount; i++)
                {
                    var outerArr = new BsonArray();
                    var polygon = new BsonArray();

                    var vertexCount = Mitab.mitab_c_get_vertex_count(feature, i);

                    for (var j = 0; j < vertexCount; j++)
                    {
                        var coordinates = new BsonArray();
                        var lng = Mitab.mitab_c_get_vertex_x(feature, i, j);
                        var lat = Mitab.mitab_c_get_vertex_y(feature, i, j);

                        coordinates.Add(lng);
                        coordinates.Add(lat);
                        polygon.Add(coordinates);

                    }
                    if (polygonCount > 1)
                    {
                        outerArr.Add(polygon);
                        polygons.Add(outerArr);
                        type = "MultiPolygon";
                    }
                    else
                    {
                        polygons.Add(polygon);
                        type = "Polygon";
                    }
                }
                var il = new Il
                {
                    IlAdi = ilAdi,
                    IdariId = Convert.ToInt32(idariId),
                    Nufus = Convert.ToInt32(nufus),
                    Geo = new GeoPointBson()
                    {
                        type = type,
                        coordinates = polygons
                    }

                };

                try
                {

                    collection.InsertOneAsync(il);
                    //collection.InsertOne(il);

                    s++;
                    Console.WriteLine("eklenen il :" + s);
                }
                catch (Exception ex)
                {
                    hata++;
                    Console.WriteLine("hata");
                }
                Mitab.mitab_c_destroy_feature(feature);
                featureId = Mitab.mitab_c_next_feature_id(h, featureId);
            }


        }

        private static void ilceler()
        {
            var h = Mitab.mitab_c_open(@"C:\Users\Hkn\Documents\visual studio 2015\Projects\BasarMapTry\BasarMapTry\data\ILCE.TAB");
            var c = new MitabColumns(h);
            var featureId = Mitab.mitab_c_next_feature_id(h, -1);

            var client = new MongoClient();
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Ilce>("Ilceler");
            collection.Indexes.CreateOneAsync("{ \"Geo\" : \"2dsphere\"}");

            var s = 0;
            var hata = 0;

            while (featureId != -1)
            {
                var feature = Mitab.mitab_c_read_feature(h, featureId);
                var polygonCount = Mitab.mitab_c_get_parts(feature);
                var ilceAdi = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ILCEADI").index);
                var ilAdi = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ILADI").index);
                var idariId = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("IDARIID").index);
                var nufus = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("NUFUS").index);
                var ustIdarıId = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("USTIDARIID").index);
                var tip = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("TIP").index);
                var tipKod = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("TIPKOD").index);

                var polygons = new BsonArray();
                var type = "";
                for (var i = 0; i < polygonCount; i++)
                {
                    var outerArr = new BsonArray();
                    var polygon = new BsonArray();
                    var vertexCount = Mitab.mitab_c_get_vertex_count(feature, i);

                    for (var j = 0; j < vertexCount; j++)
                    {
                        var coordinates = new BsonArray();
                        var lng = Mitab.mitab_c_get_vertex_x(feature, i, j);
                        var lat = Mitab.mitab_c_get_vertex_y(feature, i, j);

                        coordinates.Add(lng);
                        coordinates.Add(lat);
                        polygon.Add(coordinates);

                    }
                    if (polygonCount > 1)
                    {
                        outerArr.Add(polygon);
                        polygons.Add(outerArr);
                        type = "MultiPolygon";
                    }
                    else
                    {
                        polygons.Add(polygon);
                        type = "Polygon";
                    }
                }
                var ilce = new Ilce
                {
                    UstIdariId = Convert.ToInt32(ustIdarıId),
                    IdariId = Convert.ToInt32(idariId),
                    IlceAdi = ilceAdi,
                    IlAdi = ilAdi,
                    Tip = tip,
                    TipKod = Convert.ToInt32(tipKod),
                    Nufus = Convert.ToInt32(nufus),
                    Geo = new GeoPointBson()
                    {
                        type = type,
                        coordinates = polygons
                    }

                };

                try
                {
                    collection.InsertOneAsync(ilce);
                    s++;
                    Console.WriteLine("eklenen ilce :" + s);

                }
                catch
                {
                    hata++;
                    Console.WriteLine("hata");
                }
                Mitab.mitab_c_destroy_feature(feature);
                featureId = Mitab.mitab_c_next_feature_id(h, featureId);
            }
        }

        private static void mahalleler()
        {
            var h = Mitab.mitab_c_open(@"C:\Users\Hkn\Documents\visual studio 2015\Projects\BasarMapTry\BasarMapTry\data\KOYMAHALLE.TAB");
            var c = new MitabColumns(h);
            var featureId = Mitab.mitab_c_next_feature_id(h, -1);

            var client = new MongoClient();
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Mahalle>("Mahalleler");
            collection.Indexes.CreateOneAsync("{ \"Geo\" : \"2dsphere\"}");

            var s = 0;
            var hata = 0;

            while (featureId != -1)
            {
                var feature = Mitab.mitab_c_read_feature(h, featureId);
                var polygonCount = Mitab.mitab_c_get_parts(feature);
                var ustidariId = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("USTIDARIID").index);
                var ilAdi = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ILADI").index);
                var idariId = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("IDARIID").index);
                var ilceAdi = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ILCEADI").index);
                var ilKod = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ILKOD").index);
                var adi = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ADI").index);
                var adiAdr = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ADIADR").index);
                var tip = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("TIP").index);
                var tipKod = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("TIPKOD").index);
                var postaKodu = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("POSTAKODU").index);
                var nufus = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("NUFUS").index);

                var polygons = new BsonArray();
                var type = "";
                for (var i = 0; i < polygonCount; i++)
                {
                    var outerArr = new BsonArray();
                    var polygon = new BsonArray();
                    var vertexCount = Mitab.mitab_c_get_vertex_count(feature, i);

                    for (var j = 0; j < vertexCount; j++)
                    {
                        var coordinates = new BsonArray();
                        var lng = Mitab.mitab_c_get_vertex_x(feature, i, j);
                        var lat = Mitab.mitab_c_get_vertex_y(feature, i, j);

                        coordinates.Add(lng);
                        coordinates.Add(lat);
                        polygon.Add(coordinates);

                    }
                    if (polygonCount > 1)
                    {
                        outerArr.Add(polygon);
                        polygons.Add(outerArr);
                        type = "MultiPolygon";
                    }
                    else
                    {
                        polygons.Add(polygon);
                        type = "Polygon";
                    }
                }
                var mahalle = new Mahalle
                {
                    UstIdariId = Convert.ToInt32(ustidariId),
                    IlAdi = ilAdi,
                    IdariId = Convert.ToInt32(idariId),
                    IlceAdi = ilceAdi,
                    IlKod = Convert.ToInt32(ilKod),
                    Adi = adi,
                    AdiAdr = adiAdr,
                    Tip = tip,
                    TipKod = Convert.ToInt32(tipKod),
                    PostaKodu = Convert.ToInt32(postaKodu),
                    Nufus = Convert.ToInt32(nufus),

                    Geo = new GeoPointBson()
                    {
                        type = type,
                        coordinates = polygons
                    }

                };

                try
                {
                    collection.InsertOneAsync(mahalle);
                    s++;
                    Console.WriteLine("eklenen mahalle :" + s);

                }
                catch
                {
                    hata++;
                    Console.WriteLine("hata");
                }
                Mitab.mitab_c_destroy_feature(feature);
                featureId = Mitab.mitab_c_next_feature_id(h, featureId);
            }
        }

        private static void kapiNo()
        {
            var h = Mitab.mitab_c_open(@"C:\Users\Hkn\Documents\visual studio 2015\Projects\BasarMapTry\BasarMapTry\data\KapiNo.TAB");
            var c = new MitabColumns(h);
            var featureId = Mitab.mitab_c_next_feature_id(h, -1);

            var client = new MongoClient();
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<KapiNo>("KapiNo");
            collection.Indexes.CreateOneAsync("{ \"Geo\" : \"2dsphere\"}");

            var s = 0;
            var hata = 0;

            while (featureId != -1)
            {
                var feature = Mitab.mitab_c_read_feature(h, featureId);
                //var polygonCount = Mitab.mitab_c_get_parts(feature);
                var no = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("NO").index);

                var points = new BsonArray();
                var type = "";
                var point = new BsonArray();

                var coordinates = new BsonArray();
                var lng = Mitab.mitab_c_get_vertex_x(feature, 0, 0);
                var lat = Mitab.mitab_c_get_vertex_y(feature, 0, 0);

                coordinates.Add(lng);
                coordinates.Add(lat);
                //point.Add(coordinates);



                type = "Point";



                var kapiNo = new KapiNo
                {
                    No = no,
                    Geo = new GeoPointBson()
                    {
                        type = type,
                        coordinates = coordinates
                    }

                };

                try
                {

                    collection.InsertOneAsync(kapiNo);
                    //collection.InsertOne(il);

                    s++;
                    Console.WriteLine("eklenen kapi :" + s);
                }
                catch (Exception ex)
                {
                    hata++;
                    Console.WriteLine("hata");
                }
                Mitab.mitab_c_destroy_feature(feature);
                featureId = Mitab.mitab_c_next_feature_id(h, featureId);
            }
        }
    }


}
