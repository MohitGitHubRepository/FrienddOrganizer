using FrienddOrganizer.UI.DataService;
using FrienddOrganizer.UI.Events;
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

        public ObservableCollection<NavigationItemViewModel> ItemsLookUp { get; set; }

        private IEventAggregator _eventAggregator;

        public NavigationViewModel(ILookupDataService context,IEventAggregator eventAggregator)
        {
            _context = context;
            ItemsLookUp = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NavigationPropertyUpdateEvent>().Subscribe(NavigationModelUpdated);
          _eventAggregator.GetEvent<DetailDeleteEvent>().Subscribe(NavigationModelDeleted);
        }

        private void NavigationModelDeleted(DeleteDetailEventArg args)
        {
            switch(args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    var friend = ItemsLookUp.Where(a => a.Id == args.Id).FirstOrDefault();
                    if (friend != null)
                    {
                        ItemsLookUp.Remove(friend);
                    }
                    break;
            }
           

        }

        private void NavigationModelUpdated(NavigationPropertyUpdateArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    var friend = ItemsLookUp.Where(a => a.Id == args.Id).FirstOrDefault();
                    if (friend != null)
                    {
                        friend.Description = args.Description;
                    }
                    else
                    {
                        ItemsLookUp.Add(new NavigationItemViewModel(args.Id, args.Description, _eventAggregator, nameof(FriendDetailViewModel)));
                    }
                    break;
            }
        }

        public async Task LoadAsync()
        {
            var lookup = await _context.getAllLookUpData();
            ItemsLookUp.Clear();
            foreach(var item in lookup)
            {
                ItemsLookUp.Add(new NavigationItemViewModel(item.Id,item.Desctiption, _eventAggregator,nameof(FriendDetailViewModel)));
            }
        }
    }
}
