using FrienddOrganizer.UI.Events;
using FriendsOrganizer.Modles;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrienddOrganizer.UI.ViewModel
{
    public abstract class DetailViewModel : Observable, IDetailViewModel
    {
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        private IEventAggregator _eventAggregators;

        private bool hasChanges;
        public DetailViewModel(IEventAggregator eventAggregator)
        {
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteCommand);
            _eventAggregators = eventAggregator;
        }
        public bool HasChanges
        {
            get { return hasChanges; }
            set
            {
                if (hasChanges != value)
                {
                    hasChanges = value;
                    OnPropertChange();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            }
        }
        public abstract Task LoadAsync(int? Id);
        protected abstract void OnDeleteCommand();
        protected abstract bool OnSaveCanExecute();
        protected abstract void OnSaveExecute();

        protected void RaiseNavigationUpdateEvent(int id, string FirstName, string LastName)
        {
            _eventAggregators.GetEvent<NavigationPropertyUpdateEvent>()
                                      .Publish(new NavigationPropertyUpdateArgs()
                                      {
                                          Id = id,
                                          Description = $"{FirstName} {LastName}",
                                          ViewModelName = this.GetType().Name
                                      });
        }

        protected void RaiseDeleteDetailsEvent(int id)
        {
            _eventAggregators.GetEvent<DetailDeleteEvent>()
                             .Publish(new DeleteDetailEventArg() { Id = id, ViewModelName = this.GetType().Name });
        }

    }
}
