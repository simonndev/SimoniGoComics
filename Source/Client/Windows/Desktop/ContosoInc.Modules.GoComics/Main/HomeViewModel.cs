using ContosoInc.Modules.GoComics.Models;
using ContosoInc.Modules.GoComics.Services;
using Prism;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoInc.Modules.GoComics.Main.ViewModels
{
    public class HomeViewModel : BindableBase, IActiveAware, INavigationAware
    {
        private readonly IGoComicsService _gocomics;

        private bool _isActive;

        private ObservableCollection<ComicModel> _popularComics;
        private ComicModel _selectPopularComic;

        private ObservableCollection<ComicModel> _newestComics;
        private ComicModel _selectNewestComic;

        public HomeViewModel(IGoComicsService gocomics)
        {
            this._gocomics = gocomics;
        }

        #region Properties

        public bool IsActive
        {
            get { return this._isActive; }
            set
            {
                if (value)
                {
                    this.PopulatePopularComics();
                    this.PopulateNewestComics();
                }

                this._isActive = value;
            }
        }

        public ObservableCollection<ComicModel> PopularComics
        {
            get { return this._popularComics; }
            set { base.SetProperty(ref this._popularComics, value); }
        }

        public ComicModel SelectPopularComic
        {
            get { return this._selectPopularComic; }
            set { base.SetProperty(ref this._selectPopularComic, value); }
        }

        public ObservableCollection<ComicModel> NewestComics
        {
            get { return this._newestComics; }
            set { base.SetProperty(ref this._newestComics, value); }
        }

        public ComicModel SelectNewestComic
        {
            get { return this._selectNewestComic; }
            set { base.SetProperty(ref this._selectNewestComic, value); }
        }

        #endregion

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

        private void PopulatePopularComics()
        {
            var observer = new Observers.PopularFeaturesObserver();
            observer.Completed += (comics) =>
            {
                this.PopularComics = new ObservableCollection<ComicModel>(comics);
            };

            observer.ErrorOccurred += (error) =>
            {

            };

            this._gocomics.GetPopularFeatures(observer);
        }

        private void PopulateNewestComics()
        {
            var observer = new Observers.NewestFeaturesObserver();
            observer.Completed += (comics) =>
            {
                this.NewestComics = new ObservableCollection<ComicModel>(comics);
            };

            observer.ErrorOccurred += (error) =>
            {

            };

            this._gocomics.GetNewestFeatures(observer);
        }
    }
}
