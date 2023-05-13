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
    public static class InstrumentMapper
    {
        public static Dictionary<string, long> ToDictionary(this IQueryable<Instrument_T> query)
        {
            return query.Select(x => new { x.InstrumentCode, x.Id }).ToDictionary(pair => pair.InstrumentCode, pair => pair.Id);
        }

        public static IQueryable<InstrumentViewModel> ToViewModel(this IQueryable<Instrument_T> query)
        {
            return query.Select(x => new InstrumentViewModel { InstrumentId = x.Id, InstrumentCode = x.InstrumentCode, InstrumentName = x.InstrumentName });
        }      
    }
}
