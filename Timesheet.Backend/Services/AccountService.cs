using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Backend.Data.EFCore;
using Timesheet.Contracts.Dtos;
using Timesheet.Backend.Models;

namespace Timesheet.Backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly EfCoreAccountRepository _accountRepository;
        private readonly EfCoreAttendanceSheetRepository _attendanceRepository;

        public AccountService(EfCoreAccountRepository accountRepository, EfCoreAttendanceSheetRepository attendanceRepository)
        {
            _accountRepository = accountRepository;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<TodayAttendanceDto> Login(LoginDto model)
        {
            var account = await _accountRepository.Get(model.Email, model.Password);
            if (account == null)
                return null;

            var attendance = await _attendanceRepository.GetTodayAccountAttendance(account.Id);

            return new TodayAttendanceDto
            {
                AccountId = account.Id,
                Name = account.Name,
                Date = attendance?.Date ?? DateTime.Now.Date,
                Login = attendance?.Login?.ToString(),
                Logout = attendance?.Logout?.ToString()
            };
        }

        public async Task Register(RegisterDto model)
        {
            var account = new Account
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                Password = model.Password,
                MobileNumber = model.MobileNumber,
                Name = model.Name
            };
            await _accountRepository.Add(account);
        }
    }

}
