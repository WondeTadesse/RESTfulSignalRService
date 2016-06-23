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

using SignalRBroadCastListener.Interfaces;


namespace CachingService.Business.Interfaces
{
    /// <summary>
    /// DataBase Hub Listener interface
    /// </summary>
    public interface IDBListener
    {
        #region Public Properties 

        /// <summary>
        /// Get Insert Listener
        /// </summary>
        IBroadCastListener InsertListener { get;}

        /// <summary>
        /// Get Update Listener
        /// </summary>
        IBroadCastListener UpdateListener { get;}
        
        /// <summary>
        /// Get Delete Listener
        /// </summary>
        IBroadCastListener DeleteListener { get; }

        #endregion
    }
}