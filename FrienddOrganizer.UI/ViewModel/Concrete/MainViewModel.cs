using FrienddOrganizer.UI.Events;
using FrienddOrganizer.UI.Services;
using FrienddOrganizer.UI.ViewModel.Abstract;
using FrienddOrganizer.UI.ViewModel.Concrete;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System;
using Autofac;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac.Features.Indexed;

namespace FrienddOrganizer.UI.ViewModel
{
    public class MainViewModel : Observable
    {
        private IEventAggregator _eventAggregators;
        private IMessageDialogueService _IMessageDialogueService;
       
        private IDetailViewModel _detailviewModel;
        public ICommand OnNewFriendCreateCommand { get; }

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

        private IIndex<string, IDetailViewModel> _detailviewmodel;

        public Func<IFriendDetailViewModel> friendDetailViewModelcreator { get; set; }
        public Func<IMeetingDetailViewModel> meetingDetailViewModelcreator { get; set; }
        public DelegateCommand OnNewMeetingCreateCommand { get; }

        public MainViewModel(INavigationViewModel navigationViewModel,
                             IIndex<string,IDetailViewModel> IdetailViewModel,
                             IEventAggregator eventAggregator, 
                             IMessageDialogueService IMessageDialogueService)
        {
            _eventAggregators = eventAggregator;
            _IMessageDialogueService = IMessageDialogueService;
          
            NavigationViewModel = navigationViewModel;
            _detailviewmodel = IdetailViewModel;
            _eventAggregators.GetEvent<OpenDetailViewEvent>().Subscribe(OnEventRecieved);
            _eventAggregators.GetEvent<DetailDeleteEvent>().Subscribe(OnDeletetRecieved);
            OnNewFriendCreateCommand = new DelegateCommand(OnNewFriendAdd);
            OnNewMeetingCreateCommand = new DelegateCommand(OnNewMeetingAdd);
        }

        private void OnDeletetRecieved(DeleteDetailEventArg args)
        {
            switch(args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    detailViewModel = null;
                    break;
                case nameof(MeetingDetailViewModel):
                    detailViewModel = null;
                    break;
            }
          
        }

        private void OnNewFriendAdd()
        {
            OnEventRecieved(new OpenDetailViewEventArg() { ViewModelName = nameof(FriendDetailViewModel) });
        }
        private void OnNewMeetingAdd()
        {
            OnEventRecieved(new OpenDetailViewEventArg() { ViewModelName = nameof(MeetingDetailViewModel) });
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
            detailViewModel = _detailviewmodel[args.ViewModelName];
            await detailViewModel.LoadAsync(args.Id);

        }

        public async Task Load()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
