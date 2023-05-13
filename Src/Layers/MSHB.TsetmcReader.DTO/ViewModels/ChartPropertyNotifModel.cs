using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.DTO.ViewModels
{
    public class ChartPropertyNotifModel
    {
        public long InstrumentId { get; set; }
        public long Ltp { get; set; }
        public double CumulativeDiff { get; set; }
        public double NormCumulativeDiff { get; set; }
        public string ReceivedDate { get; set; }
    }
}
