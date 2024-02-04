using ERP.Backend.gRPC;
using ERP.Backend.gRPC.Services;
using ERP.Backend.Models;
using ERP.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
// Register the reflection service
builder.Services.AddGrpcReflection();


builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
builder.Services.AddScoped(typeof(IArticleService), typeof(ERP.Backend.Services.ArticleService));
builder.Services.AddScoped(typeof(IPriceService), typeof(ERP.Backend.Services.PriceService));




var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProtoArticleService>();
app.MapGrpcService<ProtoPriceService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();