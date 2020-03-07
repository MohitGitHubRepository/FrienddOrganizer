using FrienddOrganizer.UI.DataService;
using FrienddOrganizer.UI.Events;
using FrienddOrganizer.UI.Wrapper;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrienddOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : Observable, IFriendDetailViewModel
    {
        private IFriendsDataService _friendsDataService { get; }

        private IEventAggregator _eventAggregators;

        public ICommand SaveCommand { get; }

        private FriendWrapper _friend;

        public FriendDetailViewModel(IFriendsDataService friendsDataService, IEventAggregator eventAggregator)
        {
            _friendsDataService = friendsDataService;
            _eventAggregators = eventAggregator;
            _eventAggregators.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnEventRecieved);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);

        }

        public async Task LoadAsync(int Id)
        {
            var friend = await _friendsDataService.getFriendById(Id);
                Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
             {
                 if (e.PropertyName == nameof(Friend.HasErrors))
                 {
                     ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                 }
             };
           

        }



        public FriendWrapper Friend
        {
            get { return _friend; }
            set
            {
                _friend = value;
                OnPropertChange();

            }
        }
        private bool OnSaveCanExecute()
        {
            //TODO:Che checkIn changes only if friend has changes
            return Friend != null && !Friend.HasErrors;
        }

        private async void OnSaveExecute()
        {
            await _friendsDataService.SaveAsync(Friend.Model);
            _eventAggregators.GetEvent<NavigationPropertyUpdateEvent>()
                          .Publish(new NavigationPropertyUpdateArgs() { Id = Friend.Model.Id, Description = $"{Friend.FirstName} {Friend.LastName}" });
        }

        private async void OnEventRecieved(int FriendId)
        {
            await this.LoadAsync(FriendId);

        }







    }
}
