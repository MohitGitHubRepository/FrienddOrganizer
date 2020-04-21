using FrienddOrganizer.UI.Events;
using FrienddOrganizer.UI.Services;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrienddOrganizer.UI.ViewModel
{
    public class MainViewModel : Observable
    {
        private IEventAggregator _eventAggregators;
        private IMessageDialogueService _IMessageDialogueService;
        private IDetailViewModel _detailviewModel;
        public ICommand OnNewCreateCommand { get; }

        public IDetailViewModel detailViewModel
        {
            get { return _detailviewModel; }
            set
            {
                _detailviewModel = value;
                OnPropertChange();
            }
        }

        public INavigationViewModel NavigationViewModel { get; }
        public Func<IFriendDetailViewModel> friendDetailViewModelcreator { get; set; }

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> IFriendDetailViewModel,
                             IEventAggregator eventAggregator, IMessageDialogueService IMessageDialogueService)
        {
            _eventAggregators = eventAggregator;
            _IMessageDialogueService = IMessageDialogueService;
            NavigationViewModel = navigationViewModel;
            friendDetailViewModelcreator = IFriendDetailViewModel;
            _eventAggregators.GetEvent<OpenDetailViewEvent>().Subscribe(OnEventRecieved);
            _eventAggregators.GetEvent<DetailDeleteEvent>().Subscribe(OnDeletetRecieved);
            OnNewCreateCommand = new DelegateCommand(OnNewFriendAdd);
        }

        private void OnDeletetRecieved(DeleteDetailEventArg args)
        {
            switch(args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    detailViewModel = null;
                    break;
            }
          
        }

        private void OnNewFriendAdd()
        {
            OnEventRecieved(new OpenDetailViewEventArg() { ViewModelName = nameof(FriendDetailViewModel) });
        }

        private async void OnEventRecieved(OpenDetailViewEventArg args)
        {
            if (detailViewModel != null && detailViewModel.HasChanges)
            {
                var result = _IMessageDialogueService.SelectOKCancelMessageBox("You have unsaved changes.Navigate?", "Question");

                if (result == MessageDialogueStatus.Cancel)
                {
                    return;
                }
            }
            switch(args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    detailViewModel = friendDetailViewModelcreator();
                    await detailViewModel.LoadAsync(args.Id);
                    break;
            }
            

        }

        public async Task Load()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
