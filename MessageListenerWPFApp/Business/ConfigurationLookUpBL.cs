//|---------------------------------------------------------------|
//|                  MESSAGE LISTENER WPF APP                     |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                  MESSAGE LISTENER WPF APP                     |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading;

using CommonLibrary;
using SignalRBroadCastListener.MessageListener;
using MessageListenerWPFApp.VM;
using MessageListenerWPFApp.DataAccess;
using MessageListenerWPFApp.Business.Interfaces;
using MessageListenerWPFApp.Utility;
using System.Text;

namespace MessageListenerWPFApp.Business
{
    /// <summary>
    /// ConfiguationLookUp Business class
    /// </summary>
    public class ConfigurationLookUpBL
    {
        #region Private Members and Variables 

        private ConfigurationLookUps _configurationLookUps;
        private static object _locker = new object();

        #endregion

        #region Public Properties 

        /// <summary>
        /// Get ConfigurationLookUps 
        /// </summary>
        public ConfigurationLookUps ConfigurationLookUpCaches
        {
            get
            {
                return _configurationLookUps ?? (_configurationLookUps = ConfigurationLookUpDL.GetConfigurationLookUps());
            }
        }

        #endregion

        #region Constructor 
        
        /// <summary>
        /// ConfiguationLookUp Business class
        /// </summary>
        /// <param name="dbListener">IDBListener value</param>
        public ConfigurationLookUpBL(IDBListener dbListener)
        {
            HookDBListeners(dbListener);
        }

        #endregion

        #region Private Methods 

        /// <summary>
        /// ListenerEvent Hooker method 
        /// </summary>
        /// <param name="dbListener">IDBListener value</param>
        private void HookDBListeners(IDBListener dbListener)
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
        private void InsertEventListener(object sender, BroadCastEventArgs broadCastEventArgs)
        {
            lock (_locker)
            {
                ConfigurationLookupVM configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookupVM>(broadCastEventArgs.MessageRequest.Message, out configurationLookUp))
                {
                    // Modify the collection. Set the configuration lookup object status to Inserted 
                    App.Current.Dispatcher.Invoke(() =>
                        {
                            configurationLookUp.Status = Status.Inserted.ToString();
                            _configurationLookUps.Add(configurationLookUp);
                            _configurationLookUps.OrderByDescending(cl => cl.ID);
                        });
                }
            }
        }

        /// <summary>
        /// Update event listener
        /// </summary>
        /// <param name="sender">Sender value</param>
        /// <param name="broadCastEventArgs">BroadCastEventArgs value</param>
        private void UpdateEventListener(object sender, BroadCastEventArgs broadCastEventArgs)
        {
            lock (_locker)
            {
                ConfigurationLookupVM configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookupVM>(broadCastEventArgs.MessageRequest.Message, out configurationLookUp))
                {
                    ConfigurationLookupVM configurationLookUpToBeUpdate = _configurationLookUps.Where(cl => cl.ID == configurationLookUp.ID).FirstOrDefault();

                    if (configurationLookUpToBeUpdate != null)
                    {
                        // Modify the collection. 
                        // 1. Set the configuration lookup object status to Update and update the value 
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            configurationLookUpToBeUpdate.Status = Status.Updated.ToString();
                            configurationLookUpToBeUpdate.Name = configurationLookUp.Name;
                            configurationLookUpToBeUpdate.Value = configurationLookUp.Value;
                        });

                        // 2. Wait few second and set the configuration lookup object status to Default  
                        Thread.Sleep(2500);
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            configurationLookUpToBeUpdate.Status = Status.Default.ToString();
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Delete event listener
        /// </summary>
        /// <param name="sender">Sender value</param>
        /// <param name="broadCastEventArgs">BroadCastEventArgs value</param>
        private void DeleteEventListener(object sender, BroadCastEventArgs broadCastEventArgs)
        {
            lock (_locker)
            {
                ConfigurationLookupVM configurationLookUp;
                if (SerializationHelper.TryDeserialize<ConfigurationLookupVM>(broadCastEventArgs.MessageRequest.Message, out configurationLookUp))
                {
                    ConfigurationLookupVM configurationLookUpToBeDelete = _configurationLookUps.Where(cl => cl.ID == configurationLookUp.ID).FirstOrDefault();
                    if (configurationLookUpToBeDelete != null)
                    {
                        // Modify the collection. 
                        // 1. Set the configuration lookup object status to Deleted.This is to show the Grid animation related to Delete
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            configurationLookUpToBeDelete.Status = Status.Deleted.ToString();
                        });

                        // 2. Wait few second and remove the deleted from the collecion.
                        Thread.Sleep(2500);
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            ConfigurationLookUpCaches.Remove(configurationLookUpToBeDelete);
                            ConfigurationLookUpCaches.OrderByDescending(cl => cl.ID);
                        });
                    }
                }
            }
        }
      
        #endregion
    }
}