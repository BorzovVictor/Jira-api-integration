using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Jira.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Jira.Api.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken token = default);
        Task<TEntity> GetByIdAsync(object id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entityToUpdate);
        Task DeleteAsync(object id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> filter);
        void PatchEntity(TEntity sourceEntity, TEntity existDbEntity);
        Task SaveAsync(CancellationToken token = default);
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity: class
    {
        internal readonly DataContext _context;
        internal DbSet<TEntity> _dbSet;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        
        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken token = default)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (filter!=null)
                query = query.Where(filter);
            if (orderBy!=null)
            {
                return await orderBy(query).ToListAsync(token);
            }
            else
            {
                return await query.ToListAsync(token);
            }
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task DeleteAsync(object id)
        {
            var entityToDelete = await GetByIdAsync(id);
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            if(filter == null)
                throw new ArgumentNullException(nameof(filter), "failed to delete entity");
            var entityToDelete = await _dbSet.FirstOrDefaultAsync(filter);
            if(entityToDelete == null)
                throw new Exception("entity to delete not found");
            _dbSet.Remove(entityToDelete);
        }

        public async Task SaveAsync(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }
        
        public void PatchEntity(TEntity sourceEntity, TEntity existDbEntity)
        {
            foreach (var property in existDbEntity.GetType().GetProperties())
            {
                var oldValue = property.GetValue(existDbEntity);
                var newValue = property.GetValue(sourceEntity);

                if (Equals(oldValue, newValue)) continue;

                property.SetValue(existDbEntity, newValue);
            }
        }
    }
}