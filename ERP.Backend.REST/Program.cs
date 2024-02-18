using ERP.Backend.Models;
using ERP.Backend.Services;
using Microsoft.EntityFrameworkCore;
using static ERP.Backend.REST.Provider;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var config = builder.Configuration;
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

    var provider = config.GetValue("provider", Sqlite.Name);
    
    if (databaseType == DatabaseType.PostgreSQL)
    {
        options.UseNpgsql(
            config.GetConnectionString(Postgres.Name)!,
            x => x.MigrationsAssembly(Postgres.Assembly)
        );
  
    }
    else
    {
        options.UseSqlite(
            config.GetConnectionString(Sqlite.Name)!,
            x => x.MigrationsAssembly(Sqlite.Assembly)
        );

    }
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
builder.Services.AddScoped(typeof(IArticleService), typeof(ERP.Backend.Services.ArticleService));
builder.Services.AddScoped(typeof(IPriceService), typeof(ERP.Backend.Services.PriceService));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#if DEBUG
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "";
});
#endif




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // This line ensures the DB is created
    await dbContext.LoadDemoData();
}

app.Run();


