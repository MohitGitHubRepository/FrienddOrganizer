using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.Wrapper
{
    public class FriendPhoneNumberWrapper:ModelWrapper<FriendPhoneNumber>
    {
        public FriendPhoneNumberWrapper(FriendPhoneNumber friendPhoneNumber):base(friendPhoneNumber)
        {
                
        }

          

        public string PhoneNumber
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }


    }
}
