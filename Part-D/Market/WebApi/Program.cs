using BLL.IServices;
using BLL.Services;
using Dal;
using Dal.IRepositories;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MarketContext>(options => {
  options.UseSqlServer("Server=.;Database=Trips;TrustServerCertificate=True;Trusted_Connection=True;");
})
;

builder.Services.AddScoped(typeof(IOrderDal), typeof(OrderDal));
builder.Services.AddScoped(typeof(IOrderBll), typeof(OrderBll));


builder.Services.AddScoped(typeof(IStateDal), typeof(StateDal));
builder.Services.AddScoped(typeof(IStateBll), typeof(StateBll));

builder.Services.AddScoped(typeof(ISupplierDal), typeof(SupplierDal));
builder.Services.AddScoped(typeof(ISupplierBll), typeof(SupplierBll));

builder.Services.AddScoped(typeof(IStockDal), typeof(StockDal));
builder.Services.AddScoped(typeof(IStockBll), typeof(StockBll));


builder.Services.AddCors(opotion => opotion.AddPolicy("all",
                p => p.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

var app = builder.Build();
app.UseCors("all");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
