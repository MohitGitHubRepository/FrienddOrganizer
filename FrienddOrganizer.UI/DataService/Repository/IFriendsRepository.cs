using System.Threading.Tasks;
using FriendsOrganizer.Modles;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public interface IFriendsRepository
    {
        Task<Friend> getFriendById(int Id);
        Task SaveAsync();
        bool HasChanges();
    }
}