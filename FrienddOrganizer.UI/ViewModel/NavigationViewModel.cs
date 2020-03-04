using FrienddOrganizer.UI.DataService;
using FriendsOrganizer.Modles;
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

        public NavigationViewModel(ILookupDataService context)
        {
            _context = context;
            FriendsLookUp = new ObservableCollection<LookUpItem>();
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
        

    }
}
