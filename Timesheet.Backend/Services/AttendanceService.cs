using System;
using System.Threading.Tasks;
using Timesheet.Backend.Data.EFCore;
using Timesheet.Contracts.Dtos;
using Timesheet.Backend.Models;

namespace Timesheet.Backend.Services
{
    public class AttendanceService: IAttendanceService
    {
        private readonly EfCoreAttendanceSheetRepository _attendanceRepository;

        public AttendanceService(EfCoreAttendanceSheetRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<AttendanceDto> GetAccountAttendance(string accountId, DateTime date)
        {
            var accountAttendance = await _attendanceRepository.GetAccountAttendance(accountId, date);

            if (accountAttendance == null)
            {
                return new AttendanceDto
                {
                    AccountId = accountId,
                    Date = date.Date
                };
            }
            else
            {
                return new AttendanceDto
                {
                    AccountId = accountAttendance.AccountId,
                    Date = accountAttendance.Date,
                    Login = accountAttendance.Login?.ToString(),
                    Logout = accountAttendance.Logout?.ToString()
                };
            }
        }

        public async Task SetAttendanceLogin(SetAttendanceLoginDto model)
        {
            var accountAttendance = await _attendanceRepository.GetAccountAttendance(model.AccountId, model.LoginDate);
            if (accountAttendance == null)
            {
                var attendance = new AttendanceSheet
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountId = model.AccountId,
                    Date = model.LoginDate,
                    Login = TimeSpan.Parse(model.LoginTime)
                };
                await _attendanceRepository.Add(attendance);
            }
            else
            {
                accountAttendance.Login = TimeSpan.Parse(model.LoginTime);
                await _attendanceRepository.Update(accountAttendance);
            }
        }

        public async Task SetAttendanceLogout(SetAttendanceLogoutDto model)
        {
            var accountAttendance = await _attendanceRepository.GetAccountAttendance(model.AccountId, model.LogoutDate);
            if (accountAttendance == null)
            {
                var attendance = new AttendanceSheet
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountId = model.AccountId,
                    Date = model.LogoutDate,
                    Logout = TimeSpan.Parse(model.LogoutTime)
                };
                await _attendanceRepository.Add(attendance);
            }
            else
            {
                accountAttendance.Logout = TimeSpan.Parse(model.LogoutTime);
                await _attendanceRepository.Update(accountAttendance);
            }
        }
    }

}
