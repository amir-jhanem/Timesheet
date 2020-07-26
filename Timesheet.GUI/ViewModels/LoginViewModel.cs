using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using Timesheet.GUI.Models;

namespace Timesheet.GUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        string email;
        string password;
        bool accountInvalid;

        public LoginViewModel()
        {
            AccountInvalid = false;

            var emailEnabled = this.WhenAnyValue(
                    x => x.Email,
                    x => !string.IsNullOrWhiteSpace(x));

            var passwordEnabled = this.WhenAnyValue(
                    x => x.password,
                    x => !string.IsNullOrWhiteSpace(x));

            Login = ReactiveCommand.Create(
                () => new Account { Email = email, Password = password },
                emailEnabled.Merge(passwordEnabled));
        }
        public bool AccountInvalid
        {
            get => accountInvalid;
            set => this.RaiseAndSetIfChanged(ref accountInvalid, value);
        }

        public string Email
        {
            get => email;
            set => this.RaiseAndSetIfChanged(ref email, value);
        }

        public string Password
        {
            get => password;
            set => this.RaiseAndSetIfChanged(ref password, value);
        }

        public ReactiveCommand<Unit, Account> Login { get; }
    }

}
