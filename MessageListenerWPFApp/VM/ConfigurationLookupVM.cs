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
using System.Web;

namespace MessageListenerWPFApp.VM
{
    public class ConfigurationLookupVM : ObservableEntity
    {
        #region Private Members 

        private int _id;

        private string _name;

        private string _value;

        private string _status;

        #endregion

        #region Public Properties/Events 

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref this._id, value);
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref this._name, value);
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetProperty(ref this._value, value);
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref this._status, value);
            }
        }

        #endregion

    }
}