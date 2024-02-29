using ERP.Backend.gRPC;
using ERP.Backend.gRPC.Services;
using ERP.Backend.Models;
using ERP.Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
// Register the reflection service
builder.Services.AddGrpcReflection();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (!Enum.TryParse(Environment.GetEnvironmentVariable("DATABASE_TYPE"), true, out DatabaseType databaseType))
    {
        databaseType = DatabaseType.SQLite;
        Console.WriteLine("Environment variable DATABASE_TYPE was either not found or has an invalid value");
        Console.WriteLine("\tFalling back to SQLite");
    }

    string? connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    if (string.IsNullOrEmpty(connectionString))
    {
        connectionString = builder.Configuration.GetConnectionString("ConnectionString");
    }

    Console.WriteLine($"DatabaseType is {databaseType}");

    if (databaseType == DatabaseType.PostgreSQL)
    {
        options.UseNpgsql(
            connectionString,
            x => x.MigrationsAssembly(typeof(ERP.Backend.PostgreSQL.Marker).Assembly.GetName().Name!));
    }
    else
    {
        options.UseSqlite(
            connectionString,
            x => x.MigrationsAssembly(typeof(ERP.Backend.SQLite.Marker).Assembly.GetName().Name!));

    }
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
builder.Services.AddScoped(typeof(IArticleService), typeof(ERP.Backend.Services.ArticleService));
builder.Services.AddScoped(typeof(IPriceService), typeof(ERP.Backend.Services.PriceService));




var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProtoArticleService>();
app.MapGrpcService<ProtoPriceService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

#if DEBUG
app.MapGrpcReflectionService();
#endif 

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // This line ensures the DB is created
    await dbContext.LoadDemoData();
}

app.Run();