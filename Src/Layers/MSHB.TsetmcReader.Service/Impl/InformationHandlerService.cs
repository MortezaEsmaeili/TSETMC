using MSHB.TsetmcReader.DataLayer.DataModels;
using MSHB.TsetmcReader.DTO.FormModels;
using MSHB.TsetmcReader.DTO.ViewModels;
using MSHB.TsetmcReader.Service.Contract;
using MSHB.TsetmcReader.Service.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Impl
{
    public class InformationHandlerService : IInformationHandlerService
    {
        private StockMonitorDbContext _context;        
        private Dictionary<string, long> _insData;
        public InformationHandlerService()
        {
            _context = new StockMonitorDbContext();
            _insData = _context.Instrument_T.ToDictionary();            
        }

        public List<InformationHandlerViewModel> Insert(AddInformationHandlerFormModel formModel)
        {
            try
            {
                var data = formModel.ToEntityModel(_insData);

                _context.InformationHandler_T.BulkInsert(data, options => { options.BatchSize = data.Count; options.InsertKeepIdentity = false; });

                return data.ToViewModel();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
                // return new List<InformationHandlerViewModel>();
            }
        }
    }
}
