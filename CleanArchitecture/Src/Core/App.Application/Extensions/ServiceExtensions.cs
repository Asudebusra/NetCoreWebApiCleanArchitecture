using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using App.Application.Features.Products;
using App.Application.Features.Categories;
using App.Application.Contracts.Caching;


namespace App.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true); //.net in otomatik hata dönmesine yarayan toolunu kapattık 

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddFluentValidationAutoValidation();//manuel validationını kapatmamız lazım
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

            //services.AddScoped<ICacheService, CacheService>();
            //bunu diyemem çünkü application CacheService i referans almıyor.CacheService applicationı referans alıyor


            //TODO: API KATMANINA TAŞINACAK
            //services.AddScoped(typeof(NotFoundFilter<,>)); ---> bunu kaldırdık API Program.cs de ekledik
            //services.AddExceptionHandler<CriticalExceptionHandler>();
            //services.AddExceptionHandler<GlobalExceptonHandler>();

            return services;
        }
    }
}
