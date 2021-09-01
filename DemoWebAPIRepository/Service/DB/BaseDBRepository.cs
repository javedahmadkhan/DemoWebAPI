//
// Copyright:   Copyright (c) 
//
// Description: Base DBRepository Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
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
    ///  This class is used for performing Base DB Repository methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseDBRepository<TEntity> : IDBRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> dbset;
        private readonly DbContext dataBaseContext;

        /// <summary>
        /// Constructor for Base DBRepository
        /// </summary>
        /// <param name="dataBaseContext"></param>
        protected BaseDBRepository(DbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
            this.dbset = dataBaseContext.Set<TEntity>();
        }

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="entity">The entity object to be added</param>
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
        /// Add range of entities
        /// </summary>
        /// <param name="entities">The list of entity object to be added</param>
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
        /// Sets the EntityState of an entity in the db set collection
        /// </summary>
        /// <param name="entity">The entity the EntityState to be set for</param>
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
        /// Adds or updates an entity
        /// </summary>
        /// <param name="entity">Entity object to be added or updated</param>
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
        /// Attachs an entity
        /// </summary>
        /// <param name="entity">The entity object to be attached</param>
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
        /// Remove an entity from a db set collection
        /// </summary>
        /// <param name="id">The id object of entity to be removed</param>
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
        /// Removes an entity from a db set collection
        /// </summary>
        /// <param name="entity">The entity object to be removed</param>
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
        /// Removes range of entities from a db set collection
        /// </summary>
        /// <param name="entities">The range entities to be removed</param>
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
        /// Get an entity from a db set collection
        /// </summary>
        /// <param name="id">The id object of entity</param>
        /// <returns>Resultant entity object</returns>
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
        /// Finds single entity based on key values
        /// </summary>
        /// <param name="keyValues">Array of key values of the entity to be searched for</param>
        /// <returns>Resultant entity object</returns>
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
        /// Executes Linq FirstOrDefault on a db set collection
        /// </summary>
        /// <param name="predicate">predicate function</param>
        /// /// <param name="include">Load Object Map</param>
        /// <param name="disableTracking">disable tracking for changes</param>
        /// <returns>Resultant item</returns>
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
        /// Returns all entities and Include Object Map
        /// </summary>
        /// <param name="predicate">predicate function</param>
        /// <param name="orderBy">order by function</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="records">number of records returns</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>IEnumerable list of entity</returns>
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
        /// Saves changes to the underlying data source and commits the transaction
        /// </summary>
        /// <returns>Number of records affected</returns>
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
        /// Saves changes to the underlying data source and commits the transaction asynchronously
        /// </summary>
        /// <returns>Task of number of records affected</returns>
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
        /// Executes Linq FirstOrDefaultAsync on a db set collection
        /// </summary>
        /// <param name="predicate">predicate function</param>
        /// <param name="include">Load Object Map</param>
        /// <param name="disableTracking">disable tracking for changes</param>
        /// <returns>Task of resultant item</returns>
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
        /// Returns all entities asynchronously and Include Object Map
        /// </summary>
        /// <param name="predicate">predicate function</param>
        /// <param name="orderBy">order by function</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="records">number of records returns</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>IEnumerable list of entity</returns>
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