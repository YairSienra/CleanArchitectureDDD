﻿using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastucture.Repositories
{
    public class RepositorysBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly StreamerDbContext _streamerDb;

        public RepositorysBase(StreamerDbContext streamerDb)
        {
            _streamerDb = streamerDb;
        }

        public async Task<T> AddAsync(T entity)
        {
            _streamerDb.Set<T>().Add(entity);
            await _streamerDb.SaveChangesAsync();
            return entity;  
        }

        public void AddEntity(T entity)
        {
            _streamerDb.Set<T>().Add(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _streamerDb.Set<T>().Remove(entity);
           await _streamerDb.SaveChangesAsync();
        }

        public void DeleteEntity(T entity)
        {
            _streamerDb.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _streamerDb.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _streamerDb.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _streamerDb.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if(predicate != null) query = query.Where(predicate);
            if(orderBy !=null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        } 

        public virtual async Task<T> GetByIdAsync(int id)
        {
             return await _streamerDb.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _streamerDb.Set<T>().Attach(entity);
            _streamerDb.Entry(entity).State = EntityState.Modified;
            await _streamerDb.SaveChangesAsync();
            return entity;
        }

        public void UpdateEntity(T entity)
        {   
            _streamerDb.Set<T>().Attach(entity);
            _streamerDb.Entry(entity).State = EntityState.Modified;
        }
    }
}
