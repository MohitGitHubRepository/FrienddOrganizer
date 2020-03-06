using FriendsOrganizer.Modles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.Wrapper
{
    public class FriendWrapper : Observable ,INotifyDataErrorInfo
    {
        public Friend Model { get;  set; }

        public FriendWrapper(Friend friend)
        {
            Model = friend;
        }

        public string FirstName
        {
            get { return Model.FirstName; }
            set {
                Model.FirstName = value;
                OnPropertChange();
                ValidationProperty(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return Model.LastName; }
            set
            {
                Model.LastName = value;
                OnPropertChange();
            }
        }

        public string Email
        {
            get { return Model.Email; }
            set
            {
                Model.Email = value;
                OnPropertChange();
            }
        }

        //validation Logic
        public void ValidationProperty(string propertyName)
       {
            CleanErrors(propertyName);
            switch(propertyName)
            {
                case nameof(FirstName):
                    if(string.Equals(FirstName,"Robot",StringComparison.OrdinalIgnoreCase))
                    {
                        AddError(propertyName, "Robot is not ValidName");
                    }
                    break;
            }

        }

        //Collection of Errors in Dictionary
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
        public void AddError(string propertyName,string error)
        {
            if(!_errorList.ContainsKey(propertyName))
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
