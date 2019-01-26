using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dfreeze.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfreeze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreezersController : ControllerBase
    {
        private readonly IFreezeService _freezeService;

        public FreezersController(IFreezeService freezeService)
        {
            _freezeService = freezeService;
        }

        // GET api/freezers/list
        [HttpGet("list")]
        public async Task<JsonResult> List()
        {
            var result = await _freezeService.GetFreezersAsync();
            return new JsonResult(result);
        }

        // POST api/freezers/enable/42
        [HttpPost("enable/{id}")]
        public void Enable(int id)
        {

        }

        // POST api/freezers/disable/42
        [HttpPost("disable/{id}")]
        public void Disable(int id)
        {

        }
    }
}
