﻿using FriendsOrganizer.DataAccess;
using FriendsOrganizer.Modles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.DataService.Repository
{
    public class FriendsRepository : IFriendsRepository
    {
        private FriendSeviceDBContext _context;

        public FriendsRepository(FriendSeviceDBContext  context)
        {
            _context = context;
        }

        public void AddFriend(Friend friend)
        {
           
              _context.Friends.Add(friend);
        }

        public async  Task<Friend> getFriendById(int? Id)
        {
                return await _context.Friends.SingleAsync(f=> f.Id==Id);
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void RemoveFriend(Friend friend)
        {
             _context.Friends.Remove(friend);  
        }

        public async Task SaveAsync()
        {
                await _context.SaveChangesAsync();
                
        }
    }
}
