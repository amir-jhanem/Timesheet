using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using Timesheet.GUI.Models;

namespace Timesheet.GUI.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        string email;
        string password;
        string name;
        string mobileNumber;

        public RegisterViewModel()
        {
            var emailEnabled = this.WhenAnyValue(
                    x => x.Email,
                    x => !string.IsNullOrWhiteSpace(x));

            var passwordEnabled = this.WhenAnyValue(
                    x => x.password,
                    x => !string.IsNullOrWhiteSpace(x));

            Register = ReactiveCommand.Create(
                () => new Account 
                { 
                    Email = email, 
                    Password = password,
                    MobileNumber = mobileNumber,
                    Name = name
                },
                emailEnabled.Merge(passwordEnabled));

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

        public string MobileNumber
        {
            get => mobileNumber;
            set => this.RaiseAndSetIfChanged(ref mobileNumber, value);
        }

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public ReactiveCommand<Unit, Account> Register { get; }
    }

}
