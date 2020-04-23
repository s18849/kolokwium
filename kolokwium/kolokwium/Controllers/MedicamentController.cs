using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
    public class MedicamentController : ControllerBase
    {
        
        public IActionResult Index()
        {
            return Ok();
        }
    }
}