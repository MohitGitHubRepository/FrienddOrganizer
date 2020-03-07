using FriendsOrganizer.Modles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FrienddOrganizer.UI.Wrapper
{
    public class NotifyDataErrorInfo : Observable, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errorList = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorList.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorList.ContainsKey(propertyName) ? _errorList[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public void AddError(string propertyName, string error)
        {
            if (!_errorList.ContainsKey(propertyName))
            {
                _errorList[propertyName] = new List<string>();


            }
            if (!_errorList[propertyName].Contains(error))
            {
                _errorList[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }

        }

        public void CleanErrors(string propertyName)
        {
            if (_errorList.ContainsKey(propertyName))
            {
                _errorList.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
