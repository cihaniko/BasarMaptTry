using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace BasarMapTry.models
{
    class GeoPointBson
    {
        public string type { get; set; }
        public BsonArray coordinates { get; set; }


    }
}
