using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
namespace BasarMapTryWeb.Models
{
    public class GeoPointBson
    {
        public string type { get; set; }
        public BsonArray coordinates { get; set; }
    }
}