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
    /// HubURL configuration element
    /// </summary>
    internal class HubURLElement : ConfigurationElement
    {
        /// <summary>
        /// URL configuration property
        /// </summary>
        [ConfigurationProperty(HubConfigurationConstants.HUB_URL_PROPERTY, IsRequired = true)]
        internal string URL
        {
            get
            {
                return (string)this[HubConfigurationConstants.HUB_URL_PROPERTY];
            }
            set
            {
                this[HubConfigurationConstants.HUB_URL_PROPERTY] = value;
            }
        }
    }
}
