using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrator")]
    public class GroupsController : ControllerBase
    {
        private readonly UserManagementDataContext context;

        public GroupsController(UserManagementDataContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleGroup([FromRoute] int id)
        {
            var g = await this.context.Groups.FirstAsync(g => g.Id == id);
            return g == null ? NotFound() : Ok(g);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll() => Ok(await this.context.Groups.ToArrayAsync());
    }
}
