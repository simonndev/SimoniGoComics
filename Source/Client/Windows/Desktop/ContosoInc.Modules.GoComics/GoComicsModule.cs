using ContosoInc.Modules.GoComics.Services;
using ContosoInc.Presentation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace ContosoInc.Modules.GoComics
{
    [Module(ModuleName = "GoComics")]
    public class GoComicsModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public GoComicsModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            this._container.RegisterType<IGoComicsService, GoComicsService>(new ContainerControlledLifetimeManager());

            this._container.RegisterType<Account.ViewModels.ISignInFlyoutViewModel, Account.ViewModels.SignInFlyoutViewModel>(new ContainerControlledLifetimeManager());
            this._container.RegisterType<Main.ViewModels.HomeViewModel>(new ContainerControlledLifetimeManager());
            this._container.RegisterType<Account.ViewModels.AccountCommandsViewModel>(new ContainerControlledLifetimeManager());

            this._container.RegisterType<Comic.ViewModels.IComicPageViewModel, Comic.ViewModels.ComicPageViewModel>(new ContainerControlledLifetimeManager());
            this._container.RegisterType<Comic.ViewModels.ComicReaderViewModel>(new ContainerControlledLifetimeManager());

            this._regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(Main.Views.HomeView));
            this._regionManager.RegisterViewWithRegion(RegionNames.RightWindowCommandsRegion, typeof(Account.Views.AccountCommandsView));
            this._regionManager.RegisterViewWithRegion("AccountRegion", typeof(Account.Views.AccountCommandsView2));

            this._regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(Account.Views.SignInFlyoutView));
        }
    }
}
