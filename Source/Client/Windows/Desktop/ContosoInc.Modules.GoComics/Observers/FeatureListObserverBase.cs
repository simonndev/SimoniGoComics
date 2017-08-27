using ContosoInc.Modules.GoComics.Models;
using System;
using System.Collections.Generic;

namespace ContosoInc.Modules.GoComics.Observers
{
    public class FeatureListObserverBase<T> : IObserver<T> where T : class, new()
    {
        public FeatureListObserverBase()
        {
            this.Features = new List<ComicModel>();
        }

        public event Action<List<ComicModel>> Completed;
        public event Action<Exception> ErrorOccurred;

        public List<ComicModel> Features { get; protected set; }

        public virtual void OnError(Exception error)
        {
            var handler = ErrorOccurred;
            if (handler != null)
            {
                handler.Invoke(error);
            }
        }

        public virtual void OnCompleted()
        {
            var handler = Completed;
            if (handler != null)
            {
                handler.Invoke(this.Features);
            }
        }

        public virtual void OnNext(T value)
        {
        }
    }
}
