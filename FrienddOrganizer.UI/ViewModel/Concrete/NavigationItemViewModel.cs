using FrienddOrganizer.UI.Events;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrienddOrganizer.UI.ViewModel
{
    public class NavigationItemViewModel:Observable
    {
        public NavigationItemViewModel(int id,string description,IEventAggregator eventAggregator,string eventDaescription)
        {
            this.Id = id;
            this.Description = description;
            _eventDaescription = eventDaescription;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailView);
            _eventAggregator = eventAggregator;
        }

        private void OnOpenDetailView()
        {
           _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(new OpenDetailViewEventArg()
           {
               Id = this.Id,
               ViewModelName = _eventDaescription
           });
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

        private string _eventDaescription;

        public ICommand OpenDetailViewCommand { get; }

        private IEventAggregator _eventAggregator;
    }
}
