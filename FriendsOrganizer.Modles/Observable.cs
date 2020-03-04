using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FriendsOrganizer.Modles
{
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertChange([CallerMemberName]string callername = null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(callername));
        }
    }
}
