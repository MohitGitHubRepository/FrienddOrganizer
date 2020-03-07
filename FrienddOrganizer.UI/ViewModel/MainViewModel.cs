using FrienddOrganizer.UI.DataService;
using FrienddOrganizer.UI.Events;
using FrienddOrganizer.UI.Services;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FrienddOrganizer.UI.ViewModel
{
    public class MainViewModel : Observable
    {
        private IEventAggregator _eventAggregators;
        private IMessageDialogueService _IMessageDialogueService;
        private IFriendDetailViewModel _friendDetailviewModel;
        public ICommand OnNewCreateCommand { get; }

        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailviewModel; }
            set { _friendDetailviewModel = value;
                OnPropertChange();
            }
        }

        public INavigationViewModel NavigationViewModel { get; }
        public Func<IFriendDetailViewModel> friendDetailViewModelcreator { get; set; }

        public MainViewModel(INavigationViewModel  navigationViewModel, Func<IFriendDetailViewModel> IFriendDetailViewModel,
                             IEventAggregator eventAggregator, IMessageDialogueService IMessageDialogueService)
        {
            _eventAggregators = eventAggregator;
            _IMessageDialogueService = IMessageDialogueService;
            NavigationViewModel = navigationViewModel;
            friendDetailViewModelcreator = IFriendDetailViewModel;
            _eventAggregators.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnEventRecieved);
            OnNewCreateCommand = new DelegateCommand(OnNewFriendAdd);
        }

        private void OnNewFriendAdd()
        {
            OnEventRecieved(null);
        }

        private async void OnEventRecieved(int? FriendId)
        {
            if(FriendDetailViewModel!=null && FriendDetailViewModel.HasChanges)
            {
                var result = _IMessageDialogueService.SelectOKCancelMessageBox("You have unsaved changes.Navigate?", "Question");

                if(result== MessageDialogueStatus.Cancel)
                {
                    return;
                }
            }
            FriendDetailViewModel = friendDetailViewModelcreator();
            await FriendDetailViewModel.LoadAsync(FriendId);

        }

        public async Task Load()
        {
            await NavigationViewModel.LoadAsync();
        }
       

       

    }
}
