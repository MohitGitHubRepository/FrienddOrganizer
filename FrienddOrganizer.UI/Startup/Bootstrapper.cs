using Autofac;
using FrienddOrganizer.UI.DataService.LookupService;
using FrienddOrganizer.UI.DataService.Repository;
using FrienddOrganizer.UI.Services;
using FrienddOrganizer.UI.ViewModel;
using FrienddOrganizer.UI.ViewModel.Abstract;
using FrienddOrganizer.UI.ViewModel.Concrete;
using FriendsOrganizer.DataAccess;
using Prism.Events;

namespace FrienddOrganizer.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var Builder= new ContainerBuilder();
            Builder.RegisterType<FriendSeviceDBContext>().AsSelf();
            Builder.RegisterType<MainWindow>().AsSelf();
            Builder.RegisterType<MainViewModel>().AsSelf();
            Builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            Builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            Builder.RegisterType<MessageDialogueService>().As<IMessageDialogueService>();
            Builder.RegisterType<FriendDetailViewModel>().Keyed<IDetailViewModel>(nameof(FriendDetailViewModel));
            Builder.RegisterType<MeetingDetailViewModel>().Keyed<IDetailViewModel>(nameof(MeetingDetailViewModel));
            Builder.RegisterType<FriendsRepository>().As<IFriendRepositoryService>();
            Builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();
            Builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            return Builder.Build();
        }
    }
}
