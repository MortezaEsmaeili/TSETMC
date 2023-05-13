using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.DTO.FormModels
{
    public class InformationHandlerFormModel
    {
        [JsonProperty("bv")]
        public long Bv { get; set; }

        [JsonProperty("cp")]
        public long Cp { get; set; }

        [JsonProperty("cn")]
        public string Cn { get; set; }

        [JsonProperty("bbp")]
        public long Bbp { get; set; }

        [JsonProperty("bbq")]
        public long Bbq { get; set; }

        [JsonProperty("bsp")]
        public long Bsp { get; set; }

        [JsonProperty("bsq")]
        public long Bsq { get; set; }

        [JsonProperty("nbb")]
        public long Nbb { get; set; }

        [JsonProperty("nbs")]
        public long Nbs { get; set; }

        [JsonProperty("ftp")]
        public long Ftp { get; set; }

        [JsonProperty("gs")]
        public long Gs { get; set; }

        [JsonProperty("hap")]
        public long Hap { get; set; }

        [JsonProperty("hp")]
        public long Hp { get; set; }

        [JsonProperty("ltp")]
        public long Ltp { get; set; }

        [JsonProperty("lap")]
        public long Lap { get; set; }

        [JsonProperty("lp")]
        public long Lp { get; set; }

        [JsonProperty("mtd")]
        public string Mtd { get; set; }

        [JsonProperty("mxqo")]
        public long Mxqo { get; set; }

        [JsonProperty("mnqo")]
        public long Mnqo { get; set; }

        [JsonProperty("nc")]
        public string Nc { get; set; }

        [JsonProperty("pcp")]
        public long Pcp { get; set; }

        [JsonProperty("pv")]
        public double Pv { get; set; }

        [JsonProperty("rp")]
        public long Rp { get; set; }

        [JsonProperty("sc")]
        public string Sc { get; set; }

        [JsonProperty("sf")]
        public string Sf { get; set; }

        [JsonProperty("ss")]
        public long Ss { get; set; }

        [JsonProperty("nst")]
        public long Nst { get; set; }

        [JsonProperty("nt")]
        public long Nt { get; set; }

        [JsonProperty("tv")]
        public long Tv { get; set; }

        [JsonProperty("td")]
        public string Td { get; set; }

        [JsonProperty("vs")]
        public long Vs { get; set; }

        [JsonProperty("cpv")]
        public long Cpv { get; set; }

        [JsonProperty("cpvp")]
        public double Cpvp { get; set; }

        [JsonProperty("lpv")]
        public long Lpv { get; set; }

        [JsonProperty("lpvp")]
        public double Lpvp { get; set; }

        [JsonProperty("ic")]
        public string Ic { get; set; }

        [JsonProperty("csid")]
        public string Csid { get; set; }
    }
}
