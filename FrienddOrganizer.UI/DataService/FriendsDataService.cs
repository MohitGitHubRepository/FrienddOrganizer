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
        public async  Task<Friend> getFriendById(int Id)
        {
            using (var ctx = _context())
            {
                return await ctx.Friends.AsNoTracking().SingleAsync(f=> f.Id==Id);
            }
        }
        public async Task SaveAsync(Friend friend)
        {

            using (var ctx = _context())
            {
                ctx.Friends.Attach(friend);
                ctx.Entry(friend).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
                
            }
        }
    }
}
