using FrienddOrganizer.UI.DataService;
using FriendsOrganizer.Modles;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel
{
    public class MainViewModel : Observable
    {
        public INavigationViewModel NavigationViewModel { get; }
        public IFriendDetailViewModel FriendDetailViewModel { get; set; }

        public MainViewModel(INavigationViewModel  navigationViewModel, IFriendDetailViewModel friendDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            FriendDetailViewModel = friendDetailViewModel;
        }

        public async Task Load()
        {
            await NavigationViewModel.LoadAsync();
        }
       

       

    }
}
