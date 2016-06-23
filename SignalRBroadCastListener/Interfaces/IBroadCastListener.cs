//|---------------------------------------------------------------|
//|                  SIGNALR BROADCAST LISTENER                   |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                  SIGNALR BROADCAST LISTENER                   |
//|---------------------------------------------------------------|
using System;
using System.Threading.Tasks;
using SignalRBroadCastListener.HubConfiguration;

namespace SignalRBroadCastListener.Interfaces
{
    /// <summary>
    /// IBroadCastListener interface
    /// </summary>
    public interface IBroadCastListener
    {
        /// <summary>
        /// Get IHubConfiguration value
        /// </summary>
        IHubConfiguration HubConfiguration { get; set; }

        /// <summary>
        /// Get SignalR connection status
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Listen hub event and attach the message to the client event
        /// </summary>
        /// <param name="hubEvent">Client hub event</param>
        /// <returns>string value that shows status</returns>
        string ListenHubEvent(Action<object, CommonLibrary.BroadCastEventArgs> hubEvent);

        /// <summary>
        /// Listen hub event and attach the message to the client event asynchronously
        /// </summary>
        /// <param name="hubEvent">Client hub event</param>
        /// <returns>string task object that shows status</returns>
        Task<string> ListenHubEventAsync(Action<object, CommonLibrary.BroadCastEventArgs> hubEvent);
    }
}
