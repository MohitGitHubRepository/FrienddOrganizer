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

        public ObservableCollection<LookUpItem> FriendsLookUp { get; set; }

        private IEventAggregator _eventAggregator;

        public NavigationViewModel(ILookupDataService context,IEventAggregator eventAggregator)
        {
            _context = context;
            FriendsLookUp = new ObservableCollection<LookUpItem>();
            _eventAggregator = eventAggregator;
        }

        public async Task LoadAsync()
        {
            var lookup = await _context.getAllLookUpData();
            FriendsLookUp.Clear();
            foreach(var item in lookup)
            {
                FriendsLookUp.Add(item);
            }
        }

        private LookUpItem _selecteedItem;

        public LookUpItem SelectedItem
        {
            get { return _selecteedItem; }
            set
            {
                _selecteedItem = value;
                OnPropertChange();
                if(_eventAggregator!=null)
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(_selecteedItem.Id);
                }
            }

        }


    }
}
