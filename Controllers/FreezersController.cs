using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dfreeze.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dfreeze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreezersController : ControllerBase
    {
        private readonly IFreezerTasksProcessor _processor;
        private readonly IFreezerStateHolder _stateHolder;
        private readonly ILogger _logger;

        public FreezersController(IFreezerStateHolder stateHolder,
            IFreezerTasksProcessor processor,
            ILogger<FreezersController> logger)
        {
            _stateHolder = stateHolder;
            _processor = processor;
            _logger = logger;
        }

        // GET api/freezers/list
        [HttpGet("list")]
        public JsonResult List()
        {
            var result = _stateHolder.GetFreezers();
            return new JsonResult(result);
        }

        // POST api/freezers/enable/42
        [HttpPost("enable/{id}")]
        public JsonResult Enable(int id)
        {
            var task = new FreezerTask(id, 5, true);
            _processor.Enqueue(task);
            var result = _stateHolder.GetFreezers();
            return new JsonResult(result);
        }

        // POST api/freezers/disable/42
        [HttpPost("disable/{id}")]
        public JsonResult Disable(int id)
        {
            var task = new FreezerTask(id, 5, false);
            _processor.Enqueue(task);
            var result = _stateHolder.GetFreezers();
            return new JsonResult(result);
        }
    }
}
