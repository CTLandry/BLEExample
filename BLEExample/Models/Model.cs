﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BLEExample.Models
{
    /// <summary>
    /// Base class for all models implementing INotifyPropertyChanged across all properties of inherited classes as well
    /// as handling invoking on property changed events when a properties value changes.
    /// </summary>
    public abstract class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// If property being set is not equal to current property set property to new value and invoke OnPropertyChanged event
        /// </summary>
        /// <typeparam name="T">The object type of the property being set</typeparam>
        /// <param name="storage">The current value of the property</param>
        /// <param name="value">The value to be set as the new property value</param>
        /// <param name="propertyName">Optional name of the property</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Definition of the on property changed event
        /// </summary>
        /// <param name="propertyName">Optional name of the property</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
