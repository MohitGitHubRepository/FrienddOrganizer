using FriendsOrganizer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendsOrganizer.Modles;
using System.Data.Entity;

namespace FrienddOrganizer.UI.DataService.LookupService
{
    public class LookupDataService : ILookupDataService , IProgrammingLanguages,IMeetingLookupService
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

        public async Task<IEnumerable<LookUpItem>> getProgrammingLanguages()
        {
            using (var cntxt = _context())
            {
                return await cntxt.programmingLanguages.Select(f => new LookUpItem()
                {
                    Id = f.Id,
                    Desctiption = f.Name
                }).AsNoTracking().ToListAsync();
            }
        }
        public async Task<IEnumerable<LookUpItem>> getAllMeetingLookUpData()
        {
            using (var cntxt = _context())
            {
                return await cntxt.Meeting.Select(f => new LookUpItem()
                {
                    Id = f.Id,
                    Desctiption = f.Title
                }).AsNoTracking().ToListAsync();
            }
        }
    }
}
