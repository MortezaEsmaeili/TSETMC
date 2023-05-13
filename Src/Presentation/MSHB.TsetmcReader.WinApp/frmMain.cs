using MSHB.TsetmcReader.DTO.FormModels;
using MSHB.TsetmcReader.DTO.ViewModels;
using MSHB.TsetmcReader.Service.Contract;
using MSHB.TsetmcReader.Service.Helper;
using MSHB.TsetmcReader.Service.Impl;
using MSHB.TsetmcReader.Service.Mapper;
using MSHB.TsetmcReader.WinApp.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Zu.AsyncWebDriver.Remote;
//using Zu.Chrome;
//using Zu.ChromeDevTools.Network;

//removed packages packages= "AsyncChromeDriver" version = "0.5.8" 
//< package id = "Zu.ChromeDevToolsClient" version = "0.5.6" targetFramework = "net461" requireReinstallation = "true" /> 

namespace MSHB.TsetmcReader.WinApp
{
    public partial class frmMain : Form
    {
        
        TestmcDriver _testmcDriver = null;
        IIncomingService _incomingService;
        IConfirmedOrderService _confirmedOrderService;
        IInstrumentService _instrumentService;
        IInformationHandlerService _informationHandlerService;
        INotificationService _notificationService;
        ITsetmcService _tsetmcService;
        List<string> _instrumentCodes;

        public frmMain()
        {
            _instrumentService = new InstrumentService();
            _informationHandlerService = new InformationHandlerService();
            _notificationService = new NotificationService(ConfigReaderHelper.GetNotificationUrl());
            _instrumentCodes = _instrumentService.GetInstrumentCodes();

            _tsetmcService = new TsetmcService();
            _incomingService = new IncomingService(_notificationService, _tsetmcService, ConfigReaderHelper.GetChartChangesTime());
            _confirmedOrderService = new ConfirmedOrderService(_notificationService, _instrumentService, ConfigReaderHelper.GetThreadSleepTime());
            //string url = "http://www.tsetmc.com/loader.aspx?ParTree=15131F";
            string url = ConfigReaderHelper.GetWebsiteUrl();
            _testmcDriver = TestmcDriver.Instance(url, _incomingService);
            _testmcDriver.UpdateStatusEvent += UpdateStatus;
            InitializeComponent();
        }
        private void UpdateStatus(string status)
        {
            Action action = () => { lbStatus.Text = status; };
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void tmClock_Tick(object sender, EventArgs e)
        {
            tsmTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void tmServiceChecker_Tick(object sender, EventArgs e)
        {
            tmServiceChecker.Enabled = false;
            try
            {
                TimeSpan startTime = TimeSpan.Parse(ConfigReaderHelper.GetStartTime());
                TimeSpan endTime = TimeSpan.Parse(ConfigReaderHelper.GetEndTime());
                var currentTime = DateTime.Now.TimeOfDay;

                if (currentTime >= startTime && currentTime <= endTime)
                {
                    if (!_incomingService.GetStatus())
                        tsmActiveListener_Click(null, null);
                }
                else
                {
                    if (_incomingService.GetStatus())
                        tsmActiveListener_Click(null, null);
                }
            }
            catch (Exception) { }

            tmServiceChecker.Enabled = true;
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("آیا میخواهید از برنامه خارج شوید؟", "خرج از برنامه", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            if(_testmcDriver!=null)
               await _testmcDriver.StopInterceptorAsync();
           

        }

        private async void tsmOpenBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                if (_testmcDriver == null)
                {
                    string url = ConfigReaderHelper.GetWebsiteUrl();
                    _testmcDriver = TestmcDriver.Instance(url, _incomingService);
                    _testmcDriver.UpdateStatusEvent += UpdateStatus;
                }
                else
                    MessageBox.Show("مرورگر مشابه ای در حال اجراست");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void tsmMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIconManage.Visible = true;
                notifyIconManage.BalloonTipTitle = "مشاهده گر بورس";
                notifyIconManage.BalloonTipText = "مشاهده گر اطلاعات آنلاین بورس";
                notifyIconManage.BalloonTipIcon = ToolTipIcon.Info;
                notifyIconManage.ShowBalloonTip(2000);
            }
        }

        private void notifyIconManage_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            notifyIconManage.Visible = false;
            this.ShowInTaskbar = true;
        }

        async Task<bool> RunMonitoringAsync()
        {
            try
            {
                if (!await _notificationService.IsConnectedAsync())
                {
                    lbStatus.Text = "خـطا در اتصال به سرویس انتقال پیام ...";
                    MessageBox.Show("خـطا در اتصال به سرویس انتقال پیام ...");
                    return false;
                }

               
                lbStatus.Text = "Listen to Network Started ...";

                return true;
            }
            catch (Exception ex)
            {
                lbStatus.Text = ex.Message;
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        async void StopMonitoring()
        {
            await _testmcDriver.StopInterceptorAsync(); 
            if (_incomingService != null)
            {
                _incomingService.Stop();
                _confirmedOrderService.Stop();
            }

            lbStatus.Text = "Listen to Websocket Stopped ...";
        }

        private async void tsmActiveListener_Click(object sender, EventArgs e)
        {
            lbStatus.Text = "Ready to Connecting ...";

            if (_instrumentCodes is null || _instrumentCodes.Count == 0)
            {
                MessageBox.Show("نمادی در سیستم وجود ندارد");
                return;
            }

            
            if (_testmcDriver is null)
            {
                MessageBox.Show("ابتدا مرورگر را از قسمت اطلاعات پایه بارگذاری کنید");
                return;
            }

            string Url = "https://core.tadbirrlc.com//StockInformationHandler.ashx?{%22Type%22:%22getstockprice2%22,%22la%22:%22Fa%22,%22arr%22:%22RowIds%22}&jsoncallback=angular.callbacks._1";
            if (string.IsNullOrEmpty(Url))
            {
                lbStatus.Text = "Url does not exist ...";
                MessageBox.Show("Url does not exist ...");
                return;
            }

            var rowIds = _tsetmcService.GetRowIds(_instrumentCodes);
            Url = Url.Replace("RowIds", string.Join(",", rowIds));

            List<InformationHandlerViewModel> lastState = new List<InformationHandlerViewModel>();
            List<ChartViewModel> lastChart = new List<ChartViewModel>();

            lbStatus.Text = "Start Fetching Data ...";
            var informationHandlers = RESTHelper.CallGetMethod<List<InformationHandlerFormModel>>(Url, out string restError);
            if (string.IsNullOrEmpty(restError))
            {
                var addModel = new AddInformationHandlerFormModel()
                {
                    informationHandlers = informationHandlers,
                    ReceivedDate = DateTime.Now
                };

                lastState = _informationHandlerService.Insert(addModel);
                lastChart = lastState.ToChartModel();
                if (lastState.Count == 0)
                {
                    lbStatus.Text = "خطا در ثبت آخرین داده های کارگزاری";
                    MessageBox.Show("خطا در ثبت آخرین داده های کارگزاری");
                    return;
                }
            }
            else
            {
                MessageBox.Show(restError);
                return;
            }

            lbStatus.Text = "LastState is Ready for use ...";

            if (tsmActiveListener.Tag.ToString() == "0")
            {
                _incomingService.Start(lastState, lastChart);
                _confirmedOrderService.Start();
                if (await RunMonitoringAsync())
                {
                    tsmActiveListener.Tag = 1;
                    tsmActiveListener.ForeColor = Color.Green;
                    tsmActiveListener.Text = "وضعیت دریافت کننده: فعال";
                }
            }
            else
            {
                StopMonitoring();
                tsmActiveListener.Tag = 0;
                tsmActiveListener.ForeColor = Color.Red;
                tsmActiveListener.Text = "وضعیت دریافت کننده: غیرفعال";
            }
        }
                
    }
}
