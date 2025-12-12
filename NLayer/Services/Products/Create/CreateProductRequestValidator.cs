using App.Repositories.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        //her request geldiğinde bundan bir nesne örneği üretiliyor yaşam döngüsü scop
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Ürün ismi gereklidir.")
                .NotEmpty().WithMessage("Ürün ismi gereklidir.")
                .Length(3, 10).WithMessage("Ürün ismi 3 ila 10 karakter arasında olmalıdır");
            //.MustAsync(MustUniqueProductNameAsync).WithMessage("Ürün ismi veritabanında bulunmaktadır.");
            //.Must(MustUniqueProductName).WithMessage("Ürün ismi veritabanında bulunmaktadır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Ürün kategori değeri fiyatı 0' dan büyük olmalıdır.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0' dan büyük olmalıdır.");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Stok adeti 1 ile 100 arasında olmalıdır.");
        }

        #region 3. yol async validation
        //private async Task<bool> MustUniqueProductNameAsync(string name, CancellationToken cancellationToken)
        //{
        //    return !await _productRepository.Where(x => x.Name == name).AnyAsync(cancellationToken);
        //}
        #endregion


        #region 1.yol sync validation
        //private bool MustUniqueProductName (string name) //senkron yazdık performansa direkt etki eder.
        //{
        //    return  !_productRepository.Where(x => x.Name == name).Any();
        //    // false => dönersem hata var 
        //}
        #endregion

    }
}
