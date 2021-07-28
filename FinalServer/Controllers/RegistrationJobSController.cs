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
    [Route("api/jobseeker")]
    [ApiController]
    public class RegistrationJobSController : ControllerBase
    {
        private readonly IReg<Jobseeker> _dataRepository;
        public RegistrationJobSController(IReg<Jobseeker> dataRepository)
        {
            _dataRepository = dataRepository;
        }


        [HttpGet]
        [Route("GetAllJobSeekers")]
        public IActionResult Get()
        {
            IEnumerable<Jobseeker> jobseekers = _dataRepository.GetAll();
            return Ok(jobseekers);
        }


        [HttpPost]
        [Route("RegisterJobSeekers")]
        public IActionResult Post([FromBody] Jobseeker jobseeker)
        {
            if (jobseeker == null)
            {
                return BadRequest("JobSeeker is null.");
            }
            _dataRepository.Add(jobseeker);
            return Ok(jobseeker);
        }
    }
}

