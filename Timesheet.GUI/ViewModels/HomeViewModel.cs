using ReactiveUI;
using System;
using Timesheet.Contracts.Dtos;
using Timesheet.GUI.Models;
using Timesheet.GUI.Services;

namespace Timesheet.GUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        DateTime date;
        string loginTime;
        string logoutTime;
        bool isError;

        private readonly AttendanceService _attendanceService;
        public HomeViewModel(Account account, AttendanceSheet attendance)
        {
            isError = false;
            AccountId = account.Id;
            Name = $"Welcome {account.Name}";
            Date = attendance.Date;
            LoginTime = attendance.Login;
            LogoutTime = attendance.Logout;
            _attendanceService = new AttendanceService();
        }
        
        public string AccountId { get; set; }
        public string Name { get; set; }

        public bool IsError
        {
            get => isError;
            set => this.RaiseAndSetIfChanged(ref isError, value);
        }
        public DateTime Date
        {
            get => date;
            set => this.RaiseAndSetIfChanged(ref date, value);
        }

        public string LoginTime
        {
            get => loginTime;
            set => this.RaiseAndSetIfChanged(ref loginTime, value);
        }

        public string LogoutTime
        {
            get => logoutTime;
            set => this.RaiseAndSetIfChanged(ref logoutTime, value);
        }

        public async void GetAccountAttendance()
        {
            var accountAttendance = (await _attendanceService.GetAccountAttendance(AccountId, Date)).Response;

            LoginTime = accountAttendance.Login;
            LogoutTime = accountAttendance.Logout;
        }

        public async void SetAttendanceLogin()
        {
            var model = new SetAttendanceLoginDto { 
                AccountId = AccountId,
                LoginDate = Date,
                LoginTime = LoginTime
            };

            var result = await _attendanceService.SetAttendanceLogin(model);

            IsError = result.IsSucceded == false;
        }

        public async void SetAttendanceLogout()
        {
            var model = new SetAttendanceLogoutDto
            {
                AccountId = AccountId,
                LogoutDate = Date,
                LogoutTime = LogoutTime
            };

            var result = await _attendanceService.SetAttendanceLogout(model);

            IsError = result.IsSucceded == false;
        }

    }

}
