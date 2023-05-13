using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.DTO.ViewModels
{
    public class ChartViewModel
    {
        public long InstrumentId { get; set; }
        public long Cp { get; set; }
        public long Ltp { get; set; }
        public long Nst { get; set; }
        public double EFF { get; set; }
        public double Delta { get; set; }
        public double DiffParam { get; set; }
        public double CumulativeDiff { get; set; }
        public double NormCumulativeDiff { get; set; }
        public DateTime ReceivedDate { get; set; }

        public ChartViewModel ShallowCopy()
        {
            return (ChartViewModel)this.MemberwiseClone();
        }
    }
}
