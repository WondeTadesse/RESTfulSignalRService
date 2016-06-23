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
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessageListenerWPFApp.VM
{
    /// <summary>
    /// Observable entity class
    /// </summary>
    public abstract class ObservableEntity : INotifyPropertyChanged
    {
        #region Public Event 

        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion

        #region Public Properties 
        
        /// <summary>
        /// Notify property changed to the caller property
        /// </summary>
        /// <param name="propertyName">PropetyName value</param>
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Set Property only if there is a change on it
        /// </summary>
        /// <typeparam name="T">Property Type value</typeparam>
        /// <param name="oldValue">Old value of the property</param>
        /// <param name="newValue">New value of the property</param>
        /// <param name="propertyName">PropertyName value</param>
        public void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] String propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                oldValue = newValue;
                NotifyPropertyChanged(propertyName);
            }
        }
    
        #endregion
    }
}
