using Microsoft.EntityFrameworkCore;
using NTierApplication.DataAccess;
using NTierApplication.Repository;
using NTierApplication.Service;
using NTierApplication.Web.ActionHelpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/// salom dunyo

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<MainContext>();

builder.Services.AddDbContext<MainContext>(options => {
    options.UseSqlServer("Data Source=localhost;User ID=sa;Password=akobirakoone;Initial Catalog=NTierApplication;TrustServerCertificate=True;");
});


var app = builder.Build();

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
