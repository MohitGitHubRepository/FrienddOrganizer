using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.Wrapper
{
    public class MeetingWrapper :ModelWrapper<Meeting>
    {
        public MeetingWrapper(Meeting meeting):base(meeting)
        {

        }


        public string Title
        {
            get { return GetValue<string>(nameof(Title)); }
            set { SetValue<string>(value, nameof(Title)); }
        }

        public DateTime FromDate
        {
            get { return GetValue<DateTime>(nameof(FromDate)); }
            set { SetValue<DateTime>(value, nameof(FromDate)); }
        }

        public DateTime EndDate
        {
            get { return GetValue<DateTime>(nameof(EndDate)); }
            set { SetValue<DateTime>(value, nameof(EndDate)); }
        }

        public IEnumerable<Friend> Friends
        {
            get { return GetValue<IEnumerable<Friend>>(nameof(Friends)); }
            set { SetValue<IEnumerable<Friend>>(value, nameof(Friends)); }
        }

    }
}
