using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dfreeze.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dfreeze.Controllers
{
    [Route("api/[controller]")]
    public class ErrorController : Controller
    {
        [Route("")]
        public IActionResult Get()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return StatusCode(500, ex.Error.Message);
        }
    }
}
