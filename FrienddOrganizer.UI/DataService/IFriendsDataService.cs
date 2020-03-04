using System.Collections.Generic;
using System.Threading.Tasks;
using FriendsOrganizer.Modles;

namespace FrienddOrganizer.UI.DataService
{
    public interface IFriendsDataService
    {
        Task<List<Friend>> getAllFriends();
    }
}