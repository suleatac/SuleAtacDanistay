using App.API.CacheItems;
using App.Bus.Extentions;
using App.Repository;
using App.Repository.Extentions;
using Microsoft.CodeAnalysis.Host.Mef;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddCommonBusExtentions(builder.Configuration);
// Redis Bağlantı ayarları
builder.Services.AddStackExchangeRedisCache(options => {
    var connectiontostring = builder.Configuration.GetSection(RedisConnectionTostringOptions.Key).Get<RedisConnectionTostringOptions>();
    options.Configuration = connectiontostring!.Redis;
});
// Add services to the container.
builder.Services.AddRepositoryExtentions(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
//çalıştığında otomatik migration yapması için
//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
//    await dbContext.Database.MigrateAsync();
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
