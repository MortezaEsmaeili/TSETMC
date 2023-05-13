using MSHB.TsetmcReader.Service.Contract;
using MSHB.TsetmcReader.Service.TsetmcWebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Impl
{
    public class TsetmcService : ITsetmcService
    {
        private TsePublicV2SoapClient _client;
        public TsetmcService()
        {
            _client = new TsePublicV2SoapClient();
        }

        public List<string> GetRowIds(List<string> instrumentCodes)
        {
            try
            {
                List<string> rowIds = new List<string>();
                var table = _client.Instrument("sabafam.com", "sabafam", 1).Tables[0];
                if (table.Rows.Count > 0)
                {
                    rowIds.AddRange(table.Rows.Cast<DataRow>().Where(x => instrumentCodes.Contains(x["InsCode"].ToString())).Select(x => x["InstrumentID"].ToString()).ToList());
                }

                table = _client.Instrument("sabafam.com", "sabafam", 2).Tables[0];
                if (table.Rows.Count > 0)
                {
                    rowIds.AddRange(table.Rows.Cast<DataRow>().Where(x => instrumentCodes.Contains(x["InsCode"].ToString())).Select(x => x["InstrumentID"].ToString()).ToList());
                }

                table = _client.Instrument("sabafam.com", "sabafam", 4).Tables[0];
                if (table.Rows.Count > 0)
                {
                    rowIds.AddRange(table.Rows.Cast<DataRow>().Where(x => instrumentCodes.Contains(x["InsCode"].ToString())).Select(x => x["InstrumentID"].ToString()).ToList());
                }

                return rowIds;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public double MarketActivityLast()
        {
            try
            {
                var table = _client.MarketActivityLast("sabafam.com", "sabafam", 1).Tables[0];
                if (table.Rows.Count > 0)
                {
                    double.TryParse(table.Rows[0]["IndexLastValue"].ToString(), out double IndexLastValue);
                    return Math.Round(IndexLastValue, 0);
                }

                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
