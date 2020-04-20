using FriendsOrganizer.Modles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService.LookupService
{
    public interface IProgrammingLanguages
    {
        Task<IEnumerable<LookUpItem>> getProgrammingLanguages();
    }
}
