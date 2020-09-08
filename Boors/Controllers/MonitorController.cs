using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaMarket.Web.Core.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitorController : ControllerBase
    {
        private readonly ILogger<MonitorController> _logger;
        private readonly IMonitoring _monitoring;
        public MonitorController(ILogger<MonitorController> logger, IMonitoring monitoring)
        {
            _logger = logger;
            _monitoring = monitoring;
        }
        [HttpGet]
        public ActionResult<object> CalculateCredibility()
        {
            var returnValue = _monitoring.StartMonitor();
            if (returnValue.ReturnType != InstaMarket.Web.Core.Enum.ReturnType.Ok)
            {
                return "NOT OK";
            }
            return returnValue.Result;
        }
    }
}
