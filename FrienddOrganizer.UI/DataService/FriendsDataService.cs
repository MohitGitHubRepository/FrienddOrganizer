using FriendsOrganizer.DataAccess;
using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService
{
    public class FriendsDataService : IFriendsDataService
    {
        private Func<FriendSeviceDBContext> _context;

        public FriendsDataService(Func<FriendSeviceDBContext>  context)
        {
            _context = context;
        }
        public async  Task<List<Friend>> getAllFriends()
        {
            using (var ctx = _context())
            {
                return await ctx.Friends.AsNoTracking().ToListAsync();
            }
        }
    }
}
