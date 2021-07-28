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
    [Route("api/empprofile")]
    [ApiController]
    public class EmployerprofileController : ControllerBase
    {
        public readonly ApplicationDbContext _applicationDbContext;

        private readonly IReg<Employer> _dataRepository;
        private readonly IReg<Jobseeker> _dataRepository1;
        private readonly IJob<Jobpost> _dataRepository2;
        private readonly IApply<Apply> _dataRepository4;


        public EmployerprofileController(ApplicationDbContext context, IReg<Employer> dataRepository,
            IReg<Jobseeker> dataRepository1, IJob<Jobpost> dataRepository2, IApply<Apply> dataRepository4)
        {
            _applicationDbContext = context;
            _dataRepository = dataRepository;
            _dataRepository1 = dataRepository1;
            _dataRepository2 = dataRepository2;
            _dataRepository4 = dataRepository4;

        }

        //see profile
        [HttpGet]
        [Route("profile")]
        [Authorize(Roles = "Employer")]
        public IActionResult UserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;

            long a = Convert.ToInt64(userId);

            Employer emp = _applicationDbContext.employers.FirstOrDefault(x => x.EmployersId == a);

            if (emp == null)
            {
                return BadRequest("pai nai");
            }

            
            return Ok(emp);

        }

        //Edit profile
        [HttpPut()]
        [Route("editemp/{id}")]
        [Authorize(Roles = "Employer")]
        public IActionResult Put(long id, [FromBody] Employer emp)
        {
            if (emp == null)
            {
                return BadRequest("Employer is null.");
            }
            Employer empupdate = _dataRepository.Get(id);

            if (empupdate == null)
            {
                return NotFound("The employer record couldn't be found.");
            }
            _dataRepository.Update(empupdate, emp);
            return Ok();
        }



        //delete profile
        [HttpDelete]
        [Route("deleteemp/{id}")]
        [Authorize(Roles = "Employer")]

        public IActionResult Delete(long id)
        {
            Employer j = _dataRepository.Get(id);
            if (j == null)
            {
                return NotFound("The record couldn't be found.");
            }
            _dataRepository.Delete(j);
            return NoContent();
        }



        //post jobs
        [HttpPost]
        [Route("Postjobs")]
        [Authorize(Roles = "Employer")]
        public IActionResult Post([FromBody] Jobpost job)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;

            long a = Convert.ToInt64(userId);

            Employer emp = _applicationDbContext.employers.FirstOrDefault(x => x.EmployersId == a);

            string companyname = emp.CompanyName;


            if (job == null)
            {
                return BadRequest("job is null.");
            }

            Jobpost job1 = new Jobpost();

            job1.CompanyName = companyname;
            job1.JobTitle = job.JobTitle;
            job1.JobDescription = job.JobDescription;
            job1.JobType = job.JobType;
            job1.Experience = job.Experience;
            job1.Salary = job.Salary;
            job1.Vacancy = job.Vacancy;
            job1.Qualification = job.Qualification;
            job1.Skills = job.Skills;
            job1.LastDate = job.LastDate;
            job1.Location = job.Location;

          _dataRepository2.Add(job1);
            return Ok(job1);
        }


        //edit posted jobs
        [HttpPut()]
        [Route("editjobs/{id}")]
        [Authorize(Roles = "Employer")]
        public IActionResult Put(long id, [FromBody] Jobpost job)
        {
            if (job == null)
            {
                return BadRequest("job is null.");
            }
            Jobpost jobToUpdate = _dataRepository2.Getbyid(id);

            if (jobToUpdate == null)
            {
                return NotFound("The job record couldn't be found.");
            }
            _dataRepository2.Update(jobToUpdate, job);
            return Ok();
        }



        //delete jobs
        [HttpDelete]
        [Route("deletejobs/{id}")]
        [Authorize(Roles = "Employer")]

        public IActionResult Deletepost(long id)
        {
            Jobpost job = _dataRepository2.Getbyid(id);
            if (job == null)
            {
                return NotFound("The record couldn't be found.");
            }
            _dataRepository2.Delete(job);
            return NoContent();
        }


        //can see it's posted jobs
        [HttpGet]
        [Route("filterbycompanyname")]
        [Authorize(Roles = "Employer")]
        [HttpHead]
        public IActionResult GetC([FromQuery] string Companyname)
        {
            IEnumerable<Jobpost> employers = _dataRepository2.GetCom(Companyname);
            return Ok(employers);
        }


        //can see all the posted jobs
        [HttpGet]
        [Route("GetAlljobs")]
        [Authorize(Roles = "Employer")]
        public IActionResult Get()
        {
            IEnumerable<Jobpost> alljobs = _dataRepository2.GetAll();
            return Ok(alljobs);
        }


        //can see applicant's list
        [HttpGet]
        [Route("fiterbyempid")]
        [Authorize(Roles = "Employer")]
        [HttpHead]
        public IActionResult Get1([FromQuery] string CompanyName)
        {
            IEnumerable<Apply> emp = _dataRepository4.Get2(CompanyName);
            return Ok(emp);
        }



    }
}
