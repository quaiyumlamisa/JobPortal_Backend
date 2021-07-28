using FinalServer.Data;
using FinalServer.Model;
using FinalServer.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FinalServer.Controllers
{
    [Route("api/seekerprofile")]
    [ApiController]
    public class JobseekerprofileController : ControllerBase
    {
        public readonly ApplicationDbContext _applicationDbContext;

        private readonly IReg<Employer> _dataRepository;
        private readonly IReg<Jobseeker> _dataRepository1;
        private readonly IJob<Jobpost> _dataRepository3;
        private readonly IApply<Apply> _dataRepository4;

        public JobseekerprofileController(ApplicationDbContext context, IReg<Employer> dataRepository, IReg<Jobseeker> dataRepository1,
                                             IJob<Jobpost> dataRepository3, IApply<Apply> dataRepository4)
        {
            _applicationDbContext = context;
            _dataRepository = dataRepository;
            _dataRepository1 = dataRepository1;
            _dataRepository3 = dataRepository3;
            _dataRepository4 = dataRepository4;


        }

        [HttpGet]
        [Route("profile")]
        [Authorize(Roles = "Jobseeker")]
        public IActionResult UserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;

            long a = Convert.ToInt64(userId);

            Jobseeker emp = _applicationDbContext.jobseekers.FirstOrDefault(x => x.JobSeekerId == a);

            if (emp == null)
            {
                return BadRequest("pai nai");
            }
            return Ok(emp);

        }

        //Edit account's info 
        [HttpPut()]
        [Route("editseeker/{id}")]
        [Authorize(Roles = "Jobseeker")]
        public IActionResult Put(long id, [FromBody] Jobseeker seeker)
        {
            Jobseeker seeker1 = new Jobseeker();
            if (seeker == null)
            {
                return BadRequest("Jobseeker is null.");
            }
            Jobseeker seekerupdate = _dataRepository1.Get(id);

            if (seekerupdate == null)
            {
                return NotFound("The  record couldn't be found.");
            }
            _dataRepository1.Update(seekerupdate, seeker);
            return Ok();
        }



        //delete profile
        [HttpDelete]
        [Route("deleteseeker/{id}")]
        [Authorize(Roles = "Jobseeker")]

        public IActionResult Delete(long id)
        {
            Jobseeker j = _dataRepository1.Get(id);
            if (j == null)
            {
                return NotFound("The record couldn't be found.");
            }
            _dataRepository1.Delete(j);
            return NoContent();
        }

        //upload cv
        [HttpPost, DisableRequestSizeLimit]
        [Route("Uploadcv")]
        [Authorize(Roles = "Jobseeker")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                //  string folderName = "Resources";

                string newPath = Path.Combine("Resources", "Files");

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }


                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(newPath, fileName);
                    var dbPath = Path.Combine(newPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpPost]
        [Route("upcvdata")]
        [Authorize(Roles = "Jobseeker")]
        public IActionResult Post([FromBody] Files file)
        {
            if (file == null)
            {
                return BadRequest("File is null.");
            }
            _applicationDbContext.files.Add(file);
            _applicationDbContext.SaveChanges();
            return Ok(file);
        }



        [HttpGet]
        [Route("getcv/{id}")]
        
        public IActionResult Gete(long id)
        {
            Files file = _applicationDbContext.files
                  .FirstOrDefault(e => e.JobSeekerId == id); ;
            if (file == null)
            {
                return NotFound("The file record couldn't be found.");
            }
            return Ok(file);
        }



        //can see job posts
        [HttpGet]
        [Route("GetAlljobs")]
        [Authorize(Roles = "Jobseeker")]
        public IActionResult Get()
        {
            IEnumerable<Jobpost> alljobs = _dataRepository3.GetAll();
            return Ok(alljobs);
        }


        //can search posted jobs by jobtitle
        [HttpGet]
        [Route("fiterbyjobtitle")]
        [Authorize(Roles = "Jobseeker")]
        [HttpHead]
        public IActionResult GetC4([FromQuery] string jobtitle)
        {
            IEnumerable<Jobpost> jobs = _dataRepository3.Getbytitle(jobtitle);
            return Ok(jobs);
        }

        //can apply for jobs
        [HttpPost]
        [Route("Applyjobs")]
        [Authorize(Roles = "Jobseeker")]
        public IActionResult Post1([FromBody] map m)
        {
           
            Jobseeker seek = _dataRepository1.Get(m.seekerid);
            Jobpost job = _dataRepository3.Getbyid(m.mapid);
            Files file = _applicationDbContext.files
                 .FirstOrDefault(e => e.JobSeekerId == m.seekerid);
            Employer emp = _applicationDbContext.employers
                .FirstOrDefault(e => e.CompanyName == job.CompanyName);

            Apply ap = new Apply();
            ap.CompanyName = job.CompanyName;
            ap.EmployersId =emp.EmployersId;
            ap.JobId = m.mapid;
            ap.JobTitle = job.JobTitle;
            ap.JobType = job.JobType;
            ap.LastDate = job.LastDate;
            ap.JobSeekerId = m.seekerid;
            ap.Filepath = file.Filepath;
            ap.Name = seek.FullName;

            _applicationDbContext.applyjobs.Add(ap);
            _applicationDbContext.SaveChanges();



            return Ok();
        }

        //can see all applications
        [HttpGet]
        [Route("fiterbyseekername")]
        [Authorize(Roles = "Jobseeker")]
        [HttpHead]
        public IActionResult Get1([FromQuery] string Name)
        {
            IEnumerable<Apply> emp = _dataRepository4.Get1(Name);
            return Ok(emp);
        }

    }
}
