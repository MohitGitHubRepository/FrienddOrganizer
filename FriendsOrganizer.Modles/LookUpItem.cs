using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsOrganizer.Modles
{
    public class LookUpItem
    {
        public int  Id{ get; set; }

        public string Desctiption { get; set; }
    }
    public class NullLookUpItem: LookUpItem
    {
        public new int? Id { get; set; }

         
    }
}
