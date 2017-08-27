using Prism.Commands;

namespace ContosoInc.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand ToggleFlyoutCommand { get; }
    }

    public class ApplicationCommandsProxy : IApplicationCommands
    {
        public CompositeCommand ToggleFlyoutCommand
        {
            get { return ApplicationCommands.ToggleFlyoutCommand; }
        }
    }

    public static class ApplicationCommands
    {
        public static CompositeCommand ToggleFlyoutCommand = new CompositeCommand();
    }
}
