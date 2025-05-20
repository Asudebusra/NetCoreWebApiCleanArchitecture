using App.Repositories.Products;

namespace App.Repositories.Categories
{
    public class Category:BaseEntity<int>, IAuditEntity
    {
        public string Name { get; set; } = default!;

        public List<Product>? Products { get; set; }  //bir category nin illa bir productı olacak diye birşey yok 
        public DateTime Created { get ; set;}
        public DateTime? Updated { get; set; }
    }
}
