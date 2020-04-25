using FrienddOrganizer.UI.DataService.Repository;
using FrienddOrganizer.UI.Services;
using FrienddOrganizer.UI.ViewModel.Abstract;
using FrienddOrganizer.UI.Wrapper;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel.Concrete
{
    public class MeetingDetailViewModel:DetailViewModel,IMeetingDetailViewModel
    {
        private IMeetingRepository _meetingRepository;
        private IMessageDialogueService _messageDialogueService;
        private MeetingWrapper _meeting;

        public MeetingWrapper Meeting
        {
            get { return _meeting; }
            set
            {
                _meeting = value;
                OnPropertChange();
            }
        }

        public MeetingDetailViewModel(IEventAggregator eventAggregator,IMeetingRepository meetingRepository,IMessageDialogueService messageDialogueService):base(eventAggregator)
        {
            _meetingRepository = meetingRepository;
            _messageDialogueService = messageDialogueService;

        }

        public async override Task LoadAsync(int? Id)
        {
            var meeting = Id.HasValue ? new MeetingWrapper( await _meetingRepository.getById(Id.Value)) : createNewMeeting();
            Meeting = meeting;

            Meeting.PropertyChanged += (e, s) =>
                {
                    if(!HasChanges)
                    {
                        HasChanges = _meetingRepository.HasChanges();
                    }
                    if(s.PropertyName==nameof(Meeting.HasErrors))
                    {
                        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    }

                };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private  MeetingWrapper createNewMeeting()
        {
            var newMeeing = new Meeting();
            _meetingRepository.Add(newMeeing);
            return new MeetingWrapper(newMeeing);
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
            
           await _meetingRepository.SaveAsync();
            HasChanges = _meetingRepository.HasChanges();
            RaiseNavigationUpdateEvent(Meeting.Model.Id, Meeting.Title, string.Empty);
        }
    }
}
