using MSHB.TsetmcReader.DataLayer.DataModels;
using MSHB.TsetmcReader.DTO.ViewModels;
using MSHB.TsetmcReader.Service.Contract;
using MSHB.TsetmcReader.Service.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSHB.TsetmcReader.Service.Impl
{
    public class InstrumentService : IInstrumentService
    {
        StockMonitorDbContext _context;
        public InstrumentService()
        {
            _context = new StockMonitorDbContext();
        }

        public List<string> GetInstrumentCodes()
        {
            try
            {
                return _context.Instrument_T.Where(x => x.IsActive == true).Select(x => x.InstrumentCode).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<InstrumentViewModel> Get()
        {
            try
            {
                return _context.Instrument_T.Where(x => x.IsActive == true).ToViewModel().ToList();
            }
            catch (Exception)
            {
                return new List<InstrumentViewModel>();
            }
        }

        public Dictionary<string, long> GetDictionary()
        {
            try
            {
                return _context.Instrument_T.Where(x => x.IsActive == true && x.ActiveClientData == true).ToDictionary();
            }
            catch (Exception)
            {
                return new Dictionary<string, long>();
            }
        }       
    }
}
