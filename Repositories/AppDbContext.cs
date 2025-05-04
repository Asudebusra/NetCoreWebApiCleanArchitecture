using App.Repositories.Categories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) //bir tane dbcontextimiz var farklı farklı olsaydı DbContextOptions<AppDbContext> böyle yazacaktık amayine de yazdık
    {//optionsı App.Api tarafında appsettings de tanımladığımız connectionstringi geçicez
        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());//diyelim ki 50 tane configuration var 
            //hepsini tek tek yazamam yanlış olacak

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Bu repository içerisindeki IEntityTypeConfiguration implement etmiş tüm sınıfları al 
            base.OnModelCreating(modelBuilder); 
        }
        //kötü bir yöntem db contexti kirletmiş oluyoruz
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{




        //    return base.SaveChangesAsync(cancellationToken);
        //}
    }
}
