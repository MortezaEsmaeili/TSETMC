using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.Threading;
using MSHB.TsetmcReader.Service.Contract;

namespace MSHB.TsetmcReader.WinApp
{
    internal class TestmcDriver
    {
        private ChromeDriver chromeDriver;
        private INetwork interceptor;
        private static TestmcDriver instance = null;
        IIncomingService _incomingService;
        public delegate void UpdateStatus(string status);
        public event UpdateStatus UpdateStatusEvent;
        public void OnUpdateStatus(string status)
        {
            if (UpdateStatusEvent != null)
                UpdateStatusEvent(status);
        }
        public static TestmcDriver Instance(string url, IIncomingService incomingService)
        {
            if(instance == null)
                instance = new TestmcDriver(url, incomingService);
            return instance;
        }
        private TestmcDriver(string url, IIncomingService incomingService)
        {
            _incomingService = incomingService;
            Task.Run(async () => await ChromeTest(url));
        }
        private async Task ChromeTest(string url)
        {
            try
            {

                var options = new ChromeOptions();
                chromeDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(2));

                interceptor = chromeDriver.Manage().Network;
                //interceptor.NetworkRequestSent += OnNetworkRequestSent;

                interceptor.NetworkResponseReceived += NetworkManager_NetworkResponseReceived;
                chromeDriver.Navigate().GoToUrl(url);
                Trace.WriteLine("Interceptor before starts monitoring");
                await interceptor.StartMonitoring();
                Trace.WriteLine("Interceptor starts monitoring");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        private void OnNetworkRequestSent(object sender, NetworkRequestSentEventArgs e)
        {
            Trace.WriteLine($"R s ReqId={e.RequestId}  ResponseUrl= {e.RequestUrl} ");
        }

        private void NetworkManager_NetworkResponseReceived(object sender, NetworkResponseReceivedEventArgs e)
        {
            Trace.WriteLine($"R R ReqId={e.RequestId}  ResponseUrl= {e.ResponseUrl} status={e.ResponseStatusCode} ");
            Listener(e);
        }
        public async Task StopInterceptorAsync()
        {
            await interceptor.StopMonitoring();
        }
        void Listener(NetworkResponseReceivedEventArgs e)
        {
            string url = e.ResponseUrl;
            string status = string.Empty;
            Task.Run( () =>
            {
                try
                {
                    OnUpdateStatus ( "Message Received");
                    int counter = 0;
                    string recievedData = string.Empty;
                    while (counter <= 3)
                    {
                        try { recievedData = e.ResponseBody; }//await asyncChromeDriver.DevTools.Network.GetResponseBody(new GetResponseBodyCommand() { RequestId = requestId }); }
                        catch { }

                        if (recievedData is null)
                            Thread.Sleep(100);

                        counter++;
                    }

                    if (recievedData is null)
                    {
                        OnUpdateStatus ( "Body is Null");
                        return;
                    }

                    if (url.Contains("MarketWatchPlus.aspx"))
                    {
                        _incomingService.AddAsync(recievedData);//recievedData.Body
                        OnUpdateStatus ( "Message has sent for Analizing");
                    }
                    else
                        OnUpdateStatus( "Url is Invalid");

                }
                catch (Exception ex)
                {
                    OnUpdateStatus ( ex.Message);
                }
            });
        }
    }
}
