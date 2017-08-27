using ContosoInc.Modules.GoComics.Models.Contracts;
using System;

namespace ContosoInc.Modules.GoComics.Observers
{
    public class ComicPageObserver : IObserver<FeatureItemContract>
    {
        public event Action<FeatureItemContract> Completed;
        public event Action<Exception> ErrorOccurred;

        public FeatureItemContract ComicPage { get; protected set; }
        
        public void OnCompleted()
        {
            var handler = this.Completed;
            if (handler != null)
            {
                handler.Invoke(this.ComicPage);
            }
        }

        public virtual void OnError(Exception error)
        {
            var handler = ErrorOccurred;
            if (handler != null)
            {
                handler.Invoke(error);
            }
        }

        public void OnNext(FeatureItemContract value)
        {
            this.ComicPage = value;
        }
    }
}
