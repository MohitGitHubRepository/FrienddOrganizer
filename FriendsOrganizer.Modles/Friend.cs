using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsOrganizer.Modles
{
    public class Friend
    {
        public int Id { get; set; }

        public Friend()
        {
            FriendPhoneNumbers = new Collection<FriendPhoneNumber>();
            Meetings = new Collection<Meeting>();
        }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public int? ProgrammingLanguageID { get; set; }

        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }

        public ICollection<FriendPhoneNumber> FriendPhoneNumbers { get; set; }

        public ICollection<Meeting> Meetings { get; set; }
    }
}
