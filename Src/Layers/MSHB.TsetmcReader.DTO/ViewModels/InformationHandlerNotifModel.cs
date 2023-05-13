using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.DTO.ViewModels
{
    public class InformationHandlerNotifModel
    {
        public long InstrumentId { get; set; }
        public string Time { get; set; }
        public bool Signal1 { get; set; }
        public double Signal2 { get; set; }
        public double Signal3 { get; set; }
        public double Signal4 { get; set; }
        public double Signal5 { get; set; }
        public long Bsq { get; set; }
        public bool IsSelectedForShow { get; set; }
        public long CapecityOfSelectedShow { get; set; }
        public long Ltp { get; set; }
        public double Plp { get; set; }
        public string Information { get; set; }
    }
}
