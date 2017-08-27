using ContosoInc.Core;
using ContosoInc.Presentation;
using ContosoInc.Presentation.Services;
using ContosoInc.Presentation.Views;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContosoInc.Desktop.ViewModels
{
    public interface IShellViewModel
    {
        ICommand ToggleFlyoutCommand { get; }
    }

    public class ShellViewModel : IShellViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly IFlyoutService _flyoutService;

        public ShellViewModel(IRegionManager regionManager, IApplicationCommands commands, IFlyoutService flyoutService)
        {
            this._regionManager = regionManager;
            this._flyoutService = flyoutService;

            this.ToggleFlyoutCommand = new DelegateCommand<string>(this.ExecuteToggleFlyout);

            commands.ToggleFlyoutCommand.RegisterCommand(this.ToggleFlyoutCommand);
        }

        public ICommand ToggleFlyoutCommand { get; private set; }

        private void ExecuteToggleFlyout(string flyoutName)
        {
            _flyoutService.Toggle(flyoutName);
        }
    }
}
