using Microsoft.EntityFrameworkCore;
using STA.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STA.Data.SubStructure
{
    public interface IUnitOfWork<D> : IDisposable where D : DbContext, new()
    {
        IRepository<T, D> Repository<T>() where T : Base;
        Task<int> SaveChanges();
        void Dispose(bool disposing);
    }
    public class UnitOfWork<D> : IUnitOfWork<D> where D : DbContext, new()
    {
        private D con;
        private bool disposed = false;
        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(D _con)
        {
            con = _con;
        }

        public IRepository<T, D> Repository<T>() where T : Base
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T, D>;
            }
            IRepository<T, D> repository = new Repository<T, D>(con);
            repositories.Add(typeof(T), repository);
            return repository;
        }
        public virtual async Task<int> SaveChanges()
        {
            return await (con as DbContext).SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    (con as DbContext).Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
