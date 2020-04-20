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
using System.ComponentModel;
using System.Linq;
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
        public ICommand AddPhoneNumber { get; }

        public ICommand RemovePhoneNumber { get; }
        public ObservableCollection<LookUpItem> ProgrammingLanguageComboBox { get; }
        public ObservableCollection<FriendPhoneNumberWrapper> PhoneNumbers { get; }
        private FriendWrapper _friend;
        private FriendPhoneNumberWrapper _selectedphoneNumber;

        public FriendPhoneNumberWrapper SelectedPhoneNumber
        {
            get { return _selectedphoneNumber; }
            set { _selectedphoneNumber = value;
                OnPropertChange();
                ((DelegateCommand)RemovePhoneNumber).RaiseCanExecuteChanged();
            }
        }

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
            AddPhoneNumber = new DelegateCommand(OnAddPhoneNumber);
            RemovePhoneNumber = new DelegateCommand(OnRemovePhoneNumber, OnCanRemoveMethod);
            ProgrammingLanguageComboBox = new ObservableCollection<LookUpItem>();
            PhoneNumbers = new ObservableCollection<FriendPhoneNumberWrapper>();
        }

        private bool OnCanRemoveMethod()
        {
            return SelectedPhoneNumber!=null;
        }

        private void OnRemovePhoneNumber()
        {
            SelectedPhoneNumber.PropertyChanged -= Friends_PropertyChangedLogic;
            _friendsDataService.RemovePhoneNumber(SelectedPhoneNumber.Model);
            this.PhoneNumbers.Remove(SelectedPhoneNumber);
            HasChanges = _friendsDataService.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnAddPhoneNumber()
        {
            var newNumber = new FriendPhoneNumberWrapper(new FriendPhoneNumber());
            newNumber.PropertyChanged += Friends_PropertyChangedLogic;
            PhoneNumbers.Add(newNumber);
            Friend.Model.FriendPhoneNumbers.Add(newNumber.Model);
            newNumber.PhoneNumber = ""; 

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
            PhoneNumbersAddition(Id, friend);
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

        private void PhoneNumbersAddition(int? Id, Friend friend)
        { 
                foreach(var wrapper in PhoneNumbers)
                {
                wrapper.PropertyChanged -= Friends_PropertyChangedLogic;
                     
                }
                PhoneNumbers.Clear();
                foreach (var phone in friend.FriendPhoneNumbers)
                {
                var wrapper = new FriendPhoneNumberWrapper(phone);
                wrapper.PropertyChanged += Friends_PropertyChangedLogic;
                PhoneNumbers.Add(wrapper);

            }
             
        }

        private void Friends_PropertyChangedLogic(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendsDataService.HasChanges();
            }
            if (e.PropertyName == nameof(FriendPhoneNumberWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
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
            return Friend != null && !Friend.HasErrors && PhoneNumbers.All(p=> !p.HasErrors) && HasChanges;
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
