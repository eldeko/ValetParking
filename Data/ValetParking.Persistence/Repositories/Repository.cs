
using Microsoft.EntityFrameworkCore;
using ValetParking.Persistence.Entities;
using ValetParking.Persistence.Repositories.Contracts;
using ValetParking.Persistence.UnitOfWork;
using System.Collections.Generic;

namespace ValetParking.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private IUnitOfWorkFactory unitOfWork;

        public Repository(IUnitOfWorkFactory uof)
        {
            unitOfWork = uof;
        }

        public DbSet<TEntity> DbSet
        {
            get
            {
                return unitOfWork.Current.Context.Set<TEntity>();
            }
        }

        public TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }

        public void Add(TEntity entity)
        {
            if (entity.Id == 0)
                DbSet.Add(entity);
            unitOfWork.Current.Commit();
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            unitOfWork.Current.Commit();
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
            unitOfWork.Current.Commit();
        }
    }
}