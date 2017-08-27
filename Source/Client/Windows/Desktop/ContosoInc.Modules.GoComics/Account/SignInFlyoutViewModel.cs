using ContosoInc.Modules.GoComics.Observers;
using ContosoInc.Modules.GoComics.Services;
using ContosoInc.Presentation.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContosoInc.Modules.GoComics.Account.ViewModels
{
    public interface ISignInFlyoutViewModel
    {
        ICommand SignInCommand { get; }
    }

    public class SignInFlyoutViewModel : BindableBase, ISignInFlyoutViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IGoComicsService _gocomics;
        private readonly IFlyoutService _flyoutService;

        private string _username;
        private string _password;
        private bool _rememberMe;

        public SignInFlyoutViewModel(IGoComicsService gocomics, IFlyoutService flyoutService, IEventAggregator eventAggregator)
        {
            this._gocomics = gocomics;
            this._flyoutService = flyoutService;
            this._eventAggregator = eventAggregator;

            this.SignInCommand = new DelegateCommand<object>(this.ExecuteSignIn, this.CanSignIn);
        }

        public ICommand SignInCommand { get; private set; }

        public string Username
        {
            get { return this._username; }
            set
            {
                SetProperty(ref this._username, value);
            }
        }

        public string Password
        {
            get { return this._password; }
            set
            {
                SetProperty(ref this._password, value);
            }
        }

        public bool RememberMe
        {
            get { return this._rememberMe; }
            set
            {
                SetProperty(ref this._rememberMe, value);
            }
        }

        private void ExecuteSignIn(object parameter)
        {
            SignInObserver signin = new SignInObserver();
            signin.Completed += (auth) =>
            {
                this._flyoutService.Toggle("GoComicsSignInFlyout", hide: true);

                this._eventAggregator.GetEvent<EventAggregators.SignInSuccessEvent>().Publish(auth);
            };
            signin.ErrorOccurred += (error) =>
            {

            };


            this._gocomics.SignIn(this.Username, this.Password, signin);

        }

        private void Signin_Completed(Models.Contracts.SignInContract obj)
        {
            throw new NotImplementedException();
        }

        private bool CanSignIn(object parameter)
        {
            return true;
        }
    }
}
