using System.Threading.Tasks;
using FriendsOrganizer.Modles;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public interface IFriendsRepository
    {
        Task<Friend> getFriendById(int? Id);
        void AddFriend(Friend friend);
        Task SaveAsync();
        bool HasChanges();
    }
}