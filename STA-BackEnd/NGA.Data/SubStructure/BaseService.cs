using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NGA.Core.EntityFramework;
using NGA.Core.Model;
using NGA.Core.Validation;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NGA.Data.SubStructure
{
    public interface IBaseService<A, U, G, D>
        where A : AddVM, IAddVM, new()
        where D : Base, IBase, new()
        where U : UpdateVM, IUpdateVM, new()
        where G : BaseVM, IBaseVM, new()
    {
        IRepository<D> Repository { get; }

        Task<G> GetById(Guid id);
        IQueryable<G> GetAll();
        IQueryable<G> GetAll(Expression<Func<D, bool>> expr);

        Task<D> Add(A model, Guid? userId = null, bool isCommit = true);
        Task<D> Update(Guid id, U model, Guid? userId = null, bool isCommit = true);
        Task<bool> Delete(Guid id, Guid? userId = null, bool isCommit = true);
        Task<bool> ReverseDelete(Guid id, Guid? userId, bool isCommit = true);
        Task<int> Commit();
    }

    public class BaseService<A, U, G, D> : IBaseService<A, U, G, D>
        where A : AddVM, IAddVM, new()
        where D : Base, IBase, new()
        where U : UpdateVM, IUpdateVM, new()
        where G : BaseVM, IBaseVM, new()
    {
        protected UnitOfWork uow;
        protected readonly IMapper mapper;
        protected ILogger<BaseService<A, U, G, D>> logger;

        public BaseService(UnitOfWork _uow, IMapper _mapper, ILogger<BaseService<A, U, G, D>> _logger)
        {
            uow = _uow;
            mapper = _mapper;
            logger = _logger;
        }

        public IRepository<D> Repository
        {
            get
            {
                return uow.Repository<D>();
            }
        }

        public virtual Task<G> GetById(Guid id)
        {
            try
            {
                return Repository.Query().Where(x => x.Id == id).Select(s => mapper.Map<D, G>(s)).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "BaseService");
                return null;
            }
        }
        public virtual IQueryable<G> GetAll()
        {
            try
            {
                return mapper.ProjectTo<G>(Repository.Query());
            }
            catch (Exception e)
            {
                logger.LogError(e, "BaseService");
                return null;
            }
        }
        public virtual IQueryable<G> GetAll(Expression<Func<D, bool>> expr)
        {
            try
            {
                return mapper.ProjectTo<G>(Repository.Query().Where(expr));
            }
            catch (Exception e)
            {
                logger.LogError(e, "BaseService");
                return null;
            }
        }

        public virtual async Task<D> Add(A model, Guid? userId = null, bool isCommit = true)
        {
            try
            {
                Guid _userId = userId == null ? Guid.Empty : userId.Value;

                D entity = mapper.Map<A, D>(model);
                if (entity.Id == null || entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                if (entity is ITable)
                {
                    (entity as ITable).CreateBy = _userId;
                    (entity as ITable).CreateDT = DateTime.Now;
                }

                Repository.Add(entity);

                if (isCommit)
                    await Commit();

                return entity;
            }
            catch (Exception e)
            {
                logger.LogError(e, "BaseService");
                return null;
            }
        }
        public virtual async Task<D> Update(Guid id, U model, Guid? userId = null, bool isCommit = true)
        {
            try
            {
                Guid _userId = userId == null ? Guid.Empty : userId.Value;

                D entity = await uow.Repository<D>().GetByID(id);
                if (Validation.IsNull(entity))
                    return null;

                entity = mapper.Map<U, D>(model, entity);

                if (entity is ITable)
                {
                    (entity as ITable).UpdateBy = _userId;
                    (entity as ITable).UpdateDT = DateTime.Now;
                }

                Repository.Update(entity);

                if (isCommit)
                    await Commit();

                return entity;
            }
            catch (Exception e)
            {
                logger.LogError(e, "BaseService");
                return null;
            }
        }
        public virtual async Task<bool> Delete(Guid id, Guid? userId = null, bool isCommit = true)
        {
            try
            {
                Guid _userId = userId == null ? Guid.Empty : userId.Value;

                D entity = await uow.Repository<D>().GetByID(id);
                if (Validation.IsNull(entity))
                    return false;

                if (entity is ITable)
                {
                    (entity as ITable).UpdateBy = _userId;
                    (entity as ITable).UpdateDT = DateTime.Now;
                }

                entity.IsDeleted = true;
                Repository.Update(entity);

                if (isCommit)
                    await Commit();

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, "BaseService");
                return false;
            }
        }
        public virtual async Task<bool> ReverseDelete(Guid id, Guid? userId, bool isCommit = true)
        {
            try
            {
                Guid _userId = userId == null ? Guid.Empty : userId.Value;

                D entity = await uow.Repository<D>().GetByID(id);
                if (Validation.IsNull(entity))
                    return false;

                if (entity is ITable)
                {
                    (entity as ITable).UpdateBy = _userId;
                    (entity as ITable).UpdateDT = DateTime.Now;
                }

                entity.IsDeleted = false;
                Repository.Update(entity);

                if (isCommit)
                    await Commit();

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e, "BaseService");
                return false;
            }
        }

        public virtual async Task<int> Commit()
        {
            try
            {
                return await uow.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError(e, "BaseService");
                return 0;
            }
        }
    }
}
