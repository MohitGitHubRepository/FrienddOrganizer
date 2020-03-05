using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel:Observable
    {
        public NavigationItemViewModel(int id,string description)
        {
            this.Id = id;
            this.Description = description;

        }
        public int Id { get; set; }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value;
                OnPropertChange();
            }
        }

    }
}
