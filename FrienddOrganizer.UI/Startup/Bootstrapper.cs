using Autofac;
using FrienddOrganizer.UI.DataService;
using FrienddOrganizer.UI.ViewModel;
using FriendsOrganizer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            Builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            Builder.RegisterType<FriendsDataService>().As<IFriendsDataService>();
            Builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            return Builder.Build();
        }
    }
}
