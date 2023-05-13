using MSHB.TsetmcReader.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Contract
{
    public interface IIncomingService
    {
        void AddAsync(string responseMessage);
        void Start(List<InformationHandlerViewModel> lastState, List<ChartViewModel> lastChart);
        void Stop();
        bool GetStatus();
    }
}
