using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Contract
{
    public interface ITsetmcService
    {
        List<string> GetRowIds(List<string> instrumentCodes);
        double MarketActivityLast();
    }
}
