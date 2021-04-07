using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManagementDataContext context;

        public UsersController(UserManagementDataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetUserInfo() {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var u = await this.context.Users.FirstAsync(u => u.NameIdentifier == userId);
            return Ok(u);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll([FromQuery] string? filter = null)
        {
            if (filter != null)
            {
                return Ok(await this.context.Users.Where(u => u.FirstName.Contains(filter)
                || u.LastName.Contains(filter)
                || u.Email.Contains(filter))
                .ToArrayAsync());
            }
            var result = await this.context.Users.ToArrayAsync();

            return Ok(result);
        }

    }
}
