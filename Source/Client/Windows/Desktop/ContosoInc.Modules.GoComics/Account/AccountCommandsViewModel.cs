using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoInc.Modules.GoComics.Account.ViewModels
{
    public class AccountCommandsViewModel : BindableBase
    {
        private string _userAvatar;
        private string _userDisplayName;

        public AccountCommandsViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<EventAggregators.SignInSuccessEvent>().Subscribe(this.UpdateAccountPanel, ThreadOption.UIThread);
        }

        public string UserAvatar
        {
            get { return this._userAvatar; }
            set { SetProperty(ref this._userAvatar, value); }
        }

        public string UserDisplayName
        {
            get { return this._userDisplayName; }
            set { SetProperty(ref this._userDisplayName, value); }
        }

        private void UpdateAccountPanel(Models.Contracts.SignInContract user)
        {
            this.UserAvatar = user.AvatarUrl;
            this.UserDisplayName = user.DisplayName;
        }
    }
}
