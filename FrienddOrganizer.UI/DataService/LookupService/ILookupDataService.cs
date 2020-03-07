using System.Collections.Generic;
using System.Threading.Tasks;
using FriendsOrganizer.Modles;

namespace FrienddOrganizer.UI.DataService
{
    public interface ILookupDataService
    {
        Task<IEnumerable<LookUpItem>> getAllLookUpData();
    }
}