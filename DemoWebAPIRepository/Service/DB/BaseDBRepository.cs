using Demo.Entities.DomainEntity.Base;
using Demo.Repository.Contract.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.Repository.Service.DB
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseDBRepository<TEntity> : IDBRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> dbset;
        private readonly DbContext dataBaseContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseContext"></param>
        protected BaseDBRepository(DbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
            this.dbset = dataBaseContext.Set<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddEntity(TEntity entity)
        {
            try
            {
                this.dbset.Add(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(IList<TEntity> entities)
        {
            try
            {
                this.dbset.AddRange(entities);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void UpdateEntity(TEntity entity)
        {
            try
            {
                if (this.dataBaseContext.GetType().Name == "DbContextProxy")
                {
                    this.dbset.Attach(entity);
                }
                else
                {
                    if (this.dataBaseContext.Entry(entity).State == EntityState.Detached)
                    {
                        this.dbset.Attach(entity);
                    }

                    this.dataBaseContext.Entry(entity).State = EntityState.Modified;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AddOrUpdate(TEntity entity)
        {
            try
            {
                entity.UpdatedBy = "";
                entity.UpdatedDate = DateTime.UtcNow;

                if (entity.Id == 0)
                {
                    entity.CreatedBy = "";
                    entity.CreatedDate = DateTime.UtcNow;

                    this.AddEntity(entity);
                }
                else
                {
                    this.UpdateEntity(entity);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void AttachEntity(TEntity entity)
        {
            try
            {
                this.dbset.Attach(entity);

                entity.UpdatedBy = "";
                entity.UpdatedDate = DateTime.UtcNow;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Remove(object id)
        {
            try
            {
                TEntity entity = this.dbset.Find(id);
                this.RemoveEntity(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void RemoveEntity(TEntity entity)
        {
            try
            {
                if (this.dataBaseContext.Entry(entity).State == EntityState.Detached)
                {
                    this.dbset.Attach(entity);
                }

                this.dbset.Remove(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public virtual void RemoveRange(List<TEntity> entities)
        {
            try
            {
                foreach (var item in entities)
                {
                    if (this.dataBaseContext.Entry(item).State == EntityState.Detached)
                    {
                        this.dbset.Attach(item);
                    }
                }

                this.dbset.RemoveRange(entities);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            try
            {
                return this.dbset.Find(id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual TEntity FindEntity(params object[] keyValues)
        {
            try
            {
                return this.dbset.Find(keyValues);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            try
            {
                var query = this.dbset.AsQueryable();

                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (include != null)
                {
                    query = include(query);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return query.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="records"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int? records = null,
            bool disableTracking = true)
        {
            try
            {
                IQueryable<TEntity> query = this.dbset.AsQueryable();

                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (include != null)
                {
                    query = include(query);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                if (records != null)
                {
                    query = query.Take((int)records);
                }

                return query.ToList();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChanges()
        {
            try
            {
                return this.dataBaseContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Task<int> SaveChangesAsync()
        {
            try
            {
                return this.dataBaseContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            try
            {
                var query = this.dbset.AsQueryable();

                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (include != null)
                {
                    query = include(query);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return await query.FirstOrDefaultAsync().ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="records"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
           Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           int? records = null,
           bool disableTracking = true)
        {
            try
            {
                IQueryable<TEntity> query = this.dbset.AsQueryable();

                if (disableTracking)
                {
                    query = query.AsNoTracking();
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (include != null)
                {
                    query = include(query);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                if (records != null)
                {
                    query = query.Take((int)records);
                }

                return await query.ToListAsync().ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }
    }
}