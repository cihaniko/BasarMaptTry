using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasarMapTryWeb.Models
{
    public class Il
    {
        public int IdariId { get; set; }
        public string IlAdi { get; set; }
        public int Nufus { get; set; }
        public GeoPointBson Geo { get; set; }
    }
}