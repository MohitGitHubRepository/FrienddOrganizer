using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsOrganizer.DataAccess
{
    public class FriendSeviceDBContext :DbContext
    {

        public FriendSeviceDBContext():base("FriendOrganizerDB")
        {

        }
        public DbSet<Friend> Friends { get; set; }

        public DbSet<ProgrammingLanguage> programmingLanguages { get; set; }

        public DbSet<FriendPhoneNumber> FriendPhoneNumber { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //var user_table = modelBuilder.Entity<Friend>();
            //user_table
            //    .ToTable("Friend");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //var friend_Table = modelBuilder.Entity<Friend>();
            //friend_Table.Property(f => f.FirstName)
            //    .IsRequired()
            //    .HasMaxLength(50);
        }
    }
}
