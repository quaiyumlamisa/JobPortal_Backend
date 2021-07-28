using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Repository.IRepository
{
    public interface IJob<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        
        void Add(TEntity entity);
        TEntity Get(string CompanyName);
        TEntity Getbyid(long id);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetCom(string CompanyName);
        IEnumerable<TEntity> Getbytitle(string Jobtitle);
        IEnumerable<TEntity> Getbytype(string Jobtype);

        IEnumerable<TEntity> GetC(string CompanyName);


    }
}
