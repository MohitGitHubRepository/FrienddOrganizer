namespace FriendsOrganizer.DataAccess.Migrations
{
    using FriendsOrganizer.Modles;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendsOrganizer.DataAccess.FriendSeviceDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendsOrganizer.DataAccess.FriendSeviceDBContext context)
        {

            context.Friends.AddOrUpdate(f => f.FirstName,
                new Friend() { FirstName = "Thomas", LastName = "Huber" },
                new Friend() { FirstName = "Mohit", LastName = "Kumar" },
                new Friend() { FirstName = "Kretee", LastName = "Arora" },
                new Friend() { FirstName = "Pawan", LastName = "Kumar" },
                new Friend() { FirstName = "Shivam", LastName = "Rathore" });
        }
    }
}
