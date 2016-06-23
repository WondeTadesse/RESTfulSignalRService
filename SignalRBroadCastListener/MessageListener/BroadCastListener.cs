//|---------------------------------------------------------------|
//|                  SIGNALR BROADCAST LISTENER                   |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                  SIGNALR BROADCAST LISTENER                   |
//|---------------------------------------------------------------|
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using SignalRBroadCastListener.HubConfiguration;
using SignalRBroadCastListener.Interfaces;

namespace SignalRBroadCastListener.MessageListener
{
    /// <summary>
    /// BroadCastListener class
    /// </summary>
    public class BroadCastListener : IBroadCastListener, IDisposable
    {
        #region Private Members and Variables

        private bool isDisposed = false;
        private readonly object eventLocker = new object();

        private HubConnection hubConnection;
        private event EventHandler<BroadCastEventArgs> BroadCastListenerEventHandler;

        private IHubConfiguration _hubConfiguration;
        private BroadCastEventArgs _broadCastListenerEventArgs;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get SignalR connection status
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Get IHubConfiguration value
        /// </summary>
        public IHubConfiguration HubConfiguration
        {
            get
            {
                return _hubConfiguration;
            }
            set
            {
            }
        }

        /// <summary>
        /// Get BroadcastEventArgs value
        /// </summary>
        public CommonLibrary.BroadCastEventArgs BroadCastEventArgs
        {
            get
            {
                return _broadCastListenerEventArgs;
            }
            set
            {
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// BroadCast listener class
        /// </summary>
        /// <param name="hubConfiguration">IHubConfiguration value</param>
        public BroadCastListener(IHubConfiguration hubConfiguration)
        {
            if (hubConfiguration == null)
            {
                throw new ArgumentNullException("Hub configuration object is null !");
            }

            if (string.IsNullOrWhiteSpace(hubConfiguration.HubURL) || !Uri.IsWellFormedUriString(hubConfiguration.HubURL, UriKind.Absolute))
            {
                throw new ArgumentNullException("BroadCast service URL is empty or not well formed value !");
            }

            if (string.IsNullOrWhiteSpace(hubConfiguration.HubName))
            {
                throw new ArgumentNullException("BroadCast service HubName is empty !");
            }

            this.IsConnected = false;
            this._hubConfiguration = hubConfiguration;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Listen hub event and attach the message to the client event
        /// </summary>
        /// <param name="hubEvent">Client hub event</param>
        /// <returns>string value that shows status</returns>
        public string ListenHubEvent(Action<object, BroadCastEventArgs> hubEvent)
        {
            if (!_hubConfiguration.IsHubListeningEnabled)
                return string.Concat("Hub listening is disabled for an event named ", _hubConfiguration.HubEventName.EnumDescription());

            string statusMessage = string.Concat("All is well. Message is being listened for an event named ", _hubConfiguration.HubEventName.EnumDescription());

            try
            {

                if (hubEvent == null)
                    throw new ArgumentNullException("HubEvent is null !");

                if (hubConnection == null || isDisposed)
                    hubConnection = new HubConnection(_hubConfiguration.HubURL);

                IHubProxy proxyHub = hubConnection.CreateHubProxy(_hubConfiguration.HubName);

                try
                {
                    hubConnection.Start().
                        ContinueWith(task
                            =>
                            {
                                if (task.IsFaulted)
                                {
                                    throw task.Exception;
                                }
                                else
                                {
                                    // Register/attach broadcast listener event
                                    if (_hubConfiguration.HubEventName != EventNameEnum.UNKNOWN)
                                    {
                                        lock (eventLocker)
                                        {
                                            BroadCastListenerEventHandler += (sender, broadCastArgs) => 
                                                hubEvent.Invoke(sender, broadCastArgs);
                                        }
                                    }
                                }
                            }, TaskContinuationOptions.OnlyOnRanToCompletion).Wait();

                }
                catch (AggregateException aggregateException)
                {
                    throw aggregateException;
                }


                if (hubConnection.State == ConnectionState.Connected)
                    IsConnected = true;

                proxyHub.On<string>(_hubConfiguration.HubEventName.EnumDescription(),
                        message =>
                        {
                            _broadCastListenerEventArgs = new BroadCastEventArgs(
                                new MessageRequest()
                                {
                                    Message = message,
                                    EventName = _hubConfiguration.HubEventName
                                });
                            OnMessageListened(_broadCastListenerEventArgs);
                        });


                lock (eventLocker)
                {
                    // Unregister/detach broadcast listener event
                    BroadCastListenerEventHandler -= (sender, broadCastArgs) =>
                        hubEvent.Invoke(sender, broadCastArgs);
                }
            }
            catch (Exception exception)
            {
                // Client should handle the exception
                if (hubConnection != null && !string.IsNullOrWhiteSpace(hubConnection.ConnectionId))
                    return string.Format("Opps something wrong happened ! Hub ConnectionID : {0}, Exception - Message : {1}, StackTrace : {2} ",
                        hubConnection.ConnectionId, exception.Message, exception.StackTrace);
                return string.Format("Opps something wrong happened ! Exception - Message : {0}, StackTrace : {1} ", exception.Message, exception.StackTrace);
            }
            return hubConnection != null && !string.IsNullOrWhiteSpace(hubConnection.ConnectionId) ?
                string.Concat(statusMessage, " at Hub ConnectionID : ", hubConnection.ConnectionId) : statusMessage;

        }

        /// <summary>
        /// Listen hub event and attach the message to the client event asynchronously
        /// </summary>
        /// <param name="hubEvent">Client hub event</param>
        /// <returns>string task object that shows status</returns>
        public async Task<string> ListenHubEventAsync(Action<object, BroadCastEventArgs> hubEvent)
        {
            return await new TaskFactory().StartNew(
                () =>
                {
                    return ListenHubEvent(hubEvent);
                }
            );
        }

        /// <summary>
        /// Dispose hub connection object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Message listened and notified
        /// </summary>
        /// <param name="broadCastArgs">BroadCastArgs value</param>
        private void OnMessageListened(BroadCastEventArgs broadCastArgs)
        {
            EventHandler<BroadCastEventArgs> handler = BroadCastListenerEventHandler;
            if (handler != null)
                handler(this, broadCastArgs);
        }

        /// <summary>
        /// Dispose hub connection object
        /// </summary>
        /// <param name="disposing">Dispose indicator</param>
        private void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if (hubConnection != null)
                    {
                        hubConnection.Dispose();
                    }
                }
                isDisposed = true;
            }
        }

        #endregion

    }
}
