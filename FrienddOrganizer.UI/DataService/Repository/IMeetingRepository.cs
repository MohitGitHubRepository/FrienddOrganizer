using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public interface IMeetingRepository:IRepositoryBase<Meeting>
    {
        Task<IEnumerable<Friend>> getFriendList();
    }
}
