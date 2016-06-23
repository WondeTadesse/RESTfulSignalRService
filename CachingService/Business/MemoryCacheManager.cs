//|---------------------------------------------------------------|
//|                     CACHING SERVICE                           |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                     CACHING SERVICE                           |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using System.Collections.Concurrent;
using Newtonsoft.Json;

using CommonLibrary;
using SignalRBroadCastListener.MessageListener;

using CachingService.DTO;
using CachingService.DataAccess;
using CachingService.Business.Interfaces;
using CachingService.Utility;
using Ninject;
using System.Text;

namespace CachingService.Business
{
    /// <summary>
    /// Memory cache manager
    /// </summary>
    public class MemoryCacheManager
    {
        #region Private Variables 

        private const string CONFIGURATION_LOOKUP_CACHE_KEY = "ConfigurationLookUpCache";
        private static List<ConfigurationLookup> _configurationLookUpCaches = new List<ConfigurationLookup>();
        private static object _locker = new object();
        private static CacheItemPolicy policy;
        private static ObjectCache cache;

        #endregion

        #region Public Properties 

        /// <summary>
        /// Get ConfigurationLookUps caches
        /// </summary>
        public static List<ConfigurationLookup> ConfigurationLookUpCaches
        {
            get
            {
                cache = MemoryCache.Default;
                _configurationLookUpCaches = cache[CONFIGURATION_LOOKUP_CACHE_KEY] as List<ConfigurationLookup>;
                if (_configurationLookUpCaches == null)
                {
                    _configurationLookUpCaches = ConfigurationLookUpDL.GetConfigurationLookUps();
                    cache.Add(CONFIGURATION_LOOKUP_CACHE_KEY, _configurationLookUpCaches, policy);
                }
                return _configurationLookUpCaches ?? (_configurationLookUpCaches = new List<ConfigurationLookup>());
            }
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// IDBListener initializer method 
        /// </summary>
        /// <param name="dbListener">IDBListener value</param>
        public static void DBListener(IDBListener dbListener)
        {
            policy = new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(20) }; // A 20 min expiration policy
            HookDBListeners(dbListener);
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// ListenerEvent Hooker method 
        /// </summary>
        /// <param name="dbListener">IDBListener value</param>
        private static void HookDBListeners(IDBListener dbListener)
        {
            if (dbListener != null)
            {
                StringBuilder buildEventStatus = new StringBuilder();
                buildEventStatus.Append(dbListener.InsertListener.ListenHubEvent(InsertEventListener));
                buildEventStatus.AppendLine(dbListener.UpdateListener.ListenHubEvent(UpdateEventListener));
                buildEventStatus.Append(dbListener.DeleteListener.ListenHubEvent(DeleteEventListener));
                // Log the status
            }
        }

        /// <summary>
        /// Insert event listener
        /// </summary>
        /// <param name="sender">Sender value</param>
        /// <param name="broadCastEventArgs">BroadCastEventArgs value</param>
        private static void InsertEventListener(object sender, BroadCastEventArgs broadCastEventArgs)
        {
            lock (_locker)
            {
                ConfigurationLookup configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookup>(broadCastEventArgs.MessageRequest.Message, out configurationLookUp))
                {
                    _configurationLookUpCaches.Add(configurationLookUp);
                    _configurationLookUpCaches.OrderByDescending(cl => cl.ID);
                    cache.Set(CONFIGURATION_LOOKUP_CACHE_KEY, _configurationLookUpCaches, policy);
                }
            }
        }

        /// <summary>
        /// Update event listener
        /// </summary>
        /// <param name="sender">Sender value</param>
        /// <param name="broadCastEventArgs">BroadCastEventArgs value</param>
        private static void UpdateEventListener(object sender, BroadCastEventArgs broadCastEventArgs)
        {
            lock (_locker)
            {
                ConfigurationLookup configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookup>(broadCastEventArgs.MessageRequest.Message, out configurationLookUp))
                {
                    ConfigurationLookup configurationLookUpToBeUpdate = _configurationLookUpCaches.Where(cl => cl.ID == configurationLookUp.ID).FirstOrDefault();
                    if (configurationLookUpToBeUpdate != null)
                    {
                        configurationLookUpToBeUpdate.Name = configurationLookUp.Name;
                        configurationLookUpToBeUpdate.Value = configurationLookUp.Value;
                        cache.Set(CONFIGURATION_LOOKUP_CACHE_KEY, _configurationLookUpCaches, policy);
                    }
                }
            }
        }

        /// <summary>
        /// Delete event listener
        /// </summary>
        /// <param name="sender">Sender value</param>
        /// <param name="broadCastEventArgs">BroadCastEventArgs value</param>
        private static void DeleteEventListener(object sender, BroadCastEventArgs broadCastEventArgs)
        {
            lock (_locker)
            {
                ConfigurationLookup configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookup>(broadCastEventArgs.MessageRequest.Message, out configurationLookUp))
                {
                    ConfigurationLookup configurationLookUpToBeDelete = _configurationLookUpCaches.Where(cl => cl.ID == configurationLookUp.ID).FirstOrDefault();
                    if (configurationLookUpToBeDelete != null)
                    {
                        _configurationLookUpCaches.Remove(configurationLookUpToBeDelete);
                        _configurationLookUpCaches.OrderByDescending(cl => cl.ID);
                        cache.Set(CONFIGURATION_LOOKUP_CACHE_KEY, _configurationLookUpCaches, policy);
                    }
                }
            }
        }

        #endregion        
    }
}