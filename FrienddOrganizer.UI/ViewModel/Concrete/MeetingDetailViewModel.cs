using FrienddOrganizer.UI.DataService.Repository;
using FrienddOrganizer.UI.Services;
using FrienddOrganizer.UI.ViewModel.Abstract;
using FrienddOrganizer.UI.Wrapper;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrienddOrganizer.UI.ViewModel.Concrete
{
    public class MeetingDetailViewModel : DetailViewModel, IMeetingDetailViewModel
    {
        private IMeetingRepository _meetingRepository;
        private IMessageDialogueService _messageDialogueService;
        private MeetingWrapper _meeting;

        public ObservableCollection<Friend> AddedFriend { get; set; }
        public ObservableCollection<Friend> FriendList { get; set; }

        public ICommand MoveLeftCommand { get; }

        public ICommand MoveRightCommand { get; }

        private Friend _SelectedFriendList;
        private Friend _SelectedAddFriend;

        public Friend SelectedFriendList
        {
            get { return _SelectedFriendList; }
            set
            {
                _SelectedFriendList = value;
                ((DelegateCommand)MoveLeftCommand).RaiseCanExecuteChanged();
            }
        }



        public Friend SelectedAddFriend
        {
            get { return _SelectedAddFriend; }
            set
            {
                _SelectedAddFriend = value;
                ((DelegateCommand)MoveRightCommand).RaiseCanExecuteChanged();
            }
        }

        public MeetingWrapper Meeting
        {
            get { return _meeting; }
            set
            {
                _meeting = value;
                OnPropertChange();
            }
        }

        public MeetingDetailViewModel(IEventAggregator eventAggregator, IMeetingRepository meetingRepository, IMessageDialogueService messageDialogueService) : base(eventAggregator)
        {
            _meetingRepository = meetingRepository;
            _messageDialogueService = messageDialogueService;
            AddedFriend = new ObservableCollection<Friend>();
            FriendList = new ObservableCollection<Friend>();
            MoveLeftCommand = new DelegateCommand(OnMoveLeftExecute, OnMoveLeftCanExecute);
            MoveRightCommand = new DelegateCommand(OnMoveRightExecute, OnMoveRightCanExecute);


        }

        private bool OnMoveRightCanExecute()
        {
            return SelectedAddFriend != null;
        }

        private void OnMoveRightExecute()
        {
            FriendList.Add(SelectedAddFriend);
            AddedFriend.Remove(SelectedAddFriend);

        }

        private bool OnMoveLeftCanExecute()
        {
            return SelectedFriendList != null;
        }

        private void OnMoveLeftExecute()
        {
            AddedFriend.Add(SelectedFriendList);
            FriendList.Remove(SelectedFriendList);
        }

        public async override Task LoadAsync(int? Id)
        {
            var meeting = Id.HasValue ? new MeetingWrapper(await _meetingRepository.getById(Id.Value)) : createNewMeeting();
            Meeting = meeting;

            Meeting.PropertyChanged += (e, s) =>
                {
                    if (!HasChanges)
                    {
                        HasChanges = _meetingRepository.HasChanges();
                    }
                    if (s.PropertyName == nameof(Meeting.HasErrors))
                    {
                        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    }

                };
            FriendList.Clear();
            AddedFriend.Clear();
            var result = await _meetingRepository.getFriendList();
            foreach (var item in result)
            {
                FriendList.Add(item);
            }
            if (Id.HasValue)
            {
                foreach (var friend in Meeting.Friends)
                {
                    AddedFriend.Add(friend);
                }
                var remaningFriend = FriendList.Except(AddedFriend).ToList();
                if (remaningFriend.Count() > 0)
                {
                    FriendList.Clear();
                    foreach (var remaining in remaningFriend)
                    {
                        FriendList.Add(remaining);
                    }
                }
            }

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private MeetingWrapper createNewMeeting()
        {
            var newMeeing = new Meeting();
            _meetingRepository.Add(newMeeing);
            return new MeetingWrapper(newMeeing) { FromDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(30) };
        }

        protected override async void OnDeleteCommand()
        {
            var result = _messageDialogueService.SelectOKCancelMessageBox($"Do you confirm to delete {Meeting.Title} from { Meeting.FromDate} ?", "Confirm");
            if (result == MessageDialogueStatus.Ok)
            {
                _meetingRepository.Remove(Meeting.Model);
                await _meetingRepository.SaveAsync();
                RaiseDeleteDetailsEvent(Meeting.Model.Id);
                Meeting = null;
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Meeting != null && !Meeting.HasErrors && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            this.Meeting.Friends = AddedFriend;
            await _meetingRepository.SaveAsync();
            HasChanges = _meetingRepository.HasChanges();
            RaiseNavigationUpdateEvent(Meeting.Model.Id, Meeting.Title, string.Empty);
        }
    }
}
