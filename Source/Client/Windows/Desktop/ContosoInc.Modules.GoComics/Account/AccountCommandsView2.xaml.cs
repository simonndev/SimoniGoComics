using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContosoInc.Modules.GoComics.Account.Views
{
    /// <summary>
    /// Interaction logic for AccountCommandsView2.xaml
    /// </summary>
    public partial class AccountCommandsView2 : UserControl
    {
        public AccountCommandsView2(ViewModels.AccountCommandsViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
