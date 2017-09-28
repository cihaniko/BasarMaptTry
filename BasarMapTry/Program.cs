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
            

          iller();

        }

        private static void iller()
        {

            var h = Mitab.mitab_c_open(@"C:\Users\Hkn\Documents\visual studio 2015\Projects\BasarMapTry\BasarMapTry\data\IL.TAB");
            var c= new MitabColumns(h);
            var featureId = Mitab.mitab_c_next_feature_id(h, -1);

            var client=new MongoClient();
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<Il>("Iller");
            collection.Indexes.CreateOneAsync("{ \"Geo\" : \"2dsphere\"}");

            var s = 0;
            var hata = 0;

            while (featureId!=-1)
            {
                var feature = Mitab.mitab_c_read_feature(h, featureId);
                var polygonCount = Mitab.mitab_c_get_parts(feature);
                var ilAdi = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("ILADI").index);
                var idariId = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("IDARIID").index);
                var nufus = Mitab.mitab_c_get_field_as_string_csharp(feature, c.GetColumn("NUFUS").index);
                var polygons=new BsonArray();
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
                        polygon.Add(polygon);
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
                    s++;
                    Console.WriteLine("s");
                    collection.InsertOneAsync(il);
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

     }


 }
