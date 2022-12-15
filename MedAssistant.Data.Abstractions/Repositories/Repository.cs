using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; 
using MedAssistant.Core;
using MedAssistant.Data.Repositories.Repositories;
using MedAssistant.DataBase;
using MedAssistant.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedAssistant.Data.Abstractions.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly MedAssistantContext _context;
        private readonly DbSet<T> DbSet;

        public Repository(MedAssistantContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
           await DbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
           await DbSet.AddRangeAsync(entities);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> searchExpression, 
            params Expression<Func<T, object>>[] includes)
        {

            var result = DbSet.Where(searchExpression);

            if (result.Any())
            {
                result = includes.Aggregate(result, (current, include) => 
                current.Include(include));
            }

            return result;

        }

        public virtual IQueryable<T> Get()
        {
            return DbSet;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task PatchAsync(int id, List<PatchModel> patchData)
        {
            var model = await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (model != null) { 
                var nameValuePropertiesPairs = patchData
                    .ToDictionary(
                    patchData => patchData.PropertyName,
                    patchData => patchData.PropertyValue
                    );

                var dbEntityEntry = _context.Entry(model);
                dbEntityEntry.CurrentValues.SetValues(nameValuePropertiesPairs);
                dbEntityEntry.State = EntityState.Modified;
            }
        }

        public virtual void Remove(T entity)
        {
           DbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entity)
        {
            DbSet.RemoveRange(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entity)
        {
            DbSet.UpdateRange(entity);
        }
    }
}
