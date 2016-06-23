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
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

using CachingService.DTO;
using CachingService.Business;
using CachingService.Business.Interfaces;

namespace CachingService.Controllers
{
    /// <summary>
    /// Configuration Apicontroller class
    /// </summary>
    public class ConfigurationController : ApiController
    {
        #region Private Variable

        private IDBListener _dbListener;

        #endregion

        #region Constructor

        /// <summary>
        /// Configuration Apicontroller class
        /// </summary>
        /// <param name="dbListener">IDBListener object</param>
        public ConfigurationController(IDBListener dbListener)
        {
            _dbListener = dbListener;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Get ConfigurationLookUp Cache
        /// </summary>
        /// <returns>List of ConfigurationLookup</returns>
        public List<ConfigurationLookup> GetConfigurationLookUpCaches()
        {
            List<ConfigurationLookup> configurationLookUpCaches = new List<ConfigurationLookup>();
            MemoryCacheManager.DBListener(_dbListener);
            configurationLookUpCaches = MemoryCacheManager.ConfigurationLookUpCaches;
            
            if (configurationLookUpCaches == null)
            {
                SingletonCacheManager.DBListener(_dbListener);
                configurationLookUpCaches = SingletonCacheManager.Instance.ConfigurationLookUpCaches;
            }

            return configurationLookUpCaches;
        }

        #endregion
    }
}
