using FinalServer.Data;
using FinalServer.Model;
using FinalServer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Repository
{
    public class Job : IJob<Jobpost>
    {
        readonly ApplicationDbContext _applicationDbContext;

        public Job(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public IEnumerable<Jobpost> GetAll()
        {
            return _applicationDbContext.jobpost.ToList();

        }
    

        public void Update(Jobpost dbEntity, Jobpost entity)
        {
            dbEntity.CompanyName = dbEntity.CompanyName;
            dbEntity.JobTitle = entity.JobTitle;
            dbEntity.JobDescription = entity.JobDescription;
            dbEntity.JobType = entity.JobType;
            dbEntity.Experience = entity.Experience;
            dbEntity.Salary = entity.Salary;
            dbEntity.Vacancy = entity.Vacancy;
            dbEntity.Qualification = entity.Qualification;
            dbEntity.Skills = entity.Skills;
            dbEntity.LastDate = entity.LastDate;
            dbEntity.Location = entity.Location;


            _applicationDbContext.SaveChanges();
        }

        public void Delete(Jobpost entity)
        {
            _applicationDbContext.jobpost.Remove(entity);
            _applicationDbContext.SaveChanges();
        }





        public Jobpost Getbyid(long id)
        {
            return _applicationDbContext.jobpost
                 .FirstOrDefault(e => e.mapId == id);
        }


        public IEnumerable<Jobpost> GetCom(string CompanyName)
        {
            if (string.IsNullOrWhiteSpace(CompanyName))
                return GetAll();

            CompanyName = CompanyName.Trim();
            return _applicationDbContext.jobpost.Where(a => a.CompanyName == CompanyName).ToList();
        }



        public IEnumerable<Jobpost> Getbytype(string jobtype)
        {
            if (string.IsNullOrWhiteSpace(jobtype))
                return GetAll();

            jobtype= jobtype.Trim();
            return _applicationDbContext.jobpost.Where(a => a.JobType == jobtype).ToList();
        }

        public IEnumerable<Jobpost> Getbytitle(string jobtitle)
        {
            if (string.IsNullOrWhiteSpace(jobtitle))
                return GetAll();

            jobtitle = jobtitle.Trim();
            return _applicationDbContext.jobpost.Where(a => a.JobTitle == jobtitle).ToList();
        }


        public void Add(Jobpost entity)
        {
            _applicationDbContext.jobpost.Add(entity);
            _applicationDbContext.SaveChanges();
        }



        public Jobpost Get(string CompanyName)
        {
            return _applicationDbContext.jobpost
                .FirstOrDefault(e => e.CompanyName == CompanyName);
        }

        public IEnumerable<Jobpost> GetC(string CompanyName)
        {
            if (string.IsNullOrWhiteSpace(CompanyName))
                return GetAll();

            CompanyName = CompanyName.Trim();
            return _applicationDbContext.jobpost.Where(a => a.CompanyName == CompanyName).ToList();
        }


    }
}
