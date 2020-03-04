using FriendsOrganizer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsOrganizer.Modles;
using System.Data.Entity;

namespace FrienddOrganizer.UI.DataService
{
    public class LookupDataService : ILookupDataService
    {
        private Func<FriendSeviceDBContext> _context;

        public LookupDataService(Func<FriendSeviceDBContext> context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LookUpItem>> getAllLookUpData()
        {
            using (var cntxt = _context())
            {
                return await cntxt.Friends.Select(f => new LookUpItem()
                {
                    Id = f.Id,
                    Desctiption = f.FirstName + " " + f.LastName
                }).AsNoTracking().ToListAsync();
            }
        }
    }
}
