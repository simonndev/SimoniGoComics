using ContosoInc.Modules.GoComics.Observers;
using ContosoInc.Modules.GoComics.Services;
using Prism.Mvvm;
using System;
using System.Windows.Media.Imaging;

namespace ContosoInc.Modules.GoComics.Comic.ViewModels
{
    public interface IComicPageViewModel
    {
        void GetComicPageImage(string imageUrl);
        void GetRecentComicPageImage(int featureId);
    }

    public class ComicPageViewModel : BindableBase, IComicPageViewModel
    {
        private readonly IGoComicsService _gocomics;

        private int _id;
        private int _featureId;
        private BitmapImage _image;
        private int _imageHeight;
        private int _imageWidth;
        private DateTime _publishDate;

        public ComicPageViewModel(IGoComicsService gocomics)
        {
            this._gocomics = gocomics;
        }

        #region Properties

        public int PageId
        {
            get { return this._id; }
            set { SetProperty(ref this._id, value); }
        }

        public int FeatureId
        {
            get { return this._featureId; }
            set { SetProperty(ref this._featureId, value); }
        }

        public BitmapImage Image
        {
            get { return this._image; }
            set { SetProperty(ref this._image, value); }
        }

        public int ImageHeight
        {
            get { return this._imageHeight; }
            set { SetProperty(ref this._imageHeight, value); }
        }

        public int ImageWidth
        {
            get { return this._imageWidth; }
            set { SetProperty(ref this._imageWidth, value); }
        }

        public DateTime PublishDate
        {
            get { return this._publishDate; }
            set { SetProperty(ref this._publishDate, value); }
        }

        #endregion

        public void GetComicPageImage(string imageUrl)
        {
            this.DownloadImage(imageUrl);
        }

        public void GetRecentComicPageImage(int featureId)
        {
            ComicPageObserver observer = new ComicPageObserver();
            observer.Completed += (comicPage) => {
                this.PageId = comicPage.Id;
                this.FeatureId = comicPage.FeatureId;
                this.ImageHeight = comicPage.ImageHeight;
                this.ImageWidth = comicPage.ImageWidth;
                this.PublishDate = DateTime.Parse(comicPage.PublishDateString);

                this.DownloadImage(comicPage.ImageLink);
            };

            this._gocomics.GetRecentFeature(featureId, observer);
        }

        private void DownloadImage(string imageUrl)
        {
            ImageDownloadObserver download = new ImageDownloadObserver();
            download.Completed += imageStream =>
            {
                var image = new BitmapImage();
                image.BeginInit();
                // IMPORTANT: Without this, BitmapImage uses lazy initialization by default and stream will be closed by then.
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = imageStream;
                image.EndInit();

                this.Image = image;
            };

            this._gocomics.DownloadComicPageImage(imageUrl, download);
        }
    }
}
