using FinalServer.Data;
using FinalServer.Model;
using FinalServer.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminProfileController : ControllerBase
    {
        public readonly ApplicationDbContext _applicationDbContext;

        private readonly IReg<Employer> _dataRepository;
        private readonly IReg<Jobseeker> _dataRepository1;
        private readonly IJob<Jobpost> _dataRepository2;
        private readonly IApply<Apply> _dataRepository3;


        public AdminProfileController(ApplicationDbContext context, IReg<Employer> dataRepository, IReg<Jobseeker> dataRepository1, IJob<Jobpost> dataRepository2, IApply<Apply> dataRepository3)
        {
            _applicationDbContext = context;
            _dataRepository = dataRepository;
            _dataRepository1 = dataRepository1;
            _dataRepository2 = dataRepository2;
            _dataRepository3 = dataRepository3;


        }

        //can see it's profile
        [HttpGet]
        [Route("profile")]
        [Authorize(Roles = "Admin")]
        public IActionResult UserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;

            long a = Convert.ToInt64(userId);

            Admin emp = _applicationDbContext.admin.FirstOrDefault(x => x.AdminId == a);
            if (emp == null)
            {
                return BadRequest("pai nai");
            }
            return Ok(emp);

        }


        //can see all the jobseeker's list
        [HttpGet]
        [Route("GetAllJobSeekers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetJ()
        {
            IEnumerable<Jobseeker> jobseekers = _dataRepository1.GetAll();
            return Ok(jobseekers);
        }

        //can see all the employer's list
        [HttpGet]
        [Route("GetAllEmployers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetE()
        {
            IEnumerable<Employer> employers = _dataRepository.GetAll();
            return Ok(employers);
        }

        //can see all the posted jobs
        [HttpGet]
        [Route("GetAlljobs")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            IEnumerable<Jobpost> alljobs = _dataRepository2.GetAll();
            return Ok(alljobs);
        }


        [HttpGet]
        [Route("GetAlljobsh")]    
        public IActionResult Geth()
        {
            IEnumerable<Jobpost> alljobs = _dataRepository2.GetAll();
            return Ok(alljobs);
        }


        //can search posted jobs by jobtitle
        [HttpGet]
        [Route("filterbytitle")]
        [Authorize(Roles = "Admin")]
        [HttpHead]
        public IActionResult GetC4([FromQuery] string jobtitle)
        {
            IEnumerable<Jobpost> jobs = _dataRepository2.Getbytitle(jobtitle);
            return Ok(jobs);
        }

       
        //can delete jobseeker's profile
        [HttpDelete]
        [Route("deleteseeker/{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Delete1(long id)
        {
            Jobseeker j = _dataRepository1.Get(id);
            if (j == null)
            {
                return NotFound("The record couldn't be found.");
            }
            _dataRepository1.Delete(j);
            return NoContent();
        }



        //can delete employer's profile
        [HttpDelete]
        [Route("deleteemp/{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(long id)
        {
            Employer j = _dataRepository.Get(id);
            if (j == null)
            {
                return NotFound("The record couldn't be found.");
            }
          

            IEnumerable<Jobpost> job = _dataRepository2.GetCom(j.CompanyName);

            for (int i = 0; i < job.Count(); i++)
            {
                
                Jobpost jo = job.ElementAt(i);
               
                if (jo == null)
                {
                    return NotFound("The record couldn't be found.");
                }
                _dataRepository2.Delete(jo);
                
            }

            _dataRepository.Delete(j);

            return NoContent();
        }

        //can see applicant's list
        [HttpGet]
        [Route("GetAlljobsap")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get1()
        {
            IEnumerable<Apply> appliedjobs = _dataRepository3.GetAll();
            return Ok(appliedjobs);
        }









    }
}
