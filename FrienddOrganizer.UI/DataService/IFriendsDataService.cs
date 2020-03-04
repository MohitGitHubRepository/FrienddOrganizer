using System.Collections.Generic;
using FriendsOrganizer.Modles;

namespace FrienddOrganizer.UI.DataService
{
    public interface IFriendsDataService
    {
        IEnumerable<Friend> getAllFriends();
    }
}