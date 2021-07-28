using FinalServer.Data;
using FinalServer.Model;
using FinalServer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Repository
{
    public class ApplyAp : IApply<Apply>
    {
        readonly ApplicationDbContext _applicationDbContext;

        public ApplyAp(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }
        public void Add(Apply entity)
        {
            _applicationDbContext.applyjobs.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Apply> Get1(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return GetAll();

            Name = Name.Trim();
            return _applicationDbContext.applyjobs.Where(a => a.Name == Name).ToList();
        }

        public IEnumerable<Apply> Get2(string CompanyName)
        {
            if (string.IsNullOrWhiteSpace(CompanyName))
                return GetAll();

             CompanyName = CompanyName.Trim();
            return _applicationDbContext.applyjobs.Where(a => a.CompanyName == CompanyName).ToList();
        }

        public IEnumerable<Apply> GetAll()
        {
            return _applicationDbContext.applyjobs.ToList();
        }
    }
}
