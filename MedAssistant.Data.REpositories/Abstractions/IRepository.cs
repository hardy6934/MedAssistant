
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MedAssistant.Core;
using MedAssistant.DataBase.Entities;

namespace MedAssistant.Data.Repositories.Repositories
{
    public interface IRepository<T> where T : IBaseEntity
    {
        //CRUID METHODS HAVE TO BE REALIZED HERE


        //READ
         Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Get();

        IQueryable<T> FindBy(Expression<Func<T, bool>> searchExpression,
            params Expression<Func<T, object>>[] includes);

        //CREATE
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        //UPDATE
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);
        Task PatchAsync(int id, List<PatchModel> patchData);


        //DELETE
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);

    }
}
