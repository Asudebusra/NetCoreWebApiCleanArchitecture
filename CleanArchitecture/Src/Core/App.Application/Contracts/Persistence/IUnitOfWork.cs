namespace App.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(); // geriye etkilenen satır sayısını döncek
    }
}
