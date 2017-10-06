using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace BasarMapTryWeb.Models
{
    public class Il
    {
        public int IdariId { get; set; }
        public string IlAdi { get; set; }
        public int Nufus { get; set; }
        public GeoPointBson Geo { get; set; }
    }

    public class Illergelsin
    {
        [BsonElement]
        public int IdariId { get; set; }
        [BsonElement]
        public string IlAdi { get; set; }

    }

    public class Ilcegelsin
    {
        [BsonElement]
        public int IdariId { get; set; }
        [BsonElement]
        public int UstIdariId { get; set; }
        [BsonElement]
        public string IlceAdi { get; set; }
    }

    public class Mahallegelsin
    {
        [BsonElement]
        public int IdariId { get; set; }
        [BsonElement]
        public int UstIdariId { get; set; }
        [BsonElement]
        public string Adi { get; set; }
    }

    public class MahalleCiz
    {
        public int UstIdariId { get; set; }
        public int IdariId { get; set; }
        public string IlceAdi { get; set; }
        public string IlAdi { get; set; }
        public int IlKod { get; set; }
        public string Adi { get; set; }
        public string AdiAdr { get; set; }
        public string Tip { get; set; }
        public int TipKod { get; set; }
        public int PostaKodu { get; set; }
        public int Nufus { get; set; }
        public GeoPointBson Geo { get; set; }
    }



    public class DenemeGeoMah
    {
        public string name { get; set; }
        public GeoPointBson geometry { get; set; }
    }




}