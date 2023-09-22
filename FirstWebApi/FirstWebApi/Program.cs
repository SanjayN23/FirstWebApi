using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FirstWebApi.Models;
using ASPWebApi.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NWindConnection")));
builder.Services.AddScoped<RepositoryEmployee>();
builder.Services.AddControllersWithViews().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; });
builder.Services.AddControllersWithViews().AddJsonOptions(options => { options.JsonSerializerOptions.MaxDepth = 32; });



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();



app.MapControllers();



app.Run();