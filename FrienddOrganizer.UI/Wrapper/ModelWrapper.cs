using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FrienddOrganizer.UI.Wrapper
{
    public class ModelWrapper<T> :NotifyDataErrorInfo
    {
        public T Model { get; set; }

        public ModelWrapper(T friend)
        {
            Model = friend;
        }
        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName=null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
            
            
        }
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
             typeof(T).GetProperty(propertyName).SetValue(Model,value);
             OnPropertChange(propertyName);
            ValidatePropertyInternal(propertyName);
        }

        private void ValidatePropertyInternal(string propertyName)
        {
            CleanErrors(propertyName);
            var errors = ValidateProperty(propertyName);
            if(errors!=null)
            {
                foreach(var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }
    }
}
