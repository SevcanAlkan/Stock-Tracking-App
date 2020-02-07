using Microsoft.EntityFrameworkCore;
using NGA.Core.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NGA.Data.SubStructure
{
    public interface IRepository<T>
    {
        Task<T> GetByID(Guid id);
        IQueryable<T> Get();
        IQueryable<T> Query(bool isDeleted = false);
        void Add(T entity);
        void Update(T entity);
    }

    public class Repository<T> : IRepository<T>
          where T : Base
    {
        private NGADbContext con;
        public Repository(NGADbContext context)
        {
            con = context;
        }
        private NGADbContext Context
        {
            get { return con; }
            set { con = value; }
        }

        public IQueryable<T> Table
        {
            get
            {
                return con.Set<T>();
            }
        }

        public virtual Task<T> GetByID(Guid id)
        {
            return con.Set<T>().FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }
        public IQueryable<T> Get()
        {
            return con.Set<T>().AsQueryable();
        }
        public virtual void Add(T entity)
        {
            con.Set<T>().Add(entity);
        }
        public virtual void Update(T entity)
        {
            con.Entry(entity).State = EntityState.Modified;
        }
        public IQueryable<T> Query(bool isDeleted = false)
        {
            return con.Set<T>().AsNoTracking();
        }
    }
}
