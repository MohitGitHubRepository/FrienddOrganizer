using FrienddOrganizer.UI.DataService;
using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : Observable, IFriendDetailViewModel
    {
        private IFriendsDataService _friendsDataService { get; }
        public FriendDetailViewModel(IFriendsDataService friendsDataService)
        {
            _friendsDataService = friendsDataService;

        }
        public async Task LoadAsync(int Id)
        {
            var friend = await _friendsDataService.getFriendById(Id);

            if(friend!=null)
            {
                Friend = friend;
            }

        }
        private Friend  _friend;

        public Friend Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertChange();

            }
        }



    }
}
