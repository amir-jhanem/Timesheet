using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheet.Backend.Data;
using Timesheet.Backend.Data.EFCore;
using Timesheet.Contracts.Dtos;
using Timesheet.Backend.Models;
using Timesheet.Backend.Services;

namespace Timesheet.Backend.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _service;

        public AccountController(IAccountService service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<TodayAttendanceDto>> Login([FromBody]LoginDto model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.Values);

            var account = await _service.Login(model);
            if (account == null)
                return NotFound();

            return account;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register([FromBody]RegisterDto model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.Values);

            await _service.Register(model);
            return Ok();
        }
    }
}
