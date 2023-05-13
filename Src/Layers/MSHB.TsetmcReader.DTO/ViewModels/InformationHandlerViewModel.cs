using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.DTO.ViewModels
{
    public class InformationHandlerViewModel
    {
        public long InstrumentId { get; set; }

        public long Bv { get; set; }

        public long Cp { get; set; }

        public string Cn { get; set; }

        public long Bbp { get; set; }

        public long Bbq { get; set; }

        public long Bsp { get; set; }

        public long Bsq { get; set; }

        public long Nbb { get; set; }

        public long Nbs { get; set; }

        public long Ftp { get; set; }

        public long Gs { get; set; }

        public long Hap { get; set; }

        public long Hp { get; set; }

        public long Ltp { get; set; }

        public long Lap { get; set; }

        public long Lp { get; set; }

        public string Mtd { get; set; }

        public long Mxqo { get; set; }

        public long Mnqo { get; set; }

        public string Nc { get; set; }

        public long Pcp { get; set; }

        public double Pv { get; set; }

        public long Rp { get; set; }

        public string Sc { get; set; }

        public string Sf { get; set; }

        public long Ss { get; set; }

        public long Nst { get; set; }

        public long Nt { get; set; }

        public long Tv { get; set; }

        public string Td { get; set; }

        public long Vs { get; set; }

        public long Cpv { get; set; }

        public double Cpvp { get; set; }

        public long Lpv { get; set; }

        public double Lpvp { get; set; }

        public string Ic { get; set; }

        public string Csid { get; set; }

        public DateTime ReceivedDate { get; set; }

        public long BL { get; set; }

        public long AV { get; set; }

        public bool Signal1 { get; set; }

        public double Signal2 { get; set; }

        public double Signal3 { get; set; }

        public double Signal4 { get; set; }

        public double Signal5 { get; set; }
        public double CA { get; set; }

        public bool IsSelectedForShow { get; set; }

        public long CapecityOfSelectedShow { get; set; }

        public InformationHandlerViewModel ShallowCopy()
        {
            return (InformationHandlerViewModel)this.MemberwiseClone();
        }
    }
}
