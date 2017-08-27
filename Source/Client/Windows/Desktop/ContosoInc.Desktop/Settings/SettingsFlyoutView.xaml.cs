using ContosoInc.Presentation;
using ContosoInc.Presentation.Views;

namespace ContosoInc.Desktop.Settings.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SettingsFlyoutView : MahApps.Metro.Controls.Flyout, IFlyoutView
    {
        public SettingsFlyoutView()
        {
            InitializeComponent();
        }

        public string FlyoutName
        {
            get { return FlyoutNames.ShellSettingsFlyout; }
        }
    }
}
