using MSHB.TsetmcReader.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Contract
{
    public interface INotificationService
    {
        Task<bool> IsConnectedAsync();
        Task IncomingNotify(object model);
        void AlertNotifyAsync(object model);
        void InformationHandlerNotifyAsync(object model);
        void ChartNotifyAsync(object model);
        void ConfirmedOrderNotify(long instrumentId, object model);
    }
}
