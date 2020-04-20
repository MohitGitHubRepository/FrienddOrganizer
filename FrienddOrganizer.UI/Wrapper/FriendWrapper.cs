using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Friend>
    {
        public FriendWrapper(Friend friend) : base(friend)
        {

        }

        public string FirstName
        {
            get { return GetValue<string>(nameof(FirstName)); }
            set
            {
                SetValue<string>(value);
            }
        }

        public string LastName
        {
            get { return GetValue<string>(nameof(LastName)); }
            set
            {
                SetValue<string>(value);
            }
        }

        public string Email
        {
            get { return GetValue<string>(nameof(Email)); }
            set
            {
                SetValue<string>(value);
            }
        }
        public int? ProgrammingLanguageID
        {
            get { return GetValue<int?>(nameof(ProgrammingLanguageID)); }
            set
            {
                SetValue<int?>(value);
            }
        }
        //validation Logic
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                       yield return  "Robot is not ValidName";
                    }
                    break;
            }

        }

    }
}
