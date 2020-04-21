using FriendsOrganizer.Modles;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public interface IFriendRepositoryService :IRepositoryBase<Friend>
    {
        void RemovePhoneNumber(FriendPhoneNumber model);
    }
}