using FrienddOrganizer.UI.DataService;
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
    public class FriendDetailViewModel : Observable, IFriendDetailViewModel
    {
        private IFriendsDataService _friendsDataService { get; }

        private IEventAggregator _eventAggregators;

        public FriendDetailViewModel(IFriendsDataService friendsDataService, IEventAggregator eventAggregator)
        {
            _friendsDataService = friendsDataService;
            _eventAggregators = eventAggregator;
            _eventAggregators.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnEventRecieved);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);

        }

        private bool OnSaveCanExecute()
        {
            //Button binds this method to IsEnabledProperty
            return true;
        }

        private async void OnSaveExecute()
        {
            await _friendsDataService.SaveAsync(Friend);
            _eventAggregators.GetEvent<NavigationPropertyUpdateEvent>()
                          .Publish(new NavigationPropertyUpdateArgs() { Id = Friend.Id, Description = $"{Friend.FirstName} {Friend.LastName}" });
        }

        private async void OnEventRecieved(int FriendId)
        {
            await this.LoadAsync(FriendId);
         
        }

        public async Task LoadAsync(int Id)
        {
            var friend = await _friendsDataService.getFriendById(Id);

            if(friend!=null)
            {
                Friend = friend;
            }

        }
        private Friend  _friend;

        public Friend Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertChange();

            }
        }

        public ICommand SaveCommand { get; }



    }
}
