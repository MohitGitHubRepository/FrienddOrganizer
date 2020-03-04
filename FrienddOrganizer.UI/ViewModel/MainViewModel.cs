using FrienddOrganizer.UI.DataService;
using FriendsOrganizer.Modles;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FrienddOrganizer.UI.ViewModel
{
    public class MainViewModel : Observable
    {
        public INavigationViewModel _navigationViewModel { get; }

        public MainViewModel(INavigationViewModel  navigationViewModel)
        {
            _navigationViewModel = navigationViewModel;
        }

        public async Task Load()
        {
            await _navigationViewModel.LoadAsync();
        }
       

       

    }
}
