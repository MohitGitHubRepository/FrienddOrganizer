using FriendsOrganizer.DataAccess;
using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public class MeetingRepository : RepositoryBase<Meeting, FriendSeviceDBContext>, IMeetingRepository
    {

        public MeetingRepository(FriendSeviceDBContext context):base(context)
        {

        }

        public override Task<Meeting> getById(int? Id)
        {
            return _context.Meeting.
                Include(m => m.Friends).
                SingleAsync(m => m.Id == Id);
        }




    }
}
