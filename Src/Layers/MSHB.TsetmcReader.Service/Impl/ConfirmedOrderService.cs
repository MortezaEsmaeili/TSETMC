using MSHB.TsetmcReader.DataLayer.DataModels;
using MSHB.TsetmcReader.DTO.ViewModels;
using MSHB.TsetmcReader.Service.Contract;
using MSHB.TsetmcReader.Service.Helper;
using MSHB.TsetmcReader.Service.Mapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MSHB.TsetmcReader.Service.Impl
{
    public class ConfirmedOrderService : IConfirmedOrderService
    {
        INotificationService _notificationService;
        IInstrumentService _instrumentService;
        Dictionary<string, long> _instrumentIds;
        CancellationTokenSource _cancellationTokenSource;
        CancellationToken _cancellationToken;
        int _threadSleepTime;
        private double[][] _matrix;
        ConcurrentDictionary<long, int> _indexes;



        public ConfirmedOrderService(INotificationService notificationService, IInstrumentService instrumentService, int threadSleepTime)
        {
            _threadSleepTime = threadSleepTime;
            _notificationService = notificationService;
            _instrumentService = instrumentService;
            _instrumentIds = _instrumentService.GetDictionary();
            _indexes = new ConcurrentDictionary<long, int>();
        }


        public void Start()
        {
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationToken = _cancellationTokenSource.Token;

                _matrix = new double[_instrumentIds.Count][];
                int index = 0;
                foreach (var item in _instrumentIds)
                {
                    _matrix[index] = new double[4];
                    _matrix[index][0] = item.Value;    // InstrumentId
                    _matrix[index][1] = 0;             // Bv
                    _matrix[index][2] = 0;             // Sv    
                    _matrix[index][3] = 0;             // EWpCp

                    _indexes.TryAdd(item.Value, index);
                    index++;
                }

                foreach (var item in _instrumentIds)
                {
                    DoJobAsync(item);
                    Thread.Sleep(100);
                }
            }
            catch (Exception) { }
        }

        public void Stop()
        {
            try
            {
                _cancellationTokenSource.Cancel();
            }
            catch (Exception) { }
        }


        private Task DoJobAsync(KeyValuePair<string, long> item)
        {
            return Task.Factory.StartNew(() =>
             {
                 var _context = new StockMonitorDbContext();
                 var stopWatch = new System.Diagnostics.Stopwatch();
                 ConfirmedOrderViewModel _lastState = null;
                 List<RecordSaverViewModel> _recordSavers = new List<RecordSaverViewModel>();
                 long Cp0 = 0;

                 while (!_cancellationToken.IsCancellationRequested)
                 {
                     stopWatch.Restart();
                     try
                     {
                         var info = RESTHelper.CallGetMethod($"http://www.tsetmc.com/tsev2/data/instinfodata.aspx?i={item.Key}&c=0&e=0");
                         if (!string.IsNullOrEmpty(info))
                         {
                             var data = info.Split(';');
                             if (data.Length == 9)
                             {
                                 string text1 = data[2].Trim().Replace(",", "@");
                                 var param_Part1 = text1.Split('@').Select(x => x.Trim()).ToArray();
                                 var param_Part2 = data[4].Trim().Split(',').Select(x => x.Trim()).ToArray();
                                 var param_Part3 = data[0].Trim().Split(',').Select(x => x.Trim()).ToArray();


                                 Nullable<DateTime> orderInsertedTime = null;
                                 if (param_Part1.Length == 31)
                                 {
                                     ConfirmedOrderViewModel model = new ConfirmedOrderViewModel();
                                     model.InstrumentId = item.Value;

                                     #region Rows

                                     ///Row_1                                  

                                     if (long.TryParse(param_Part1[0], out long PurchaseOrderCount_1))
                                         model.PurchaseOrderCount_1 = PurchaseOrderCount_1;

                                     if (long.TryParse(param_Part1[1], out long PurchaseOrderVolume_1))
                                         model.PurchaseOrderVolume_1 = PurchaseOrderVolume_1;

                                     if (long.TryParse(param_Part1[2], out long PurchaseOrderPrice_1))
                                         model.PurchaseOrderPrice_1 = PurchaseOrderPrice_1;

                                     if (long.TryParse(param_Part1[3], out long SalesOrderPrice_1))
                                         model.SalesOrderPrice_1 = SalesOrderPrice_1;

                                     if (long.TryParse(param_Part1[4], out long SalesOrderVolume_1))
                                         model.SalesOrderVolume_1 = SalesOrderVolume_1;

                                     if (long.TryParse(param_Part1[5], out long SalesOrderCount_1))
                                         model.SalesOrderCount_1 = SalesOrderCount_1;


                                     //Row_2

                                     if (long.TryParse(param_Part1[6], out long PurchaseOrderCount_2))
                                         model.PurchaseOrderCount_2 = PurchaseOrderCount_2;

                                     if (long.TryParse(param_Part1[7], out long PurchaseOrderVolume_2))
                                         model.PurchaseOrderVolume_2 = PurchaseOrderVolume_2;

                                     if (long.TryParse(param_Part1[8], out long PurchaseOrderPrice_2))
                                         model.PurchaseOrderPrice_2 = PurchaseOrderPrice_2;

                                     if (long.TryParse(param_Part1[9], out long SalesOrderPrice_2))
                                         model.SalesOrderPrice_2 = SalesOrderPrice_2;

                                     if (long.TryParse(param_Part1[10], out long SalesOrderVolume_2))
                                         model.SalesOrderVolume_2 = SalesOrderVolume_2;

                                     if (long.TryParse(param_Part1[11], out long SalesOrderCount_2))
                                         model.SalesOrderCount_2 = SalesOrderCount_2;


                                     //Row_3

                                     if (long.TryParse(param_Part1[12], out long PurchaseOrderCount_3))
                                         model.PurchaseOrderCount_3 = PurchaseOrderCount_3;

                                     if (long.TryParse(param_Part1[13], out long PurchaseOrderVolume_3))
                                         model.PurchaseOrderVolume_3 = PurchaseOrderVolume_3;

                                     if (long.TryParse(param_Part1[14], out long PurchaseOrderPrice_3))
                                         model.PurchaseOrderPrice_3 = PurchaseOrderPrice_3;

                                     if (long.TryParse(param_Part1[15], out long SalesOrderPrice_3))
                                         model.SalesOrderPrice_3 = SalesOrderPrice_3;

                                     if (long.TryParse(param_Part1[16], out long SalesOrderVolume_3))
                                         model.SalesOrderVolume_3 = SalesOrderVolume_3;

                                     if (long.TryParse(param_Part1[17], out long SalesOrderCount_3))
                                         model.SalesOrderCount_3 = SalesOrderCount_3;


                                     //Row_4

                                     if (long.TryParse(param_Part1[18], out long PurchaseOrderCount_4))
                                         model.PurchaseOrderCount_4 = PurchaseOrderCount_4;

                                     if (long.TryParse(param_Part1[19], out long PurchaseOrderVolume_4))
                                         model.PurchaseOrderVolume_4 = PurchaseOrderVolume_4;

                                     if (long.TryParse(param_Part1[20], out long PurchaseOrderPrice_4))
                                         model.PurchaseOrderPrice_4 = PurchaseOrderPrice_4;

                                     if (long.TryParse(param_Part1[21], out long SalesOrderPrice_4))
                                         model.SalesOrderPrice_4 = SalesOrderPrice_4;

                                     if (long.TryParse(param_Part1[22], out long SalesOrderVolume_4))
                                         model.SalesOrderVolume_4 = SalesOrderVolume_4;

                                     if (long.TryParse(param_Part1[23], out long SalesOrderCount_4))
                                         model.SalesOrderCount_4 = SalesOrderCount_4;


                                     //Row_5

                                     if (long.TryParse(param_Part1[24], out long PurchaseOrderCount_5))
                                         model.PurchaseOrderCount_5 = PurchaseOrderCount_5;

                                     if (long.TryParse(param_Part1[25], out long PurchaseOrderVolume_5))
                                         model.PurchaseOrderVolume_5 = PurchaseOrderVolume_5;

                                     if (long.TryParse(param_Part1[26], out long PurchaseOrderPrice_5))
                                         model.PurchaseOrderPrice_5 = PurchaseOrderPrice_5;

                                     if (long.TryParse(param_Part1[27], out long SalesOrderPrice_5))
                                         model.SalesOrderPrice_5 = SalesOrderPrice_5;

                                     if (long.TryParse(param_Part1[28], out long SalesOrderVolume_5))
                                         model.SalesOrderVolume_5 = SalesOrderVolume_5;

                                     if (long.TryParse(param_Part1[29], out long SalesOrderCount_5))
                                         model.SalesOrderCount_5 = SalesOrderCount_5;

                                     #endregion

                                     if (param_Part3.Length == 16)
                                     {

                                         if (long.TryParse(param_Part3[2], out long Ltp))
                                             model.Ltp = Ltp;

                                         if (long.TryParse(param_Part3[3], out long Cp))
                                             model.Cp = Cp;

                                         if (Cp0 == 0)
                                             long.TryParse(param_Part3[5], out Cp0);
                                     }


                                     if (model.HasChanged(_lastState))
                                     {
                                         model.ClientStatusCheck(_lastState, param_Part2.Length == 10);

                                         _indexes.TryGetValue(model.InstrumentId, out int index);
                                         {
                                             model.ToAnalizing(_lastState, _recordSavers, model.HasClientData, Cp0, _matrix, index);

                                             orderInsertedTime = DateTime.Now;
                                             _context.ConfirmedOrder_T.Add(model.ToEntityModel(orderInsertedTime.Value));

                                             _notificationService.ConfirmedOrderNotify(item.Value, model.ToNotifModel(orderInsertedTime.Value, model.HasClientData));
                                             _lastState = model;
                                         }
                                     }
                                 }

                                 if (param_Part2.Length == 10 && orderInsertedTime.HasValue)
                                 {
                                     ClientHandlerViewModel clientHandlerModel = new ClientHandlerViewModel();
                                     clientHandlerModel.InstrumentId = item.Value;

                                     #region Rows

                                     if (long.TryParse(param_Part2[0], out long Ib_nst))
                                         clientHandlerModel.Ib_nst = Ib_nst;

                                     if (long.TryParse(param_Part2[1], out long Nb_nst))
                                         clientHandlerModel.Nb_nst = Nb_nst;

                                     if (long.TryParse(param_Part2[3], out long Is_nst))
                                         clientHandlerModel.Is_nst = Is_nst;

                                     if (long.TryParse(param_Part2[4], out long Ns_nst))
                                         clientHandlerModel.Ns_nst = Ns_nst;

                                     if (long.TryParse(param_Part2[5], out long Ibn))
                                         clientHandlerModel.Ibn = Ibn;

                                     if (long.TryParse(param_Part2[6], out long Nbn))
                                         clientHandlerModel.Nbn = Nbn;

                                     if (long.TryParse(param_Part2[8], out long Isn))
                                         clientHandlerModel.Isn = Isn;

                                     if (long.TryParse(param_Part2[9], out long Nsn))
                                         clientHandlerModel.Nsn = Nsn;

                                     #endregion

                                     _context.ClientHandler_T.Add(clientHandlerModel.ToEntityModel(orderInsertedTime.Value));
                                 }

                                 try
                                 {
                                     _context.SaveChanges();
                                 }
                                 catch { _context = new StockMonitorDbContext(); }
                             }
                         }
                     }
                     catch (Exception) { }

                     stopWatch.Stop();
                     if (stopWatch.ElapsedMilliseconds <= _threadSleepTime)
                     {
                         Thread.Sleep(_threadSleepTime - (int)stopWatch.ElapsedMilliseconds);
                     }
                 }
             }, _cancellationToken);
        }
    }
}
