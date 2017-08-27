using ContosoInc.Core;
using ContosoInc.Presentation;
using ContosoInc.Presentation.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContosoInc.Desktop
{
    public class AppBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Application commands
            Container.RegisterType<IApplicationCommands, ApplicationCommandsProxy>();

            // Flyout service
            Container.RegisterInstance<IFlyoutService>(Container.Resolve<FlyoutService>());

            //// Localizer service
            //// Localizer-Service
            //Container.RegisterInstance(typeof(ILocalizerService),
            //    ServiceNames.LocalizerService,
            //    new LocalizerService("de-DE"),
            //    new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());

            //Container.re
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            //// Register ModuleA
            moduleCatalog.AddModule(typeof(ContosoInc.Modules.GoComics.GoComicsModule));
            //// Register ModuleB
            //moduleCatalog.AddModule(typeof(ModuleB.ModuleB));
        }

        protected override DependencyObject CreateShell()
        {
            Container.RegisterInstance(typeof(Window), "Shell", Container.Resolve<Views.ShellView>(), new ContainerControlledLifetimeManager());

            return Container.Resolve<Window>("Shell");
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            // Register views
            var regionManager = this.Container.Resolve<IRegionManager>();
            if (regionManager != null)
            {
                // Add right windows commands
                regionManager.RegisterViewWithRegion(RegionNames.RightWindowCommandsRegion, typeof(Settings.Views.SettingsCommandsView));
                // Add flyouts
                regionManager.RegisterViewWithRegion(RegionNames.FlyoutRegion, typeof(Settings.Views.SettingsFlyoutView));
                // Add tiles to MainRegion
                //regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(HomeTiles));
            }

            //// Register services
            this.RegisterServices();

            Application.Current.MainWindow.Show();
        }

        private void RegisterServices()
        {

        }
    }
}
