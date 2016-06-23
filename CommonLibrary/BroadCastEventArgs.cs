//|---------------------------------------------------------------|
//|                     COMMON LIBRARY                            |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                     COMMON LIBRARY                            |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLibrary
{
    /// <summary>
    /// Broadcast event argument class
    /// </summary>
    public class BroadCastEventArgs : EventArgs
    {
        #region Private Members

        /// <summary>
        /// Get or set MessageRequest value
        /// </summary>
        public MessageRequest MessageRequest { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Broadcast event argument class
        /// </summary>
        /// <param name="messageRequest">MessageRequest object value</param>
        public BroadCastEventArgs(MessageRequest messageRequest)
        {
            MessageRequest = messageRequest ?? new MessageRequest();
        }

        #endregion

    }
}