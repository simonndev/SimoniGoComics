using System;
using ContosoInc.Modules.GoComics.Models.Contracts;

namespace ContosoInc.Modules.GoComics.Observers
{
    public class SignInObserver : IObserver<SignInContract>
    {
        private SignInContract _signInResult;

        public SignInObserver()
        {
        }

        public event Action<SignInContract> Completed;
        public event Action<Exception> ErrorOccurred;

        public void OnCompleted()
        {
            var handler = this.Completed;
            if (handler != null)
            {
                handler.Invoke(this._signInResult);
            }
        }

        public void OnError(Exception error)
        {
            var handler = this.ErrorOccurred;
            if (handler != null)
            {
                handler.Invoke(error);
            }
        }

        public void OnNext(SignInContract value)
        {
            this._signInResult = value;
        }
    }
}
