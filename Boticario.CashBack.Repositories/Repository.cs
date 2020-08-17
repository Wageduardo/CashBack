// ----------------------------------------
// <copyright file=Repository.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Repositories.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boticario.CashBack.Repositories
{
    /// <summary>
    /// Purchase Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Boticario.CashBack.Repositories.IRepository{T}" />
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly DbContext context;
        
        /// <summary>
        /// The entities
        /// </summary>
        private DbSet<T> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(BoticarioContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return Entities.AsEnumerable().OrderBy(e => e.CreatedTime).ToList();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await Entities.OrderBy(e => e.CreatedTime).ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetById(Guid id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <returns></returns>
        public T GetFirst()
        {
            return Entities.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetFirstAsync()
        {
            return await Entities.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> match)
        {
            return Entities.FirstOrDefault(match);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await Entities.FirstOrDefaultAsync(match);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public List<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Entities.Where(match).OrderBy(e => e.CreatedTime).ToList();
        }

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await Entities.Where(match).OrderBy(e => e.CreatedTime).ToListAsync();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = GetId();
            }
            Entities.Add(entity);
            this.Context.SaveChanges();

        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = GetId();
            }

            Entities.Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }


        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id"></param>
        public void Update(T entity, Guid id)
        {
            T existing = Entities.Find(id);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(entity);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T entity, Guid id)
        {
            if (entity == null)
                return null;

            T existing = await Entities.FindAsync(id);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(entity);
                await Context.SaveChangesAsync();
            }
            return existing;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            Entities.Remove(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(T entity)
        {
            Entities.Remove(entity);
            var ret = await Context.SaveChangesAsync();
            return ret;
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return Entities.Count();
        }

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await Entities.CountAsync();
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> match)
        {
            return Entities.Count(match);
        }

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> match)
        {
            return await Entities.CountAsync(match);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns></returns>
        private Guid GetId()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public DbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = Context.Set<T>();
                }
                return entities;
            }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public DbContext Context
        {
            get
            {
                return context;
            }
        }

        /// <summary>
        /// Add a range of objects
        /// </summary>
        /// <param name="list">The list.</param>
        /// <exception cref="ArgumentNullException">list</exception>
        public void AddRange(List<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Any())
            {
                Entities.AddRange(list);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Add a range of objects asynchronous.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <exception cref="ArgumentNullException">list</exception>
        public Task AddRangeAsync(List<T> list)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            return AddRangeInternalAsync(list);
        }

        private async Task AddRangeInternalAsync(List<T> list)
        {
            if (list.Any())
            {
                await Entities.AddRangeAsync(list);
                await Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Exists the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool Exists(Guid id)
        {
            return Entities.Any(e => e.Id == id);
        }

        /// <summary>
        /// Existses the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> match)
        {
            return await Entities.AnyAsync(match);
        }

        /// <summary>
        /// Exists the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> match)
        {
            return Entities.Any(match);
        }

        /// <summary>
        /// Existses the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await Entities.AnyAsync(e => e.Id == id);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //// text
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}