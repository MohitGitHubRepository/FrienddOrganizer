using FrienddOrganizer.UI.DataService;
using FrienddOrganizer.UI.DataService.LookupService;
using FrienddOrganizer.UI.Events;
using FrienddOrganizer.UI.ViewModel.Concrete;
using FriendsOrganizer.Modles;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel
{
    public class NavigationViewModel : Observable, INavigationViewModel
    {
        private ILookupDataService _context;
        private IMeetingLookupService _meetingLookupService;

        public ObservableCollection<NavigationItemViewModel> ItemsLookUp { get; set; }

        public ObservableCollection<NavigationItemViewModel> MeetingsLookUp { get; set; }

        private IEventAggregator _eventAggregator;

        public NavigationViewModel(ILookupDataService context,IMeetingLookupService meetingLookupService,IEventAggregator eventAggregator)
        {
            _context = context;
            _meetingLookupService = meetingLookupService;
            ItemsLookUp = new ObservableCollection<NavigationItemViewModel>();
            MeetingsLookUp = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NavigationPropertyUpdateEvent>().Subscribe(NavigationModelUpdated);
          _eventAggregator.GetEvent<DetailDeleteEvent>().Subscribe(NavigationModelDeleted);
        }

        private void NavigationModelDeleted(DeleteDetailEventArg args)
        {
            switch(args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    DeleteNavigationViewModelItem(ItemsLookUp, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    DeleteNavigationViewModelItem(MeetingsLookUp, args);
                    break;
            }
           

        }

        private void DeleteNavigationViewModelItem(ObservableCollection<NavigationItemViewModel> Items, DeleteDetailEventArg args)
        {
            var item = Items.Where(a => a.Id == args.Id).FirstOrDefault();
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        private void NavigationModelUpdated(NavigationPropertyUpdateArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    UpdateNavigationViewModel(ItemsLookUp, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    UpdateNavigationViewModel(MeetingsLookUp, args);
                    break;
            }
        }

        private void UpdateNavigationViewModel(ObservableCollection<NavigationItemViewModel> items, NavigationPropertyUpdateArgs args)
        {
            var item = items.Where(a => a.Id == args.Id).FirstOrDefault();
            if (item != null)
            {
                item.Description = args.Description;
            }
            else
            {
                items.Add(new NavigationItemViewModel(args.Id, args.Description, _eventAggregator, args.ViewModelName));
            }
        }

        public async Task LoadAsync()
        {
            await LoadFriendData();
            await LoadMeetingData();
        }

        private async Task LoadMeetingData()
        {
            var meetingLookUp = await _meetingLookupService.getAllMeetingLookUpData();
            MeetingsLookUp.Clear();
            foreach (var item in meetingLookUp)
            {
                MeetingsLookUp.Add(new NavigationItemViewModel(item.Id, item.Desctiption, _eventAggregator, nameof(MeetingDetailViewModel)));
            }
        }

        private async Task LoadFriendData()
        {
            var lookup = await _context.getAllLookUpData();
            ItemsLookUp.Clear();
            foreach (var item in lookup)
            {
                ItemsLookUp.Add(new NavigationItemViewModel(item.Id, item.Desctiption, _eventAggregator, nameof(FriendDetailViewModel)));
            }
        }
    }
}
