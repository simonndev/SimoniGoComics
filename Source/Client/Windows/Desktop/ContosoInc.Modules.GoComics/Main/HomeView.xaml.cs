using System.Windows.Controls;

namespace ContosoInc.Modules.GoComics.Main.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView(ViewModels.HomeViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
