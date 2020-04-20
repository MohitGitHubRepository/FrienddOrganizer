using FrienddOrganizer.UI.DataService;
using FrienddOrganizer.UI.DataService.LookupService;
using FrienddOrganizer.UI.DataService.Repository;
using FrienddOrganizer.UI.Events;
using FrienddOrganizer.UI.Services;
using FrienddOrganizer.UI.Wrapper;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrienddOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : Observable, IFriendDetailViewModel
    {
        private IFriendsRepository _friendsDataService { get; }

        private IEventAggregator _eventAggregators;
        private IMessageDialogueService _IMessageDialogueService;

        public ICommand SaveCommand { get; }

        private IProgrammingLanguages _IProgrammingLanguages;

        public ICommand DeleteCommand { get; }
        public ObservableCollection<LookUpItem> ProgrammingLanguageComboBox { get; }

        private FriendWrapper _friend;
        private bool hasChanges;

        //  public ObservableCollection<LookUpItem> ProgrammingLanguageComboBox { get; set; }
        public FriendDetailViewModel(IFriendsRepository friendsDataService, IEventAggregator eventAggregator, IMessageDialogueService IMessageDialogueService, IProgrammingLanguages IProgrammingLanguages)
        {
            _friendsDataService = friendsDataService;
            _eventAggregators = eventAggregator;
            _IMessageDialogueService = IMessageDialogueService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            _IProgrammingLanguages = IProgrammingLanguages;
            DeleteCommand = new DelegateCommand(OnDeleteCommand);
            ProgrammingLanguageComboBox = new ObservableCollection<LookUpItem>();

        }



        public bool HasChanges
        {
            get { return hasChanges; }
            set
            {
                if (hasChanges != value)
                {
                    hasChanges = value;
                    OnPropertChange();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            }
        }

        public FriendWrapper Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertChange();
            }
        }

        public async Task LoadAsync(int? Id)
        {
            var friend = Id.HasValue ?
                await _friendsDataService.getFriendById(Id.Value) : CreateNewFriend();
            await AddProgrammingLanguage(friend);

            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendsDataService.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

        }

        private async Task AddProgrammingLanguage(Friend friend)
        {
            ProgrammingLanguageComboBox.Clear();
            
                ProgrammingLanguageComboBox.Add(new NullLookUpItem() { Desctiption = "-" });

            
           
            var languages = await _IProgrammingLanguages.getProgrammingLanguages();
            foreach (var item in languages)
            {
                ProgrammingLanguageComboBox.Add(item);
            }
        }

        private async void OnDeleteCommand()
        {
            var result = _IMessageDialogueService.SelectOKCancelMessageBox($"Do you confirm to delete {Friend.FirstName} {Friend.FirstName} ?", "Confirm");
            if (result == MessageDialogueStatus.Ok)
            {
                _friendsDataService.RemoveFriend(_friend.Model);
                await _friendsDataService.SaveAsync();
                _eventAggregators.GetEvent<NavigationPropertyDeleteEvent>()
                             .Publish(Friend.Model.Id);
                Friend = null;
            }

        }
        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            _friendsDataService.AddFriend(friend);
            return friend;
        }

        private bool OnSaveCanExecute()
        {
            //TODO:Che checkIn changes only if friend has changes
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            await _friendsDataService.SaveAsync();

            HasChanges = _friendsDataService.HasChanges();
            _eventAggregators.GetEvent<NavigationPropertyUpdateEvent>()
                          .Publish(new NavigationPropertyUpdateArgs() { Id = Friend.Model.Id, Description = $"{Friend.FirstName} {Friend.LastName}" });
        }

    }
}
