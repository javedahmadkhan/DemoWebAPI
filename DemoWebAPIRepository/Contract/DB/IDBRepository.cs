using Demo.Entities.DomainEntity.Contract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.Repository.Contract.DB
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDBRepository<TEntity>
         where TEntity : IEntity
    {
        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="entity">The entity object to be added</param>
        void AddEntity(TEntity entity);

        /// <summary>
        /// Add range of entities
        /// </summary>
        /// <param name="entities">The list of entity object to be added</param>
        void AddRange(IList<TEntity> entities);

        /// <summary>
        /// Sets the EntityState of an entity in the db set collection
        /// </summary>
        /// <param name="entity">The entity the EntityState to be set for</param>
        void UpdateEntity(TEntity entity);

        /// <summary>
        /// Adds or updates an entity
        /// </summary>
        /// <param name="entity">Entity object to be added or updated</param>
        void AddOrUpdate(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void AttachEntity(TEntity entity);

        /// <summary>
        /// Remove an entity from a db set collection
        /// </summary>
        /// <param name="id">The id object of entity to be removed</param>
        void Remove(object id);

        /// <summary>
        /// Removes an entity from a db set collection
        /// </summary>
        /// <param name="entity">The entity object to be removed</param>
        void RemoveEntity(TEntity entity);

        /// <summary>
        /// Removes range of entities from a db set collection
        /// </summary>
        /// <param name="entities">The range entities to be removed</param>
        void RemoveRange(List<TEntity> entities);

        /// <summary>
        /// Get an entity from a db set collection
        /// </summary>
        /// <param name="id">The id object of entity</param>
        /// <returns>Resultant entity object</returns>
        TEntity GetByID(object id);

        /// <summary>
        /// Finds single entity based on key values
        /// </summary>
        /// <param name="keyValues">Array of key values of the entity to be searched for</param>
        /// <returns>Resultant entity object</returns>
        TEntity FindEntity(params object[] keyValues);

        /// <summary>
        /// Executes Linq FirstOrDefault on a db set collection
        /// </summary>
        /// <param name="predicate">predicate function</param>
        /// /// <param name="include">Load Object Map</param>
        /// <param name="disableTracking">disable tracking for changes</param>
        /// <returns>Resultant item</returns>
        TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        /// <summary>
        /// Returns all entities and Include Object Map
        /// </summary>
        /// <param name="predicate">predicate function</param>
        /// <param name="orderBy">order by function</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="records">number of records returns</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>IEnumerable list of entity</returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int? records = null,
            bool disableTracking = true);

        /// <summary>
        /// Saves changes to the underlying data source and commits the transaction
        /// </summary>
        /// <returns>Number of records affected</returns>
        int SaveChanges();

        /// <summary>
        /// Saves changes to the underlying data source and commits the transaction asynchronously
        /// </summary>
        /// <returns>Task of number of records affected</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Executes Linq FirstOrDefaultAsync on a db set collection
        /// </summary>
        /// <param name="predicate">predicate function</param>
        /// <param name="include">Load Object Map</param>
        /// <param name="disableTracking">disable tracking for changes</param>
        /// <returns>Task of resultant item</returns>
        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        /// <summary>
        /// Returns all entities asynchronously and Include Object Map
        /// </summary>
        /// <param name="predicate">predicate function</param>
        /// <param name="orderBy">order by function</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="records">number of records returns</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>IEnumerable list of entity</returns>
        Task<IEnumerable<TEntity>> GetAsync(
           Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           int? records = null,
           bool disableTracking = true);
    }
}
