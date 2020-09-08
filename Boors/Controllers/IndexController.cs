using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Boors.Controllers
{
    [Route("")]
    public class IndexController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "Service Is UP";
        }
    }
}