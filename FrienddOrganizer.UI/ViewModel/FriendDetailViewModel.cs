using FrienddOrganizer.UI.DataService;
using FrienddOrganizer.UI.DataService.Repository;
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
        private IFriendsRepository _friendsDataService { get; }

        private IEventAggregator _eventAggregators;

        public ICommand SaveCommand { get; }

        private FriendWrapper _friend;

        public FriendDetailViewModel(IFriendsRepository friendsDataService, IEventAggregator eventAggregator)
        {
            _friendsDataService = friendsDataService;
            _eventAggregators = eventAggregator;
          
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);

        }

        public async Task LoadAsync(int Id)
        {
            var friend = await _friendsDataService.getFriendById(Id);
                Friend = new FriendWrapper(friend);
              
            Friend.PropertyChanged += (s, e) =>
             {
                 if(!HasChanges)
                 {
                     HasChanges = _friendsDataService.HasChanges();
                 }
                 if (e.PropertyName == nameof(Friend.HasErrors))
                 {
                     ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                 }
             };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

        }
        private bool hasChanges;

        public bool HasChanges
        {
            get { return hasChanges; }
            set {
                if (hasChanges != value)
                {
                    hasChanges = value;
                    OnPropertChange();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            }
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
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            await _friendsDataService.SaveAsync();

            HasChanges = _friendsDataService.HasChanges();
            _eventAggregators.GetEvent<NavigationPropertyUpdateEvent>()
                          .Publish(new NavigationPropertyUpdateArgs() { Id = Friend.Model.Id, Description = $"{Friend.FirstName} {Friend.LastName}" });
        }

    }
}
