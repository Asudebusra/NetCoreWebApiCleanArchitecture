namespace App.Domain.Events
{
    public record ProductAddedEvent(int Id, string Name, decimal Price): IEventOrMessage;//Nesne örneği oluştuktan sonra asla bi değişiklik hakkımız yok.

    //public record ProductAddedEvent2
    //{
    //    public int Id { get; init; } //bu ne demek nesne örneği ürettiğin andan itibaren bunu değiştiremeyeceksin

    //    public ProductAddedEvent2(int Id)
    //    {
    //        this.Id = Id;
    //    }
    //}
}
