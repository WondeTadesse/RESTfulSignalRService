//|---------------------------------------------------------------|
//|                  SIGNALR BROADCAST LISTENER                   |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                  SIGNALR BROADCAST LISTENER                   |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRBroadCastListener.HubConfiguration
{
    /// <summary>
    /// Hub configration constants
    /// </summary>
    public class HubConfigurationConstants
    {
        #region Configuation Element Constants 

       
        public const string HUB_URL = "hubUrl";

        public const string HUB_NAME = "hubName";

        public const string HUB_EVENT_NAME = "hubEventName";

        public const string HUB_LISTENING_INDICATOR = "hubListeningIndicator";

        #endregion

        #region Configuation Element Property Constants 

        public const string HUB_URL_PROPERTY = "url";

        public const string HUB_NAME_PROPERTY = "name";

        public const string HUB_EVENT_NAME_PROPERTY = "eventName";

        public const string IS_ENABLED_PROPERTY = "isEnabled";

        #endregion
    }
}
