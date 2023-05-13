using Microsoft.AspNetCore.SignalR.Client;
using MSHB.TsetmcReader.Service.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.Service.Impl
{
    public class NotificationService : INotificationService
    {
        JsonSerializerSettings _jsonSerializerSettings;
        HubConnection _hubConnection;
        private readonly object balanceLock = new object();

        public NotificationService(string SignalRUrl)
        {
            _jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            _hubConnection = new HubConnectionBuilder().WithUrl($"{SignalRUrl}MessengerHub").Build();
        }

        public async Task<bool> IsConnectedAsync()
        {
            if (_hubConnection.State == HubConnectionState.Connected)
                return true;

            try { await _hubConnection.StartAsync(); } catch { }

            return _hubConnection.State == HubConnectionState.Connected;
        }

        public async Task IncomingNotify(object model)
        {
            try
            {
                lock (balanceLock)
                {
                    if (_hubConnection.State == HubConnectionState.Disconnected)
                        _hubConnection.StartAsync();
                }

                string message = JsonConvert.SerializeObject(model, _jsonSerializerSettings);
                await _hubConnection.InvokeAsync("IncomingMessage", message);
            }
            catch (Exception)
            {
            }
        }

        public void InformationHandlerNotifyAsync(object model)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    lock (balanceLock)
                    {
                        if (_hubConnection.State == HubConnectionState.Disconnected)
                            _hubConnection.StartAsync();
                    }

                    string message = JsonConvert.SerializeObject(model, _jsonSerializerSettings);
                    await _hubConnection.InvokeAsync("InformationHandlerMessage", message);
                }
                catch (Exception) { }
            });
        }

        public void ChartNotifyAsync(object model)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    lock (balanceLock)
                    {
                        if (_hubConnection.State == HubConnectionState.Disconnected)
                            _hubConnection.StartAsync();
                    }

                    string message = JsonConvert.SerializeObject(model, _jsonSerializerSettings);
                    await _hubConnection.InvokeAsync("ChartMessage", message);
                }
                catch (Exception) { }
            });
        }

        public void AlertNotifyAsync(object model)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    lock (balanceLock)
                    {
                        if (_hubConnection.State == HubConnectionState.Disconnected)
                            _hubConnection.StartAsync();
                    }

                    string message = JsonConvert.SerializeObject(model, _jsonSerializerSettings);
                    await _hubConnection.InvokeAsync("AlertNotifyMessage", message);
                }
                catch (Exception) { }
            });
        }

        public void ConfirmedOrderNotify(long instrumentId, object model)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    lock (balanceLock)
                    {
                        if (_hubConnection.State == HubConnectionState.Disconnected)
                            _hubConnection.StartAsync();
                    }

                    string message = JsonConvert.SerializeObject(model, _jsonSerializerSettings);
                    await _hubConnection.InvokeAsync("ConfirmedOrderMessage", instrumentId, message);
                }
                catch (Exception) { }
            });
        }
    }
}
