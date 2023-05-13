using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.DTO.ViewModels
{
    public class ChartNotifModel
    {
        public double IndexLastValue { get; set; }
        public List<ChartPropertyNotifModel> Properties { get; set; }
    }
}
