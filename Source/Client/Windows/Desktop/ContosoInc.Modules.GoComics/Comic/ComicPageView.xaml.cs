using System.Windows.Controls;

namespace ContosoInc.Modules.GoComics.Comic.Views
{
    /// <summary>
    /// Interaction logic for ComicPageView.xaml
    /// </summary>
    public partial class ComicPageView : UserControl
    {
        public ComicPageView(ViewModels.IComicPageViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
