using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Contracts.Dtos;
using Timesheet.GUI.Http;
using Timesheet.GUI.Http.Models;

namespace Timesheet.GUI.Services
{
    public class AttendanceService
    {
        public IntegrationManager _integrationManager { get; set; }
        public AttendanceService()
        {
            _integrationManager = new IntegrationManager();
        }

        public async Task<WebApiResponse<AttendanceDto>> GetAccountAttendance(string accountId, DateTime date)
        {
            var keys = new Dictionary<string, object>
            {
                { nameof(accountId), accountId },
                { nameof(date), date }
            };

            return await _integrationManager.Get<AttendanceDto>("/Attendance/GetAccountAttendance", keys);
        }

        public async Task<WebApiResponse<object>> SetAttendanceLogin(SetAttendanceLoginDto model)
        {
            return await _integrationManager.Post<SetAttendanceLoginDto, object>("/Attendance/SetAttendanceLogin", model);
        }

        public async Task<WebApiResponse<object>> SetAttendanceLogout(SetAttendanceLogoutDto model)
        {
            return await _integrationManager.Post<SetAttendanceLogoutDto, object>("/Attendance/SetAttendanceLogout", model);
        }
    }
}
