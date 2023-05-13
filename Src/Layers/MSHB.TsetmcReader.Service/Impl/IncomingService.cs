using MSHB.TsetmcReader.DataLayer.DataModels;
using MSHB.TsetmcReader.DTO.ViewModels;
using MSHB.TsetmcReader.Service.Contract;
using MSHB.TsetmcReader.Service.Mapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MSHB.TsetmcReader.Service.Impl
{
    public class IncomingService : IIncomingService
    {
        INotificationService _notificationService;
        ITsetmcService _tsetmcService;
        private List<InformationHandlerViewModel> _lastState;
        private List<ChartViewModel> _lastChart;

        ConcurrentDictionary<string, int> _instrumentIds;
        private double[][] _matrix;
        private Timer _backupTimer;
        private Timer _notifTimer;
        private Timer _chartTimer;
        private bool _isBusy;


        public IncomingService(INotificationService notificationService, ITsetmcService tsetmcService, int chartChangesTime)
        {
            _isBusy = false;
            _notificationService = notificationService;
            _tsetmcService = tsetmcService;
            _instrumentIds = new ConcurrentDictionary<string, int>();

            _backupTimer = new System.Timers.Timer(5000);
            _backupTimer.Elapsed += _backupTimer_Elapsed;

            _notifTimer = new System.Timers.Timer(1000);
            _notifTimer.Elapsed += _notifTimer_Elapsed;

            _chartTimer = new System.Timers.Timer(chartChangesTime * 1000);
            _chartTimer.Elapsed += _chartTimer_Elapsed;
        }

        public void AddAsync(string responseMessage)
        {
            
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var logs = responseMessage.Trim().Split(';').Select(x => x.Trim()).ToArray();
                    if (logs != null && logs.Length > 0)
                    {
                       
                        Parallel.ForEach(logs, item =>
                        {
                            var data = item.Replace("'", "").Trim().Split(',').Select(x => x.Trim()).ToArray();
                            int dataLength = data.Length;
                            Trace.WriteLine($"len = {dataLength}; " +responseMessage);
                            if (dataLength == 8 || dataLength == 10)
                            {
                                if (_instrumentIds.TryGetValue(data[0], out int index))
                                {
                                    data.ToAnalizing(_matrix, index);
                                }
                            }
                        });
                    }
                }
                catch { }
            });
        }

        public bool GetStatus()
        {
            try
            {
                return _isBusy;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Start(List<InformationHandlerViewModel> lastState, List<ChartViewModel> lastChart)
        {
            try
            {
                _lastState = lastState;
                _lastChart = lastChart;

                _matrix = new double[_lastState.Count][];
                int index = 0;
                foreach (var item in _lastState.OrderBy(x => x.InstrumentId))
                {
                    _matrix[index] = new double[12];
                    _matrix[index][0] = item.InstrumentId;
                    _matrix[index][1] = item.Tv;
                    _matrix[index][2] = item.Bsq;
                    _matrix[index][3] = item.Bsp;
                    _matrix[index][4] = item.Bbq;
                    _matrix[index][5] = item.Bbp;
                    _matrix[index][6] = item.Nt;
                    _matrix[index][7] = item.Nst;
                    _matrix[index][8] = item.Ltp;
                    _matrix[index][9] = item.Lpvp;
                    _matrix[index][10] = item.Cp;
                    _matrix[index][11] = item.Cpvp;

                    _instrumentIds.TryAdd(item.Ic, index);
                    index++;
                }

                _backupTimer.Enabled = _notifTimer.Enabled = _chartTimer.Enabled = _isBusy = true;
            }
            catch (Exception) { }
        }

        public void Stop()
        {
            try
            {
                _backupTimer.Enabled = _notifTimer.Enabled = _chartTimer.Enabled = false;
            }
            catch (Exception) { }

            _isBusy = false;
        }

        private void _backupTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _backupTimer.Enabled = false;

            try
            {
                var _context = new StockMonitorDbContext();
                var insertModel = _lastState.ToEntityModel(DateTime.Now);
                _context.InformationHandler_T.BulkInsert(insertModel);
            }
            catch (Exception) { }

            _backupTimer.Enabled = true;
        }

        private void _notifTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _notifTimer.Enabled = false;
            try
            {
                var clonedMatrix = _matrix.Clone() as double[][];
                _lastState.LastStateUpdater(clonedMatrix);
                _notificationService.InformationHandlerNotifyAsync(_lastState.ToNotifModel());
            }
            catch (Exception) { }

            _notifTimer.Enabled = true;
        }

        private void _chartTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _chartTimer.Enabled = false;
            try
            {
                var clonedMatrix = _matrix.Clone() as double[][];
                _lastChart.LastStateUpdater(clonedMatrix);
                var IndexLastValue = _tsetmcService.MarketActivityLast();

                _notificationService.ChartNotifyAsync(new ChartNotifModel() { IndexLastValue = IndexLastValue, Properties = _lastChart.ToNotifModel() });
            }
            catch (Exception) { }

            _chartTimer.Enabled = true;
        }
    }
}
