// ----------------------------------------
// <copyright file=IRepository.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boticario.CashBack.Repositories
{
    /// <summary>
    /// Repository generic to access database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        DbContext Context { get; }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        DbSet<T> Entities { get; }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="list">The list.</param>
        void AddRange(List<T> list);

        /// <summary>
        /// Adds the range asynchronous.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        Task AddRangeAsync(List<T> list);

        /// <summary>
        /// Anies the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> match);

        /// <summary>
        /// Anies the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> match);

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<int> DeleteAsync(T entity);


        /// <summary>
        /// Existses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool Exists(Guid id);

        /// <summary>
        /// Existses the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        List<T> FindAll(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetById(Guid id);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <returns></returns>
        T GetFirst();

        /// <summary>
        /// Gets the first asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<T> GetFirstAsync();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity, Guid id);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity, Guid id);
    }
}