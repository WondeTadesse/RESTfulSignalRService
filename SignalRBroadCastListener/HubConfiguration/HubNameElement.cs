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
    /// HubName configuration element
    /// </summary>
    internal class HubNameElement : ConfigurationElement
    {
        /// <summary>
        /// Get or set Name configuration property
        /// </summary>
        [ConfigurationProperty(HubConfigurationConstants.HUB_NAME_PROPERTY, IsRequired = true)]
        internal string Name
        {
            get
            {
                return (string)this[HubConfigurationConstants.HUB_NAME_PROPERTY];
            }
            set
            {
                this[HubConfigurationConstants.HUB_NAME_PROPERTY] = value;
            }
        }
    }
}
