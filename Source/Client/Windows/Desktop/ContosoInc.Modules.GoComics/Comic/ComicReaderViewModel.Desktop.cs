using ContosoInc.Modules.GoComics.Models;
using ContosoInc.Presentation.ViewModels;
using System;
using System.Windows;
using System.Windows.Data;

namespace ContosoInc.Modules.GoComics.Comic.ViewModels
{
    public partial class ComicReaderViewModel : IHeaderInfoProvider<string>
    {
        public static readonly DependencyProperty HeaderInfoProperty =
            DependencyProperty.Register("HeaderInfo", typeof(string), typeof(ComicReaderViewModel), null);

        public string HeaderInfo
        {
            get { return (string)GetValue(HeaderInfoProperty); }
            set { SetValue(HeaderInfoProperty, value); }
        }

        private void SetHeaderInfo(ComicModel comic)
        {
            //This instance of TransactionInfo acts as a "shared model" between this view and the order details view.
            //The scenario says that these 2 views are decoupled, so they don't share the view model, they are only tied
            //with this TransactionInfo
            //this._comicPageViewModel

            //Bind the CompositeOrderView header to a string representation of the TransactionInfo shared instance (we expect the details view model to modify it from user interaction).
            MultiBinding binding = new MultiBinding();
            binding.Bindings.Add(new Binding("Title") { Source = comic });
            binding.Bindings.Add(new Binding("Author") { Source = comic });
            binding.Converter = new ComicPageHeaderConverter();

            BindingOperations.SetBinding(this, HeaderInfoProperty, binding);
        }

        private class ComicPageHeaderConverter : IMultiValueConverter
        {
            /// <summary>
            /// Converts a <see cref="TransactionType"/> and a ticker symbol to a header that can be placed on the TabItem header
            /// </summary>
            /// <param name="values">values[0] should be of type <see cref="TransactionType"/>. values[1] should be a string with the ticker symbol</param>
            /// <param name="targetType"></param>
            /// <param name="parameter"></param>
            /// <param name="culture"></param>
            /// <returns>Returns a human readable string with the transaction type and ticker symbol</returns>
            public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (values == null)
                {
                    throw new ArgumentNullException("values");
                }

                if (values.Length < 2)
                {
                    throw new InvalidOperationException();
                }

                return string.Format("{0} by {1}", values[0], values[1]);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
