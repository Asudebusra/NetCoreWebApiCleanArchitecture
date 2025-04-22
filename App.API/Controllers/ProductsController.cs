using App.Services;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    //[Route("api/[controller]")] eğer /[action] yazarsan işte Post datayı kaydederken, datayı güncellerken PUT bir daha bunları yanında yazmasın 
    //[ApiController]
    public class ProductsController(IProductService productService) : CustomBaseController
    {

        //base path aynı hangisi çalışcak bilemiyor hangi get metodu
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResult = await productService.GetAllListAsync();

            return CreateActionResult(serviceResult);

            //if(productResult.IsSuccess)
            //{
            //    return Ok(productResult.data);
            //}
            //else
            //{
            //    return BadRequest(productResult.ErrorMessage);
            //} // ee her durumda gidip her şeyin altına if -else mi yazıcaz 
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));



        // http://localhost:5000/api/products/id=2 --> aşağıdaki method çalışır
        [HttpGet("{id:int}")] 
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResult = await productService.GetByIdAsync(id);

            return CreateActionResult(serviceResult);
            //var result = new ObjectResult(productResult) { StatusCode = (int)productResult.Status };

            //if(productResult.Status == HttpStatusCode.NoContent) // reponse un body sinde bişey göndermicez
            //{
            //    return new ObjectResult(null) {  StatusCode = productResult.Status.GetHashCode() };
            //}

            //var result = new ObjectResult(productResult) { StatusCode = productResult.Status.GetHashCode() };

            //return result;

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var serviceResult = await productService.CreateAsync(request);

            return CreateActionResult(serviceResult);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,UpdateProductRequest request)
        {
            var serviceResult = await productService.UpdateAsync(id,request);

            return CreateActionResult(serviceResult);
        }

        //[HttpPut("UpdateStock")]
        //public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request)
        //{
        //    var serviceResult = await productService.UpdateStockAsync(request);

        //    return CreateActionResult(serviceResult);
        //}

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request)
        {
            var serviceResult = await productService.UpdateStockAsync(request);

            return CreateActionResult(serviceResult);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await productService.DeleteAsync(id);

            return CreateActionResult(serviceResult);
        }
    }
}
