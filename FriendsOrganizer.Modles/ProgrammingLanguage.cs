using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsOrganizer.Modles
{
    public class ProgrammingLanguage
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
