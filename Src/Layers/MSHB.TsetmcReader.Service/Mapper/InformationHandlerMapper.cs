using MSHB.TsetmcReader.DataLayer.DataModels;
using MSHB.TsetmcReader.DTO.FormModels;
using MSHB.TsetmcReader.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Mapper
{
    public static class InformationHandlerMapper
    {
        public static List<InformationHandler_T> ToEntityModel(this AddInformationHandlerFormModel formModel, Dictionary<string, long> _insData)
        {
            var data = new List<InformationHandler_T>();

            foreach (var item in formModel.informationHandlers)
            {
                var handler = new InformationHandler_T();
                handler.ReceivedDate = formModel.ReceivedDate;
                handler.InstrumentId = _insData.First(x => x.Key == item.Ic.Trim()).Value;
                handler.Bbp = item.Bbp;
                handler.Bsp = item.Bsp;
                handler.Bv = item.Bv;
                handler.Bbq = item.Bbq;
                handler.Bsq = item.Bsq;
                handler.Cn = item.Cn;
                handler.Cp = item.Cp;
                handler.Cpv = item.Cpv;
                handler.Cpvp = item.Cpvp;
                handler.Csid = item.Csid;
                handler.Ftp = item.Ftp;
                handler.Gs = item.Gs;
                handler.Hap = item.Hap;
                handler.Hp = item.Hp;
                handler.Ic = item.Ic;
                handler.Lap = item.Lap;
                handler.Lp = item.Lp;
                handler.Lpv = item.Lpv;
                handler.Lpvp = item.Lpvp;
                handler.Ltp = item.Ltp;
                handler.Mnqo = item.Mnqo;
                handler.Mtd = item.Mtd;
                handler.Mxqo = item.Mxqo;
                handler.Nbb = item.Nbb;
                handler.Nbs = item.Nbs;
                handler.Nc = item.Nc;
                handler.Nst = item.Nst;
                handler.Nt = item.Nt;
                handler.Pcp = item.Pcp;
                handler.Pv = item.Pv;
                handler.Rp = item.Rp;
                handler.Sc = item.Sc;
                handler.Sf = item.Sf;
                handler.Ss = item.Ss;
                handler.Td = item.Td;
                handler.Tv = item.Tv;
                handler.Vs = item.Vs;

                var Signal1 = handler.Bsp == handler.Lap ? true : false;

                var AV = handler.Ltp == handler.Lap ? handler.Bsq : 0;

                handler.BL = 0;

                var AZ = handler.Nst;
                double Signal2 = AZ == 0 ? 0 : handler.BL / AZ;


                double BK = handler.Bsp == handler.Lap ? (AZ == 0 ? 0 : handler.Bsq / AZ) : 0;
                double BM = Signal2;
                double Signal3 = BM == 0 ? 0 : BK / BM;


                long BB = 0;
                long AX = 0;

                double BI = handler.Bsp == handler.Lap ? (handler.Bsq == 0 ? 0 : ((-1 * AX) / handler.Bsq)) : 0;
                double BG = AV == 0 ? 0 : (handler.Bsp == handler.Lap ? BB / AV : 0);
                double Signal4 = BI + BG;

                handler.AV = AV;
                handler.Signal1 = Signal1;
                handler.Signal2 = Signal2 * 100;
                handler.Signal3 = Signal3 * 100;
                handler.Signal4 = Signal4 * 100;
                handler.Signal5 = 0;
                handler.IsSelectedForShow = handler.Bsp == handler.Lap ? true : false;
                handler.CapecityOfSelectedShow = handler.Bsq;

                data.Add(handler);
            }

            return data;
        }

        public static List<InformationHandler_T> ToEntityModel(this List<InformationHandlerViewModel> information, DateTime receivedDate)
        {
            return information.Select(item => new InformationHandler_T
            {
                ReceivedDate = receivedDate,
                InstrumentId = item.InstrumentId,
                Bbp = item.Bbp,
                Bsp = item.Bsp,
                Bv = item.Bv,
                Bbq = item.Bbq,
                Bsq = item.Bsq,
                Cn = item.Cn,
                Cp = item.Cp,
                Cpv = item.Cpv,
                Cpvp = item.Cpvp,
                Csid = item.Csid,
                Ftp = item.Ftp,
                Gs = item.Gs,
                Hap = item.Hap,
                Hp = item.Hp,
                Ic = item.Ic,
                Lap = item.Lap,
                Lp = item.Lp,
                Lpv = item.Lpv,
                Lpvp = item.Lpvp,
                Ltp = item.Ltp,
                Mnqo = item.Mnqo,
                Mtd = item.Mtd,
                Mxqo = item.Mxqo,
                Nbb = item.Nbb,
                Nbs = item.Nbs,
                Nc = item.Nc,
                Nst = item.Nst,
                Nt = item.Nt,
                Pcp = item.Pcp,
                Pv = item.Pv,
                Rp = item.Rp,
                Sc = item.Sc,
                Sf = item.Sf,
                Ss = item.Ss,
                Td = item.Td,
                Tv = item.Tv,
                Vs = item.Vs,
                AV = item.AV,
                BL = item.BL,
                CapecityOfSelectedShow = item.CapecityOfSelectedShow,
                IsSelectedForShow = item.IsSelectedForShow,
                Signal1 = item.Signal1,
                Signal2 = item.Signal2,
                Signal3 = item.Signal3,
                Signal4 = item.Signal4,
                Signal5 = item.Signal5
            }).ToList();
        }

        public static List<InformationHandlerNotifModel> ToNotifModel(this List<InformationHandlerViewModel> data)
        {
            return data.Select(information => new InformationHandlerNotifModel
            {
                InstrumentId = information.InstrumentId,
                Signal1 = information.Signal1,
                Signal2 = information.Signal2,
                Signal3 = information.Signal3,
                Signal4 = information.Signal4,
                Signal5 = information.Signal5,
                Bsq = information.Bsq,
                IsSelectedForShow = information.IsSelectedForShow,
                CapecityOfSelectedShow = information.CapecityOfSelectedShow,
                Ltp = information.Ltp,
                Plp = ((double)(information.Ltp - information.Lap) / information.Lap) * 100 - 1.25,
                Time = information.ReceivedDate.ToString("HH:mm:ss fff"),
                Information = $"Bl:{information.BL}, Bbp:{information.Bbp}, Bbq:{information.Bbq}, Bsp:{information.Bsp}, Bsq:{information.Bsq}, Ltp:{information.Ltp}"
            }).ToList();
        }

        public static List<ChartPropertyNotifModel> ToNotifModel(this List<ChartViewModel> data)
        {
            return data.Select(item => new ChartPropertyNotifModel
            {
                InstrumentId = item.InstrumentId,
                Ltp = item.Ltp,
                CumulativeDiff = item.CumulativeDiff,
                NormCumulativeDiff = item.NormCumulativeDiff,
                ReceivedDate = item.ReceivedDate.ToString("HH:mm:ss")
            }).ToList();
        }

        public static List<InformationHandlerViewModel> ToViewModel(this List<InformationHandler_T> informationHandlers)
        {
            return informationHandlers.Select(item => new InformationHandlerViewModel
            {
                ReceivedDate = item.ReceivedDate,
                InstrumentId = item.InstrumentId,
                Bbp = item.Bbp,
                Bsp = item.Bsp,
                Bv = item.Bv,
                Bbq = item.Bbq,
                Bsq = item.Bsq,
                Cn = item.Cn,
                Cp = item.Cp,
                Cpv = item.Cpv,
                Cpvp = item.Cpvp,
                Csid = item.Csid,
                Ftp = item.Ftp,
                Gs = item.Gs,
                Hap = item.Hap,
                Hp = item.Hp,
                Ic = item.Ic,
                Lap = item.Lap,
                Lp = item.Lp,
                Lpv = item.Lpv,
                Lpvp = item.Lpvp,
                Ltp = item.Ltp,
                Mnqo = item.Mnqo,
                Mtd = item.Mtd,
                Mxqo = item.Mxqo,
                Nbb = item.Nbb,
                Nbs = item.Nbs,
                Nc = item.Nc,
                Nst = item.Nst,
                Nt = item.Nt,
                Pcp = item.Pcp,
                Pv = item.Pv,
                Rp = item.Rp,
                Sc = item.Sc,
                Sf = item.Sf,
                Ss = item.Ss,
                Td = item.Td,
                Tv = item.Tv,
                Vs = item.Vs,
                AV = item.AV,
                BL = item.BL,
                CA = 0,
                CapecityOfSelectedShow = item.CapecityOfSelectedShow,
                IsSelectedForShow = item.IsSelectedForShow,
                Signal1 = item.Signal1,
                Signal2 = item.Signal2,
                Signal3 = item.Signal3,
                Signal4 = item.Signal4,
                Signal5 = item.Signal5
            }).ToList();
        }

        public static List<ChartViewModel> ToChartModel(this List<InformationHandlerViewModel> informationHandlers)
        {
            return informationHandlers.Select(item => new ChartViewModel
            {
                ReceivedDate = item.ReceivedDate,
                InstrumentId = item.InstrumentId,
                Cp = item.Cp,
                Nst = item.Nst,
                Ltp = item.Ltp,
                EFF = (item.Ltp - item.Cp) / (double)item.Cp,
                Delta = item.Nst,
                DiffParam = (item.Ltp - item.Cp) / (double)item.Cp,
                CumulativeDiff = 1 + ((item.Ltp - item.Cp) / (double)item.Cp),
                NormCumulativeDiff = 5000 * (1 + ((item.Ltp - item.Cp) / (double)item.Cp)) - 5000
            }).ToList();
        }

        public static void LastStateUpdater(this List<InformationHandlerViewModel> lastState, double[][] matrix)
        {
            DateTime ReceivedDate = DateTime.Now;

            Parallel.For(0, matrix.GetLength(0), index =>
            {

                var handler = lastState.FirstOrDefault(x => x.InstrumentId == matrix[index][0]);
                var lastStateHandler = handler.ShallowCopy();
                handler.ReceivedDate = ReceivedDate;
                handler.Bbp = (long)matrix[index][5];
                handler.Bsp = (long)matrix[index][3];
                handler.Bbq = (long)matrix[index][4];
                handler.Bsq = (long)matrix[index][2];
                handler.Cp = (long)matrix[index][10];
                handler.Cpvp = matrix[index][11];
                handler.Lpvp = matrix[index][9];
                handler.Ltp = (long)matrix[index][8];
                handler.Nst = (long)matrix[index][7];
                handler.Nt = (long)matrix[index][6];
                handler.Tv = (long)matrix[index][1];


                var Signal1 = handler.Bsp == handler.Lap ? true : false;

                var AV = handler.Ltp == handler.Lap ? handler.Bsq : 0;

                if (lastStateHandler != null)
                    handler.BL = handler.Bsp == handler.Lap ? (lastStateHandler.BL >= AV ? lastStateHandler.BL : AV) : 0;
                else
                    handler.BL = 0;

                var AZ = handler.Nst;
                double Signal2 = AZ == 0 ? 0 : handler.BL / AZ;


                double BK = handler.Bsp == handler.Lap ? (AZ == 0 ? 0 : handler.Bsq / AZ) : 0;
                double BM = Signal2;
                double Signal3 = BM == 0 ? 0 : BK / BM;


                long BB = 0;
                long AX = 0;
                if (lastStateHandler != null)
                {
                    BB = AZ - lastStateHandler.Nst;
                    AX = (lastStateHandler.AV != 0 && handler.AV != 0) ? (handler.AV - lastStateHandler.AV) : 0;
                }


                double BI = handler.Bsp == handler.Lap ? (handler.Bsq == 0 ? 0 : ((-1 * AX) / handler.Bsq)) : 0;
                double BG = AV == 0 ? 0 : (handler.Bsp == handler.Lap ? BB / AV : 0);
                double Signal4 = BI + BG;


                double Signal5 = 0;

                if (lastStateHandler != null)
                {
                    long BX = handler.Bsq - lastStateHandler.Bsq;
                    long BZ = handler.Nst - lastStateHandler.Nst;
                    double CA = lastStateHandler.CA + BX + BZ;
                    handler.CA = CA;
                    Signal5 = handler.Nst == 0 ? 0 : ((CA / handler.Nst) < 0 ? (CA / handler.Nst) : 0);
                }


                handler.AV = AV;
                handler.Signal1 = Signal1;
                handler.Signal2 = Signal2 * 100;
                handler.Signal3 = Signal3 * 100;
                handler.Signal4 = Signal4 * 100;
                handler.Signal5 = Signal5 * 100;
                handler.IsSelectedForShow = handler.Bsp == handler.Lap ? true : false;
                handler.CapecityOfSelectedShow = handler.Bsq;
            });
        }

        public static void LastStateUpdater(this List<ChartViewModel> lastChart, double[][] matrix)
        {
            DateTime ReceivedDate = DateTime.Now;

            Parallel.For(0, matrix.GetLength(0), index =>
            {

                var handler = lastChart.FirstOrDefault(x => x.InstrumentId == matrix[index][0]);
                var lastStateHandler = handler.ShallowCopy();
                handler.ReceivedDate = ReceivedDate;
                handler.Cp = (long)matrix[index][10];
                handler.Ltp = (long)matrix[index][8];
                handler.Nst = (long)matrix[index][7];

                handler.EFF = handler.Cp != 0 ? (handler.Ltp - handler.Cp) / (double)handler.Cp : 0;
                handler.Delta = handler.Nst - lastStateHandler.Nst;
                handler.DiffParam = handler.Nst != 0 ? (handler.EFF * handler.Delta) / handler.Nst : 0;
                handler.CumulativeDiff = lastStateHandler.CumulativeDiff * (1 + handler.DiffParam);
                handler.NormCumulativeDiff = 5000 * handler.CumulativeDiff - 5000;
            });
        }
    }
}