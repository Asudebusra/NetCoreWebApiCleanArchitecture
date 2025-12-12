using App.Domain.Entities.Common;

namespace App.Domain.Entities
{
    public class Category : BaseEntity<int>, IAuditEntity
    {
        public string Name { get; set; } = default!;

        public List<Product>? Products { get; set; }  //bir category nin illa bir productı olacak diye birşey yok 
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
