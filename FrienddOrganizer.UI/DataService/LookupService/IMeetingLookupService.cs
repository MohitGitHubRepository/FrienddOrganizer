using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService.LookupService
{
    public interface IMeetingLookupService
    {
        Task<IEnumerable<LookUpItem>> getAllMeetingLookUpData();
    }
}
