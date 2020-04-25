using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace FriendsOrganizer.Modles
{
    public class Meeting
    {
        public Meeting()
        {
            Friends = new Collection<Friend>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime EndDate { get; set; }

        public  ICollection<Friend> Friends { get; set; }
    }
}
