using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtBaseApiController : ControllerBase
    {
        public static string UserId { get; set; }
    }
}
