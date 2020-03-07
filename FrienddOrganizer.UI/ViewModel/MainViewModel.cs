using FrienddOrganizer.UI.DataService;
using FrienddOrganizer.UI.Events;
using FriendsOrganizer.Modles;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel
{
    public class MainViewModel : Observable
    {
        private IEventAggregator _eventAggregators;
        private IFriendDetailViewModel _friendDetailviewModel;
        

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailviewModel; }
            set { _friendDetailviewModel = value;
                OnPropertChange();
            }
        }

        public INavigationViewModel NavigationViewModel { get; }
        public Func<IFriendDetailViewModel> friendDetailViewModelcreator { get; set; }

        public MainViewModel(INavigationViewModel  navigationViewModel, Func<IFriendDetailViewModel> IFriendDetailViewModel,IEventAggregator eventAggregator)
        {
            _eventAggregators = eventAggregator;
            NavigationViewModel = navigationViewModel;
            friendDetailViewModelcreator = IFriendDetailViewModel;
            _eventAggregators.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnEventRecieved);
        }
        private async void OnEventRecieved(int FriendId)
        {
            FriendDetailViewModel = friendDetailViewModelcreator();
            await FriendDetailViewModel.LoadAsync(FriendId);

        }

        public async Task Load()
        {
            await NavigationViewModel.LoadAsync();
        }
       

       

    }
}
