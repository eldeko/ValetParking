using ValetParking.Persistence.Entities;
using System.Collections.Generic;

namespace ValetParking.Persistence.Repositories.Contracts
    {
    public interface IRepository<TEntity> where TEntity : BaseEntity
        {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        }
    }
    
