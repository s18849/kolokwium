using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium.DTOs.Requests;
using kolokwium.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
    public class MedicamentController : ControllerBase
    {
        private readonly IDbService _service;

        public MedicamentController(IDbService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult Getprescription(int IdPrescription)
        {
            var result = _service.GetPrescription(IdPrescription);
            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
            
        }
        [HttpPost]
        public IActionResult InsertPrescription(InsertPrescriptionRequest request)
        {
            var result = _service.InsertPrescription(request);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }

        }
    }
}