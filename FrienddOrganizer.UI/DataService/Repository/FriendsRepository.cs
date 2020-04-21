using FriendsOrganizer.DataAccess;
using FriendsOrganizer.Modles;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public class FriendsRepository : RepositoryBase<Friend, FriendSeviceDBContext>, IFriendRepositoryService
    {
        public FriendsRepository(FriendSeviceDBContext context) : base(context)
        {

        }

        public override async Task<Friend> getById(int? Id)
        {
            return await _context.Friends
            .Include(b => b.FriendPhoneNumbers).
            SingleAsync(f => f.Id == Id);
        }

        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            _context.FriendPhoneNumber.Remove(model);
        }
    }
}
