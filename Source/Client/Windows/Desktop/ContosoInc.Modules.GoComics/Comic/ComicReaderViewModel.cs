using ContosoInc.Presentation.ViewModels;
using Prism;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ContosoInc.Modules.GoComics.Comic.ViewModels
{
    public interface IComicReaderViewModel
    {
        ICommand LatestPageCommand { get; }
        ICommand NextPageCommand { get; }
        ICommand PreviousPageCommand { get; }
        ICommand LastPageCommand { get; }
    }

    public partial class ComicReaderViewModel : DependencyObject, IActiveAware, INavigationAware
    {
        private bool _isActive;

        private readonly IComicPageViewModel _comicPageViewModel;

        public ComicReaderViewModel(IComicPageViewModel comicPageViewModel)
        {
            this._comicPageViewModel = comicPageViewModel;
        }

        public bool IsActive
        {
            get { return this._isActive; }
            set
            {
                if (value)
                {
                    // Hack, download image from Calvin and Hobbes
                    this._comicPageViewModel.GetRecentComicPageImage(344);
                }

                this._isActive = value;
            }
        }


        public event EventHandler IsActiveChanged;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
