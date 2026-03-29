using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Products
{//cunstructlar miras yoluya gelmez
    //IGenericRepository nin interfacei ni de miras almalıyım.Bir class bir tane classı miras alabilir ama birden fazla interface implamente alabilir.
    public class ProductRepository(AppDbContext context) : GenericRepository<Product,int>(context), IProductRepository
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count)
        {
            return Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
        }
    }
}
