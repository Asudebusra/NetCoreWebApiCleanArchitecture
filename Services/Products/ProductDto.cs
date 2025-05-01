using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public record ProductDto (int Id,string Name,decimal Price,int Stock,int CategoryId);

    //public class ProductDto
    //{
    //    //Recordlar kullanılacak sebebi nedir
    //    // product1 == product2 dediğimde ikisinde aynı değer olsa bile farklı referanslara sahipse 
    //    //bu bize false döner
    //    //Recordlar işte bunları propertylerine göre karşılaştırır.
     //Recordlar classların wraplanmış yada özelleşmiş tipleri gibi düşünebiliriz


    //    //setlendiği zaman veri üerinde değişikliklere gidebiliyorduk ama init dediğimizde olmayacak
    //    public int Id { get; init; }
    //    public string Name { get; init; }
    //    public decimal Price { get; init; }
    //    public int Stock { get; init; }
    //}
}
