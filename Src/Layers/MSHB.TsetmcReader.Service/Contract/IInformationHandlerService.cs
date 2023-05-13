using MSHB.TsetmcReader.DTO.FormModels;
using MSHB.TsetmcReader.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Contract
{
    public interface IInformationHandlerService
    {
        List<InformationHandlerViewModel> Insert(AddInformationHandlerFormModel formModel);
    }
}
