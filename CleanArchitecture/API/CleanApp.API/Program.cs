using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Bus;
using App.Caching;
using App.Persistence.Extensions;
using CleanApp.API.ExceptionHandler;
using CleanApp.API.Extensions;
using CleanApp.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers(options => {
//    options.Filters.Add<FluentValidationFilter>();
//    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;//referans tipler için nullable deðerleri kontrol etmeyecek
//}); --> gerek yok çünkü extension yazdýk
builder.Services.AddControllersWithFiltersExt().AddSwaggerGenExt().AddExceptionHandlerExt().AddCachingExt();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGenExt(); ayrý ayrý yazmaya gerek yok çünkü extension yazdýk
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration).AddBusExt(builder.Configuration);
//builder.Services.AddScoped(typeof(NotFoundFilter<,>));

//builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
//builder.Services.AddExceptionHandler<GlobalExceptonHandler>();
//builder.Services.AddMemoryCache();
//builder.Services.AddSingleton<ICacheService, CacheService>();

//Use ile baþlayanlar middlewarelerdir
var app = builder.Build();
//app.UseExceptionHandler(x => { });
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwaggerExt();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
