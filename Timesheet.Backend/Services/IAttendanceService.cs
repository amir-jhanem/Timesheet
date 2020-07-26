using System;
using System.Threading.Tasks;
using Timesheet.Contracts.Dtos;

namespace Timesheet.Backend.Services
{
    public interface IAttendanceService
    {
        Task<AttendanceDto> GetAccountAttendance(string accountId, DateTime date);
        Task SetAttendanceLogin(SetAttendanceLoginDto model);
        Task SetAttendanceLogout(SetAttendanceLogoutDto model);
    }

}
