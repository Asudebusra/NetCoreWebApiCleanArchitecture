using App.Repositories;
using App.Repositories.Extensions;
using App.Services;
using App.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => {
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;//referans tipler için nullable deðerleri kontrol etmeyecek
  });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

//Repository katmanýna ait bi kod burda kalabalýða neden olacak Repository katamanýnda extension method haline getirmem lazým 
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    var connectionString = builder.Configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
//    //tip güvenli bir þekilde 
//    //options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")); //eski hallerde böyle olmalýydý tip güvenli olmasaydý

//    options.UseSqlServer(connectionString!.SqlServer); // datanýn null olmayacaðý anlamýna geliyor
//});


var app = builder.Build();

app.UseExceptionHandler(x=> { });//middlewarein dolmasý lazým 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
