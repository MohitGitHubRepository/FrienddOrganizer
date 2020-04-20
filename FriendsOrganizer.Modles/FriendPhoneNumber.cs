using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsOrganizer.Modles
{
    public class FriendPhoneNumber
    {
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public int FriendId { get; set; }

        public Friend Friend { get; set; }
    }
}
