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
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLibrary;
using SignalRBroadCastListener.Interfaces;

namespace SignalRBroadCastListener.HubConfiguration
{
    /// <summary>
    /// Hub confguration section class
    /// </summary>
    public class HubConfigurationSection : ConfigurationSection, IHubConfiguration
    {
        #region Private Members

        private string _hubUrl;
        private string _hubName;
        private EventNameEnum _hubEventName;
        private bool _isHubListeningEnabled = true;// Default is enabled

        #endregion

        #region Constructor

        /// <summary>
        /// Hub confguration section class
        /// </summary>
        public HubConfigurationSection()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set hub URL value
        /// </summary>
        public string HubURL
        {
            get
            {
                return HubURLElement != null && !string.IsNullOrWhiteSpace(this.HubURLElement.URL) ? this.HubURLElement.URL : _hubUrl;
            }
            set
            {
                _hubUrl = value;
            }
        }

        /// <summary>
        /// Get or set hub name value
        /// </summary>
        public string HubName
        {
            get
            {
                return this.HubNameElement != null &&
                    !string.IsNullOrWhiteSpace(this.HubNameElement.Name) ?
                    this.HubNameElement.Name : _hubName;
            }
            set
            {
                _hubName = value;
            }
        }

        /// <summary>
        /// Get or set hub event name value
        /// </summary>
        public EventNameEnum HubEventName
        {
            get
            {
                return this.HubEventNameElement != null &&
                    !string.IsNullOrWhiteSpace(this.HubEventNameElement.EventName) ?
                    this.HubEventNameElement.EventName.FindEnumFromDescription<EventNameEnum>() : _hubEventName;
            }
            set
            {
                _hubEventName = value;
            }
        }

        /// <summary>
        /// Get or set hub listening enable value. Default is true
        /// </summary>
        public bool IsHubListeningEnabled
        {
            get
            {
                if (this.HubListeningIndicatorElement != null && !string.IsNullOrWhiteSpace(this.HubListeningIndicatorElement.IsEnabled))
                    bool.TryParse(this.HubListeningIndicatorElement.IsEnabled, out _isHubListeningEnabled);
                return _isHubListeningEnabled;
            }
            set
            {
                _isHubListeningEnabled = value;
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Get Hub URL value from configuration
        /// </summary>
        [ConfigurationProperty(HubConfigurationConstants.HUB_URL)]
        private HubURLElement HubURLElement
        {
            get
            {
                return (HubURLElement)this[HubConfigurationConstants.HUB_URL];
            }
            set
            {
            }
        }

        /// <summary>
        /// Get Hub name value from configuration 
        /// </summary>
        [ConfigurationProperty(HubConfigurationConstants.HUB_NAME)]
        private HubNameElement HubNameElement
        {
            get
            {
                return (HubNameElement)this[HubConfigurationConstants.HUB_NAME];
            }
            set
            {
            }
        }

        /// <summary>
        /// Get Hub EventName value from configuration 
        /// </summary>
        [ConfigurationProperty(HubConfigurationConstants.HUB_EVENT_NAME)]
        private HubEventNameElement HubEventNameElement
        {
            get
            {
                return (HubEventNameElement)this[HubConfigurationConstants.HUB_EVENT_NAME];
            }
            set
            {
            }
        }

        /// <summary>
        /// Get Hub Listening value from configuration 
        /// </summary>
        [ConfigurationProperty(HubConfigurationConstants.HUB_LISTENING_INDICATOR)]
        private HubListeningIndicatorElement HubListeningIndicatorElement
        {
            get
            {
                return (HubListeningIndicatorElement)this[HubConfigurationConstants.HUB_LISTENING_INDICATOR];
            }
            set
            {
            }
        }

        #endregion

    }
}
