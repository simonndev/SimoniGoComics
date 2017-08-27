using System;
using System.IO;

namespace ContosoInc.Modules.GoComics.Observers
{
    public class ImageDownloadObserver : IObserver<MemoryStream>
    {
        public event Action<MemoryStream> Completed;
        public event Action<Exception> ErrorOccurred;

        public MemoryStream ImageStream { get; protected set; }

        public void OnCompleted()
        {
            var handler = this.Completed;
            if (handler != null)
            {
                handler.Invoke(this.ImageStream);
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

        public void OnNext(MemoryStream value)
        {
            this.ImageStream = value;
        }
    }
}
