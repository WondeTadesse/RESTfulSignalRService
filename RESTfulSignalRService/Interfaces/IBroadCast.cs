//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;

namespace RESTfulSignalRService.Interfaces
{
    /// <summary>
    /// IBroadCast interface
    /// </summary>
    public interface IBroadCast
    {
        /// <summary>
        /// BroadCast messsage
        /// </summary>
        /// <param name="messageRequest">MessageRequest value</param>
        void BroadCast(MessageRequest messageRequest);

        /// <summary>
        /// Message Listener event handler
        /// </summary>
        event EventHandler<BroadCastEventArgs> MessageListened;
    }
}
