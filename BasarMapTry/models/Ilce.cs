using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasarMapTry.models
{
    class Ilce
    {
        public int UstIdariId { get; set; }
        public int IdariId { get; set; }
        public string IlceAdi { get; set; }
        public string IlAdi { get; set; }
        public string Tip { get; set; }
        public int TipKod { get; set; }
        public int Nufus { get; set; }
        public GeoPointBson Geo { get; set; }



    }
}
