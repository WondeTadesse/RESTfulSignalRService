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
using System.Collections.Concurrent;
using Newtonsoft.Json;
using CommonLibrary;
using SignalRBroadCastListener.MessageListener;

using CachingService.DTO;
using CachingService.DataAccess;
using CachingService.Business.Interfaces;
using CachingService.Utility;
using System.Text;

namespace CachingService.Business
{
    public class SingletonCacheManager
    {
        #region Private Member Variables 

        private static object _locker = new object();
        private static readonly Lazy<SingletonCacheManager> _instance = new Lazy<SingletonCacheManager>(() => new SingletonCacheManager());
        private static IDBListener _dbListener;

        private List<ConfigurationLookup> _configurationLookUpCaches = new List<ConfigurationLookup>();

        #endregion
        
        #region Public Properties 

        /// <summary>
        /// Get ConfigurationLookUps cache
        /// </summary>
        public List<ConfigurationLookup> ConfigurationLookUpCaches
        {
            get
            {
                return _configurationLookUpCaches ?? ConfigurationLookUpDL.GetConfigurationLookUps();
            }
        }

        #endregion
        
        #region Public Methods 

        /// <summary>
        /// SingletonCacheManager object
        /// </summary>
        public static SingletonCacheManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// IDBListener initializer method 
        /// </summary>
        /// <param name="dbListener">IDBListener value</param>
        public static void DBListener(IDBListener dbListener)
        {
            _dbListener = dbListener;
        }

        #endregion

        #region Constructor 

        /// <summary>
        /// Private Constructor 
        /// </summary>
        private SingletonCacheManager()
        {
            try
            {
                LoadConfiguration();
                HookDBListeners();
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// Load Configuration 
        /// </summary>
        private void LoadConfiguration()
        {
            // Load other cacheable datas
            _configurationLookUpCaches = ConfigurationLookUpDL.GetConfigurationLookUps();

            /* 
             *
             * others 
             *              
             */
        }
 
        /// <summary>
        /// ListenerEvent Hooker method 
        /// </summary>
        /// <param name="dbListener">IDBListener value</param>
        private void HookDBListeners()
        {
            if (_dbListener != null)
            {
                StringBuilder buildEventStatus = new StringBuilder();
                buildEventStatus.Append(_dbListener.InsertListener.ListenHubEvent(InsertEventListener));
                buildEventStatus.AppendLine(_dbListener.UpdateListener.ListenHubEvent(UpdateEventListener));
                buildEventStatus.Append(_dbListener.DeleteListener.ListenHubEvent(DeleteEventListener));
                // Log the status
            }
        }

        /// <summary>
        /// Insert event listener
        /// </summary>
        /// <param name="sender">Sender value</param>
        /// <param name="e">BroadCastEventArgs value</param>
        private void InsertEventListener(object sender, BroadCastEventArgs e)
        {
            lock (_locker)
            {
                ConfigurationLookup configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookup>(e.MessageRequest.Message, out configurationLookUp))
                {
                    _configurationLookUpCaches.Add(configurationLookUp);
                    _configurationLookUpCaches.OrderByDescending(cl => cl.ID);
                }
            }
        }

        /// <summary>
        /// Update event listener
        /// </summary>
        /// <param name="sender">Sender value</param>
        /// <param name="e">BroadCastEventArgs value</param>
        private void UpdateEventListener(object sender, BroadCastEventArgs e)
        {
            lock (_locker)
            {
                ConfigurationLookup configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookup>(e.MessageRequest.Message, out configurationLookUp))
                {
                    ConfigurationLookup configurationLookUpToBeUpdate = _configurationLookUpCaches.Where(cl => cl.ID == configurationLookUp.ID).FirstOrDefault();
                    if (configurationLookUpToBeUpdate != null)
                    {
                        configurationLookUpToBeUpdate.Name = configurationLookUp.Name;
                        configurationLookUpToBeUpdate.Value = configurationLookUp.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Delete event listener
        /// </summary>
        /// <param name="sender">Sender value</param>
        /// <param name="e">BroadCastEventArgs value</param>
        private void DeleteEventListener(object sender, BroadCastEventArgs e)
        {
            lock (_locker)
            {
                ConfigurationLookup configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookup>(e.MessageRequest.Message, out configurationLookUp))
                {
                    ConfigurationLookup configurationLookUpToBeDelete = _configurationLookUpCaches.Where(cl => cl.ID == configurationLookUp.ID).FirstOrDefault();
                    if (configurationLookUpToBeDelete != null)
                    {
                        _configurationLookUpCaches.Remove(configurationLookUpToBeDelete);
                        _configurationLookUpCaches.OrderByDescending(cl => cl.ID);
                    }
                }
            }
        }

        #endregion
    }
}