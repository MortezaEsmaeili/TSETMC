using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.DTO.FormModels
{
    public class AddInformationHandlerFormModel
    {
        public List<InformationHandlerFormModel> informationHandlers { get; set; }
        public DateTime ReceivedDate { get; set; }
    }
}
