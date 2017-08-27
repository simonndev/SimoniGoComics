using ContosoInc.Presentation;
using ContosoInc.Presentation.Views;

namespace ContosoInc.Modules.GoComics.Account.Views
{
    /// <summary>
    /// Interaction logic for LogInFlyoutView.xaml
    /// </summary>
    public partial class SignInFlyoutView : MahApps.Metro.Controls.Flyout, IFlyoutView
    {
        public SignInFlyoutView(ViewModels.ISignInFlyoutViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        public string FlyoutName
        {
            get { return FlyoutNames.GoComicsSignInFlyout; }
        }
    }
}
