using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Timesheet.Backend.Data.EFCore;
using Timesheet.Contracts.Dtos;
using Timesheet.Backend.Models;
using Timesheet.Backend.Services;

namespace Timesheet.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route(nameof(GetAccountAttendance))]
        public async Task<ActionResult<AttendanceDto>> GetAccountAttendance(string accountId, DateTime date)
        {
            return await _service.GetAccountAttendance(accountId, date);
        }

        [HttpPost]
        [Route(nameof(SetAttendanceLogin))]
        public async Task<ActionResult> SetAttendanceLogin([FromBody]SetAttendanceLoginDto model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.Values);

            await _service.SetAttendanceLogin(model);

            return Ok();
        }

        [HttpPost]
        [Route(nameof(SetAttendanceLogout))]
        public async Task<ActionResult> SetAttendanceLogout([FromBody]SetAttendanceLogoutDto model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.Values);

            await _service.SetAttendanceLogout(model);

            return Ok();
        }

    }
}