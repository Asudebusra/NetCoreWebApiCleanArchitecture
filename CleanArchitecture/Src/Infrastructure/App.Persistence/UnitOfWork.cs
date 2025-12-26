using App.Application.Contracts.Persistence;
namespace App.Persistence
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork// constructorı bu şekilde verebiliriz
    {
        public Task<int> SaveChangeAsync() => context.SaveChangesAsync();

        //{
        //    return context.SaveChangesAsync();
        //}
    }
}
