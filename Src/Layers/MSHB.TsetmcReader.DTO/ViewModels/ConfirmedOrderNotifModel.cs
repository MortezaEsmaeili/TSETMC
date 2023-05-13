using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.DTO.ViewModels
{
    public class ConfirmedOrderNotifModel
    {
        public long InstrumentId { get; set; }
        public string ReceivedDate { get; set; }
        public double Wp { get; set; }
        public long Ltp { get; set; }
        public long Cp { get; set; }
        public double Av15qpcd { get; set; }
        public double Cptqpcd { get; set; }
        public double Qpcd { get; set; }
        public double Ewpcp { get; set; }
        public double Tavewpcp { get; set; }
        public double Cltppbpsdp { get; set; }
        public ConfirmOrderGridModel orderGridModel { get; set; }
    }
}
