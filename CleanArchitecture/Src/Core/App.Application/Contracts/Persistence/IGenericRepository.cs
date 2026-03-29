using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Contracts.Persistence
{
    //in parametresi datayı alacağımızı belirtiyor
    public interface IGenericRepository<T, TId> where T : class where TId : struct
    {
        public Task<bool> AnyAsync(TId id);

        //IQueryable<T> GetAll();
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);//method overloading
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
