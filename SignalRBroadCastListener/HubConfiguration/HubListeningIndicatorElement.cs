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

namespace SignalRBroadCastListener.HubConfiguration
{
    /// <summary>
    /// Hub listening indicator configuration element
    /// </summary>
    internal class HubListeningIndicatorElement : ConfigurationElement
    {
        /// <summary>
        /// Get or set hub enable configuration property
        /// </summary>
        [ConfigurationProperty(HubConfigurationConstants.IS_ENABLED_PROPERTY, IsRequired = true)]
        internal string IsEnabled
        {
            get
            {
                return (string)this[HubConfigurationConstants.IS_ENABLED_PROPERTY];
            }
            set
            {
                this[HubConfigurationConstants.IS_ENABLED_PROPERTY] = value;
            }
        }
    }
}
