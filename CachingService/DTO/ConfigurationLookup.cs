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

namespace CachingService.DTO
{
    /// <summary>
    /// ConfigurationLookUp class
    /// </summary>
    public class ConfigurationLookup
    {
        #region Public Properties 
        
        /// <summary>
        /// Get or set ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Get or set Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set Value
        /// </summary>
        public string Value { get; set; }
        
        #endregion

    }
}