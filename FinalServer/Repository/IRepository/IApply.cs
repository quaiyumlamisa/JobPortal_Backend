using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Repository.IRepository
{
    public interface IApply<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        IEnumerable<TEntity> Get1(string Name);
        IEnumerable<TEntity> Get2(string CompanyName);
        
    }
}
