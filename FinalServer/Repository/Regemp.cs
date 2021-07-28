using FinalServer.Data;
using FinalServer.Model;
using FinalServer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Repository
{
    public class Regemp : IReg<Employer>
    {
        readonly ApplicationDbContext _applicationDbContext;

        public Regemp(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public IEnumerable<Employer> GetAll()
        {
            return _applicationDbContext.employers.ToList();

        }
        public Employer Get(long id)
        {
            return _applicationDbContext.employers
                  .FirstOrDefault(e => e.EmployersId == id);
        }
        public void Add(Employer entity)
        {
            _applicationDbContext.employers.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public void Update(Employer dbEntity, Employer entity)
        {
            dbEntity.CompanyName = entity.CompanyName;
            dbEntity.ContactPerson = entity.ContactPerson;
            dbEntity.Location = entity.Location;
            dbEntity.ContactNumber = entity.ContactNumber;
            dbEntity.Email = entity.Email;
            dbEntity.Password = entity.Password;
           

            _applicationDbContext.SaveChanges();
        }

        public void Delete(Employer employer)
        {
            _applicationDbContext.employers.Remove(employer);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Employer> Getbyname(string CompanyName)
        {
            if (string.IsNullOrWhiteSpace(CompanyName))
                return GetAll();

            CompanyName = CompanyName.Trim();
            return _applicationDbContext.employers.Where(a => a.CompanyName == CompanyName).ToList();
        }
    }
}
