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
    public static class ConfirmedOrderMapper
    {
        public static void ToAnalizing(this ConfirmedOrderViewModel item, ConfirmedOrderViewModel lastParam, List<RecordSaverViewModel> recordSavers, bool hasClientData, long cp0, double[][] matrix, int index)
        {

            item.Ns = item.SalesOrderCount_1 + item.SalesOrderCount_2 + item.SalesOrderCount_3 + item.SalesOrderCount_4 + item.SalesOrderCount_5;
            item.Qs = item.SalesOrderVolume_1 + item.SalesOrderVolume_2 + item.SalesOrderVolume_3 + item.SalesOrderVolume_4 + item.SalesOrderVolume_5;
            item.Ps = ((item.SalesOrderVolume_1 * item.SalesOrderPrice_1) + (item.SalesOrderVolume_2 * item.SalesOrderPrice_2) + (item.SalesOrderVolume_3 * item.SalesOrderPrice_3) +
                (item.SalesOrderVolume_4 * item.SalesOrderPrice_4) + (item.SalesOrderVolume_5 * item.SalesOrderPrice_5)) / (double)item.Qs;

            item.Nb = item.PurchaseOrderCount_1 + item.PurchaseOrderCount_2 + item.PurchaseOrderCount_3 + item.PurchaseOrderCount_4 + item.PurchaseOrderCount_5;
            item.Qb = item.PurchaseOrderVolume_1 + item.PurchaseOrderVolume_2 + item.PurchaseOrderVolume_3 + item.PurchaseOrderVolume_4 + item.PurchaseOrderVolume_5;
            item.Pb = ((item.PurchaseOrderVolume_1 * item.PurchaseOrderPrice_1) + (item.PurchaseOrderVolume_2 * item.PurchaseOrderPrice_2) + (item.PurchaseOrderVolume_3 * item.PurchaseOrderPrice_3) +
                (item.PurchaseOrderVolume_4 * item.PurchaseOrderPrice_4) + (item.PurchaseOrderVolume_5 * item.PurchaseOrderPrice_5)) / (double)item.Qb;

            if (double.IsNaN(item.Ps))
            {
                item.Qs = 1;
                item.Ps = item.Pb;
            }

            if (double.IsNaN(item.Pb))
            {
                item.Qb = 1;
                item.Pb = item.Ps;
            }


            item.Sv = item.Qs * item.Ps;
            item.Bv = item.Qb * item.Pb;

            item.Wp = (item.Sv + item.Bv) / (item.Qs + item.Qb);

            item.Sqpc = (double)item.Qs / item.Ns;
            if (double.IsNaN(item.Sqpc) || double.IsInfinity(item.Sqpc))
                item.Sqpc = 0;

            item.Bqpc = (double)item.Qb / item.Nb;
            if (double.IsNaN(item.Bqpc) || double.IsInfinity(item.Bqpc))
                item.Bqpc = 0;

            item.Qpcd = item.Bqpc - item.Sqpc;


            if (lastParam is null)
            {
                item.Cptqpcd = item.Qpcd;
            }
            else
            {
                if (item.Qb == lastParam.Qb)
                    item.Cptqpcd = lastParam.Cptqpcd;
                else
                    item.Cptqpcd = lastParam.Cptqpcd + item.Qpcd;
            }


            item.Ewpcp = ((item.Wp - item.Cp) / item.Cp) + ((item.Cp - cp0) / cp0);
            if (double.IsInfinity(item.Ewpcp) || double.IsNaN(item.Ewpcp))
                item.Ewpcp = 0;


            if (hasClientData)
            {
                item.Ltppbpsd = ((item.Ps + item.Pb) / 2) - item.Ltp;
                if (double.IsInfinity(item.Ltppbpsd) || double.IsNaN(item.Ltppbpsd))
                    item.Ltppbpsd = 0;

                item.Ltppbpsdp = item.Ltppbpsd / item.Ltp;
                if (double.IsInfinity(item.Ltppbpsdp) || double.IsNaN(item.Ltppbpsdp))
                    item.Ltppbpsdp = 0;

                if (lastParam is null)
                {
                    item.Cltppbpsdp = 0;
                }
                else
                {
                    item.Cltppbpsdp = lastParam.Cltppbpsdp + item.Ltppbpsdp;
                }
            }
            else
            {
                item.Ltppbpsd = 0;
                item.Ltppbpsdp = 0;
                item.Cltppbpsdp = 0;
            }


            matrix[index][1] = item.Bv;
            matrix[index][2] = item.Sv;
            matrix[index][3] = item.Ewpcp;

            var clonedMatrix = matrix.Clone() as double[][];
            int matrixLength = clonedMatrix.Length;
            double SumEwpcp = 0, SumBv = 0, SumSv = 0;

            for (int i = 0; i < matrixLength; i++)
            {
                SumBv += clonedMatrix[i][1];
                SumSv += clonedMatrix[i][2];
                SumEwpcp += clonedMatrix[i][3];
            }

            item.Tavewpcp = SumEwpcp / matrixLength;
            if (double.IsInfinity(item.Tavewpcp) || double.IsNaN(item.Tavewpcp))
                item.Tavewpcp = 0;


            recordSavers.Add(new RecordSaverViewModel()
            {
                InsertDate = DateTime.Now,
                Qpcd = item.Qpcd
            });

            if (recordSavers.Count <= 15)
            {
                item.Av15qpcd = 0;
            }
            else
            {
                var record = recordSavers.OrderBy(x => x.InsertDate).First();
                recordSavers.Remove(record);
                item.Av15qpcd = recordSavers.Average(x => x.Qpcd);
            }
        }

        public static ConfirmedOrder_T ToEntityModel(this ConfirmedOrderViewModel item, DateTime receivedDate)
        {
            return new ConfirmedOrder_T
            {
                InstrumentId = item.InstrumentId,
                ReceivedDate = receivedDate,
                Bv = item.Bv,
                Nb = item.Nb,
                Ns = item.Ns,
                Pb = item.Pb,
                Ps = item.Ps,
                PurchaseOrderCount_1 = item.PurchaseOrderCount_1,
                PurchaseOrderCount_2 = item.PurchaseOrderCount_2,
                PurchaseOrderCount_3 = item.PurchaseOrderCount_3,
                PurchaseOrderCount_4 = item.PurchaseOrderCount_4,
                PurchaseOrderCount_5 = item.PurchaseOrderCount_5,
                PurchaseOrderPrice_1 = item.PurchaseOrderPrice_1,
                PurchaseOrderPrice_2 = item.PurchaseOrderPrice_2,
                PurchaseOrderPrice_3 = item.PurchaseOrderPrice_3,
                PurchaseOrderPrice_4 = item.PurchaseOrderPrice_4,
                PurchaseOrderPrice_5 = item.PurchaseOrderPrice_5,
                PurchaseOrderVolume_1 = item.PurchaseOrderVolume_1,
                PurchaseOrderVolume_2 = item.PurchaseOrderVolume_2,
                PurchaseOrderVolume_3 = item.PurchaseOrderVolume_3,
                PurchaseOrderVolume_4 = item.PurchaseOrderVolume_4,
                PurchaseOrderVolume_5 = item.PurchaseOrderVolume_5,
                Qb = item.Qb,
                Qs = item.Qs,
                SalesOrderCount_1 = item.SalesOrderCount_1,
                SalesOrderCount_2 = item.SalesOrderCount_2,
                SalesOrderCount_3 = item.SalesOrderCount_3,
                SalesOrderCount_4 = item.SalesOrderCount_4,
                SalesOrderCount_5 = item.SalesOrderCount_5,
                SalesOrderPrice_1 = item.SalesOrderPrice_1,
                SalesOrderPrice_2 = item.SalesOrderPrice_2,
                SalesOrderPrice_3 = item.SalesOrderPrice_3,
                SalesOrderPrice_4 = item.SalesOrderPrice_4,
                SalesOrderPrice_5 = item.SalesOrderPrice_5,
                SalesOrderVolume_1 = item.SalesOrderVolume_1,
                SalesOrderVolume_2 = item.SalesOrderVolume_2,
                SalesOrderVolume_3 = item.SalesOrderVolume_3,
                SalesOrderVolume_4 = item.SalesOrderVolume_4,
                SalesOrderVolume_5 = item.SalesOrderVolume_5,
                Sv = item.Sv,
                Wp = item.Wp,
                Ltp = item.Ltp,
                Cp = item.Cp,
                Av15qpcd = item.Av15qpcd,
                Bqpc = item.Bqpc,
                Cptqpcd = item.Cptqpcd,
                Qpcd = item.Qpcd,
                Sqpc = item.Sqpc,
                Ewpcp = item.Ewpcp,
                Tavewpcp = item.Tavewpcp,
                Ltppbpsd = item.Ltppbpsd,
                Ltppbpsdp = item.Ltppbpsdp,
                Cltppbpsdp = item.Cltppbpsdp
            };
        }

        public static ConfirmedOrderNotifModel ToNotifModel(this ConfirmedOrderViewModel item, DateTime receivedDate, bool hasClientData)
        {
            return new ConfirmedOrderNotifModel
            {
                InstrumentId = item.InstrumentId,
                ReceivedDate = receivedDate.ToString("HH:mm:ss"),
                Wp = item.Wp,
                Ltp = item.Ltp,
                Cp = item.Cp,
                Qpcd = item.Qpcd,
                Av15qpcd = item.Av15qpcd,
                Cptqpcd = item.Cptqpcd,
                Ewpcp = item.Ewpcp,
                Tavewpcp = item.Tavewpcp,
                Cltppbpsdp = hasClientData ? item.Cltppbpsdp : double.NaN,
                orderGridModel = item.ToGridModel()
            };
        }

        public static ConfirmOrderGridModel ToGridModel(this ConfirmedOrderViewModel item)
        {
            return new ConfirmOrderGridModel()
            {
                PurchaseOrderCount_1 = item.PurchaseOrderCount_1,
                PurchaseOrderCount_2 = item.PurchaseOrderCount_2,
                PurchaseOrderCount_3 = item.PurchaseOrderCount_3,
                PurchaseOrderCount_4 = item.PurchaseOrderCount_4,
                PurchaseOrderCount_5 = item.PurchaseOrderCount_5,
                PurchaseOrderPrice_1 = item.PurchaseOrderPrice_1,
                PurchaseOrderPrice_2 = item.PurchaseOrderPrice_2,
                PurchaseOrderPrice_3 = item.PurchaseOrderPrice_3,
                PurchaseOrderPrice_4 = item.PurchaseOrderPrice_4,
                PurchaseOrderPrice_5 = item.PurchaseOrderPrice_5,
                PurchaseOrderVolume_1 = item.PurchaseOrderVolume_1,
                PurchaseOrderVolume_2 = item.PurchaseOrderVolume_2,
                PurchaseOrderVolume_3 = item.PurchaseOrderVolume_3,
                PurchaseOrderVolume_4 = item.PurchaseOrderVolume_4,
                PurchaseOrderVolume_5 = item.PurchaseOrderVolume_5,
                SalesOrderCount_1 = item.SalesOrderCount_1,
                SalesOrderCount_2 = item.SalesOrderCount_2,
                SalesOrderCount_3 = item.SalesOrderCount_3,
                SalesOrderCount_4 = item.SalesOrderCount_4,
                SalesOrderCount_5 = item.SalesOrderCount_5,
                SalesOrderPrice_1 = item.SalesOrderPrice_1,
                SalesOrderPrice_2 = item.SalesOrderPrice_2,
                SalesOrderPrice_3 = item.SalesOrderPrice_3,
                SalesOrderPrice_4 = item.SalesOrderPrice_4,
                SalesOrderPrice_5 = item.SalesOrderPrice_5,
                SalesOrderVolume_1 = item.SalesOrderVolume_1,
                SalesOrderVolume_2 = item.SalesOrderVolume_2,
                SalesOrderVolume_3 = item.SalesOrderVolume_3,
                SalesOrderVolume_4 = item.SalesOrderVolume_4,
                SalesOrderVolume_5 = item.SalesOrderVolume_5
            };
        }

        public static bool HasChanged(this ConfirmedOrderViewModel item, ConfirmedOrderViewModel lastParam)
        {
            try
            {
                if (item.PurchaseOrderCount_1 == 0 && item.PurchaseOrderCount_2 == 0 && item.PurchaseOrderCount_3 == 0 && item.PurchaseOrderCount_4 == 0 && item.PurchaseOrderCount_5 == 0 &&
                  item.SalesOrderCount_1 == 0 && item.SalesOrderCount_2 == 0 && item.SalesOrderCount_3 == 0 && item.SalesOrderCount_4 == 0 && item.SalesOrderCount_5 == 0)
                    return false;

                if (lastParam is null)
                    return true;

                #region checkParam               

                //PurchaseOrderVolume
                if (item.PurchaseOrderVolume_1 != lastParam.PurchaseOrderVolume_1)
                    return true;

                if (item.PurchaseOrderVolume_2 != lastParam.PurchaseOrderVolume_2)
                    return true;

                if (item.PurchaseOrderVolume_3 != lastParam.PurchaseOrderVolume_3)
                    return true;

                if (item.PurchaseOrderVolume_4 != lastParam.PurchaseOrderVolume_4)
                    return true;

                if (item.PurchaseOrderVolume_5 != lastParam.PurchaseOrderVolume_5)
                    return true;



                //SalesOrderVolume
                if (item.SalesOrderVolume_1 != lastParam.SalesOrderVolume_1)
                    return true;

                if (item.SalesOrderVolume_2 != lastParam.SalesOrderVolume_2)
                    return true;

                if (item.SalesOrderVolume_3 != lastParam.SalesOrderVolume_3)
                    return true;

                if (item.SalesOrderVolume_4 != lastParam.SalesOrderVolume_4)
                    return true;

                if (item.SalesOrderVolume_5 != lastParam.SalesOrderVolume_5)
                    return true;


                //PurchaseOrderPrice
                if (item.PurchaseOrderPrice_1 != lastParam.PurchaseOrderPrice_1)
                    return true;

                if (item.PurchaseOrderPrice_2 != lastParam.PurchaseOrderPrice_2)
                    return true;

                if (item.PurchaseOrderPrice_3 != lastParam.PurchaseOrderPrice_3)
                    return true;

                if (item.PurchaseOrderPrice_4 != lastParam.PurchaseOrderPrice_4)
                    return true;

                if (item.PurchaseOrderPrice_5 != lastParam.PurchaseOrderPrice_5)
                    return true;


                //SalesOrderPrice
                if (item.SalesOrderPrice_1 != lastParam.SalesOrderPrice_1)
                    return true;

                if (item.SalesOrderPrice_2 != lastParam.SalesOrderPrice_2)
                    return true;

                if (item.SalesOrderPrice_3 != lastParam.SalesOrderPrice_3)
                    return true;

                if (item.SalesOrderPrice_4 != lastParam.SalesOrderPrice_4)
                    return true;

                if (item.SalesOrderPrice_5 != lastParam.SalesOrderPrice_5)
                    return true;

                #endregion

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void ClientStatusCheck(this ConfirmedOrderViewModel item, ConfirmedOrderViewModel lastParam, bool hasClientData)
        {
            try
            {
                if (lastParam != null && lastParam.HasClientData)
                    item.HasClientData = true;
                else
                    item.HasClientData = hasClientData;
            }
            catch (Exception)
            {
                item.HasClientData = false;
            }
        }
    }
}
