﻿using FrienddOrganizer.UI.DataService;
using FriendsOrganizer.Modles;
using System.Collections.ObjectModel;

namespace FrienddOrganizer.UI.ViewModel
{
    public class MainViewModel : Observable
    {
        public ObservableCollection<Friend> Friends { get; set; }

        private IFriendsDataService _dataProvider;

        private Friend _selectedFriend;
        public MainViewModel(IFriendsDataService dataProvider)
        {
            Friends = new ObservableCollection<Friend>();
            _dataProvider = dataProvider;
            
        }

        public void Load()
        {
            var friends = _dataProvider.getAllFriends();
            Friends.Clear();
            foreach(var friend in friends)
            {
                Friends.Add(friend);
            }
        }
       

        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set {
                _selectedFriend = value;
                OnPropertChange();
            }
        }

    }
}
