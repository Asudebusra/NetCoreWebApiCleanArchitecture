﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class GenericRepository<T,TId>(AppDbContext context) : IGenericRepository<T,TId> where T : BaseEntity<TId> where TId : struct
    {
        protected AppDbContext Context = context; //miras alınan sınıflarda kullanılsın 


        private readonly DbSet<T> _dbset = context.Set<T>();

        public Task<bool> AnyAsync(TId id) 
        {
            return _dbset.AnyAsync(x => x.Id.Equals(id));
        } 

        //public Task<Boolean> Find ile kullanabilirdik ama 80 tane veri var diyelim ki hepsi gelcek ama gerek yok ki sadece any durumuna bakıcaz

        public async ValueTask AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void Delete(T entity) => _dbset.Remove(entity);

        //public IQueryable<T> GetAll()
        //{
        //    return _dbset.AsQueryable();
        //}

        public IQueryable<T> GetAll() => _dbset.AsQueryable().AsNoTracking();

        public ValueTask<T?> GetByIdAsync(int id) => _dbset.FindAsync(id);

        public void Update(T entity) => _dbset.Update(entity);

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbset.Where(predicate).AsNoTracking();
    }
}
