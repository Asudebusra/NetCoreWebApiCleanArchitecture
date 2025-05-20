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
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;//referans tipler i�in nullable de�erleri kontrol etmeyecek
  });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

//Repository katman�na ait bi kod burda kalabal��a neden olacak Repository kataman�nda extension method haline getirmem laz�m 
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    var connectionString = builder.Configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
//    //tip g�venli bir �ekilde 
//    //options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")); //eski hallerde b�yle olmal�yd� tip g�venli olmasayd�

//    options.UseSqlServer(connectionString!.SqlServer); // datan�n null olmayaca�� anlam�na geliyor
//});


var app = builder.Build();

app.UseExceptionHandler(x=> { });//middlewarein dolmas� laz�m 


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
