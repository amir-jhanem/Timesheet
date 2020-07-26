using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Contracts.Dtos;
using Timesheet.GUI.Models;
using Timesheet.GUI.Services;

namespace Timesheet.GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        Account account;
        AttendanceSheet attendance;
        public MainWindowViewModel()
        {
            Login();
        }
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }
        public Account Account
        {
            get => account;
            private set => this.RaiseAndSetIfChanged(ref account, value);
        }
        public AttendanceSheet Attendance
        {
            get => attendance;
            private set => this.RaiseAndSetIfChanged(ref attendance, value);
        }

        public void Logout()
        {
            Login();
        }
        public void Login()
        {
            var vm = new LoginViewModel();

            vm.Login.Subscribe(async model =>
            {
                var accountService = new AccountService();

                var result = await accountService.Login(vm.Email, vm.Password);
                if (result.IsSucceded == false)
                {
                    vm.AccountInvalid = true;
                    return;
                }

                var account = result.Response;


                Account = new Account();
                Attendance = new AttendanceSheet();

                Account.Id = account.AccountId;
                Account.Email = vm.Email;
                Account.Name = account.Name;
                Attendance.Date = account.Date;
                Attendance.Login = account.Login;
                Attendance.Logout = account.Logout;


                Content = new HomeViewModel(Account, Attendance);
            });

            Content = vm;
        }

        public void Register()
        {
            var vm = new RegisterViewModel();

            vm.Register.Subscribe(async model =>
            {
                var accountService = new AccountService();

                var account = new RegisterDto
                { 
                    Name = vm.Name,
                    Email = vm.Email,
                    MobileNumber = vm.MobileNumber,
                    Password = vm.Password
                };

                await accountService.Register(account);

                Login();
            });

            Content = vm;
        }
    }
}
