using FinalServer.Data;
using FinalServer.Model;
using FinalServer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FinalServer.Repository
{
    public class RegJob : IReg<Jobseeker>
    {
        readonly ApplicationDbContext _applicationDbContext;

        public RegJob(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public IEnumerable<Jobseeker> GetAll()
        {
            return _applicationDbContext.jobseekers.ToList();

        }
        public void Add(Jobseeker entity)
        {
            _applicationDbContext.jobseekers.Add(entity);
            _applicationDbContext.SaveChanges();
        }


         public void Update(Jobseeker dbEntity, Jobseeker entity)
        {
            dbEntity.FullName = entity.FullName;
            dbEntity.PhoneNumber = entity.PhoneNumber;
            dbEntity.Email = entity.Email;
            dbEntity.Password = entity.Password;
           

            _applicationDbContext.SaveChanges();
        }

        public void Delete(Jobseeker jobseeker)
        {
            _applicationDbContext.jobseekers.Remove(jobseeker);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Jobseeker> Getbyname(string FullName)
        {
            if (string.IsNullOrWhiteSpace(FullName))
                return GetAll();

            FullName = FullName.Trim();
            return _applicationDbContext.jobseekers.Where(a => a.FullName == FullName).ToList();
        }

        public Jobseeker Get(long id)
        {
            return _applicationDbContext.jobseekers
                  .FirstOrDefault(e => e.JobSeekerId == id);
        }


    }
}