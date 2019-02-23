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
        public async Task<JsonResult> Enable(int id)
        {
            var result = await _freezeService.EnableAsync(id);
            return new JsonResult(result);
        }

        // POST api/freezers/disable/42
        [HttpPost("disable/{id}")]
        public async Task<JsonResult> Disable(int id)
        {
            var result = await _freezeService.DisableAsync(id);
            return new JsonResult(result);
        }
    }
}
