using System.Threading.Tasks;
using Timesheet.Contracts.Dtos;

namespace Timesheet.Backend.Services
{
    public interface IAccountService
    {
        Task<TodayAttendanceDto> Login(LoginDto model);
        Task Register(RegisterDto model);
    }
}
