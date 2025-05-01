using App.Repositories;
using App.Repositories.Products;
using App.Services.ExceptionHandler;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    //public class ProductService (IGenericRepository<Product> productRepository)
    //{
    //    Task A()
    //    {
    //        var products = productRepository.GetAll().OrderByDescending(x => x.Price).Take(5); // --> repository e ait birkod 
    //    }
    //}
    public class ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork,IValidator<CreateProductRequest> createProductRequestValidator, IMapper mapper ):IProductService
    {
        //public Task<List<Product>> GetTopPriceProductAsync(int count)
        //{
        //    return productRepository.GetTopPriceProductAsync(count);
        //}//aynı method direkt api tarafındaki repository i kulanalım ne gerek var ? Diyelimki yarın dediler ki KDV li ürünleri gönder bana nasıl hesaplayacaksın ? controller tarafında yapmak zorunda kalacaksın Bussines Logicsiz uygulama olmaz Controllerlar hiç bir zaman repositorylerle çalışmaz

        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductAsync(count);

            //var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return new ServiceResult<List<ProductDto>>()
            {
                data = productAsDto
            };
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if(product is null)
            {
                return ServiceResult<ProductDto?>.Fail("Product is not found", HttpStatusCode.NotFound);
            }

            //manuel mapping
            //var productAsDto = new ProductDto(product!.Id,product.Name,product.Price,product.Stock);

            var productAsDto = mapper.Map<ProductDto>(product);


            //return new ServiceResult<Product>()
            //{
            //    data = product
            //};

            return ServiceResult<ProductDto>.Success(productAsDto)!;
        }


        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber,int pageSize)
        {
            // 1-10 => ilk 10 kayıt Skip(0).Take(10) 
            // 2-10 => 11 - 20 kayıt Skip(10).Take(10) 10 atla 10 tane al 
            // 3-10 => 21 - 30 kayıt Skip(20).Take(10)
            int skip = (pageNumber - 1) * pageSize;

            var products = await productRepository.GetAll().Skip(skip).Take(pageSize).ToListAsync();
            //manuel mapper
            //var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
            var productAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }


        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();

            //var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList(); manuel mapping 

            var productAsDto = mapper.Map<List<ProductDto>>(products);


            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            //throw new CriticalException("kritik seviye bir hata meydana geldi.");

            //throw new Exception("db hatası");


           // 2.yol mamuel service business check
            var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();

            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.NotFound);
            }
            //sağlıklı değil burada yazmak async oldu ama

            #region 3. yol manuel fluent validation business
            //var validationResult = await createProductRequestValidator.ValidateAsync(request);

            //if (!validationResult.IsValid)
            //{
            //    return ServiceResult<CreateProductResponse>.Fail(validationResult.Errors.Select(x=>x.ErrorMessage).ToList());
            //}
            #endregion


            #region Manuel Mapping
            //var product = new Product()
            //{
            //    Name = request.Name,
            //    Price = request.Price,
            //    Stock = request.Stock,
            //};
            #endregion

            var product = mapper.Map<Product>(request);


            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangeAsync();

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"api/products/{product.Id}");
        }
        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            //Fast Fail
            //önce olumsuz durumları dönmek demektir sonra olumlu durumları dönmek demektir.

            //Guard Clouses 
            //önce bir gardını al yani olumsuz durumlara karşı kendini koru ifle yaz 
            var product = await productRepository.GetByIdAsync(id);

            if(product is null)
            {
               return  ServiceResult.Fail("güncellenecek ürün bulunamadı.", HttpStatusCode.NotFound);
            }

            var isProductNameExist = await productRepository.Where(x => x.Name == request.Name && x.Id != product.Id).AnyAsync();

            if (isProductNameExist)
            {
                return ServiceResult.Fail("Ürün ismi veritabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }



            //product.Name = request.Name;
            //product.Price = request.Price;
            //product.Stock = request.Stock;

            product = mapper.Map(request, product);

            productRepository.Update(product);
            await unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);

            if (product is null)
            {
                return ServiceResult.Fail("Product is not found", HttpStatusCode.NotFound);
            }

            product.Stock = request.Quantity;
            productRepository.Update(product);
            await unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if(product is null)
            {
                return ServiceResult.Fail("Product is not found", HttpStatusCode.NotFound);
            }

            productRepository.Delete(product);
            await unitOfWork.SaveChangeAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);

        }
    }
}
