using MSHB.TsetmcReader.DataLayer.DataModels;
using MSHB.TsetmcReader.DTO.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Mapper
{
    public static class ClientHandlerMapper
    {
        public static List<ClientHandler_T> ToEntityModel(this ConcurrentBag<ClientHandlerViewModel> query, DateTime receivedDate)
        {
            return query.Select(item => new ClientHandler_T
            {
                InstrumentId = item.InstrumentId,
                Ibn = item.Ibn,
                Ib_nst = item.Ib_nst,
                Isn = item.Isn,
                Is_nst = item.Is_nst,
                Nbn = item.Nbn,
                Nb_nst = item.Nb_nst,
                Nsn = item.Nsn,
                Ns_nst = item.Ns_nst,
                ReceivedDate = receivedDate
            }).ToList();
        }

        public static ClientHandler_T ToEntityModel(this ClientHandlerViewModel item, DateTime receivedDate)
        {
            return new ClientHandler_T
            {
                InstrumentId = item.InstrumentId,
                Ibn = item.Ibn,
                Ib_nst = item.Ib_nst,
                Isn = item.Isn,
                Is_nst = item.Is_nst,
                Nbn = item.Nbn,
                Nb_nst = item.Nb_nst,
                Nsn = item.Nsn,
                Ns_nst = item.Ns_nst,
                ReceivedDate = receivedDate
            };
        }
    }
}
