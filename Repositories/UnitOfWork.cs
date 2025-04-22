using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork// constructorı bu şekilde verebiliriz
    {
        public Task<int> SaveChangeAsync() => context.SaveChangesAsync();

        //{
        //    return context.SaveChangesAsync();
        //}
    }
}
