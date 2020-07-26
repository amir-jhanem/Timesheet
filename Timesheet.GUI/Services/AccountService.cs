using System.Text;
using System.Threading.Tasks;
using Timesheet.Contracts.Dtos;
using Timesheet.GUI.Http;
using Timesheet.GUI.Http.Models;

namespace Timesheet.GUI.Services
{
    public class AccountService
    {
        public IntegrationManager _integrationManager { get; set; }
        public AccountService()
        {
            _integrationManager = new IntegrationManager();
        }

        public async Task<WebApiResponse<TodayAttendanceDto>> Login(string email, string password)
        {
            return await _integrationManager.Post<LoginDto, TodayAttendanceDto>("/Account/Login", new LoginDto {Email = email, Password = password });
        }

        public async Task Register(RegisterDto model)
        {
            await _integrationManager.Post<RegisterDto, object>("/Account/Register", model);
        }
    }
}
