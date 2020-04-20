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
            //context.programmingLanguages.AddOrUpdate(p => p.Id,

            //   new ProgrammingLanguage() { Name = "C#" },
            //   new ProgrammingLanguage() { Name = "Java" },
            //   new ProgrammingLanguage() { Name = "RubyOnRails" },
            //   new ProgrammingLanguage() { Name = "C" },
            //   new ProgrammingLanguage() { Name = "C++" }
            //   );

            //context.Friends.AddOrUpdate(f => f.FirstName,
            //    new Friend() { FirstName = "Thomas", LastName = "Huber", ProgrammingLanguageID = 1 },
            //    new Friend() { FirstName = "Mohit", LastName = "Kumar", ProgrammingLanguageID = 2 },
            //    new Friend() { FirstName = "Kretee", LastName = "Arora", ProgrammingLanguageID = 3 },
            //    new Friend() { FirstName = "Pawan", LastName = "Kumar", ProgrammingLanguageID = 4 },
            //    new Friend() { FirstName = "Shivam", LastName = "Rathore", ProgrammingLanguageID = 1 });

            context.FriendPhoneNumber.AddOrUpdate(p => p.Id,
              new FriendPhoneNumber() { PhoneNumber="789856566", FriendId=1}
               );

        }
    }
}
