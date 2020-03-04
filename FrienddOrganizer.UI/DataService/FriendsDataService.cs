using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService
{
    public class FriendsDataService : IFriendsDataService
    {
        public IEnumerable<Friend> getAllFriends()
        {        
            yield return new Friend() { FirstName = "Thomas", LastName = "Huber" };
            yield return new Friend() { FirstName = "Mohit", LastName = "Kumar" };
            yield return new Friend() { FirstName = "Kretee", LastName = "Arora" };
            yield return new Friend() { FirstName = "Pawan", LastName = "Kumar" };
            yield return new Friend() { FirstName = "Shivam", LastName = "Rathore" };
        }
    }
}
