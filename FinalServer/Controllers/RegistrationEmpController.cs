using FinalServer.Model;
using FinalServer.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Controllers
{
    [Route("api/Employer")]
    [ApiController]
    public class RegistrationEmpController : ControllerBase
    {
        private readonly IReg<Employer> _dataRepository;
        public RegistrationEmpController(IReg<Employer> dataRepository)
        {
            _dataRepository = dataRepository;
        }



        [HttpGet]
        [Route("GetAllEmployers")]
        public IActionResult Get()
        {
            IEnumerable<Employer> employers = _dataRepository.GetAll();
            return Ok(employers);
        }


        [HttpPost]
        [Route("RegisterEmployers")]
        public IActionResult Post([FromBody] Employer employer)
        {
            if (employer == null)
            {
                return BadRequest("Employer is null.");
            }
            _dataRepository.Add(employer);
            return Ok(employer);
        }
    }
}

