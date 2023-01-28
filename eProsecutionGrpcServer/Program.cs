global using eProsecutionGrpcServer.Model;
global using eProsecutionGrpcServer.GrpcServices;
global using Microsoft.EntityFrameworkCore;
using eProsecutionGrpcServer.DAO;
using Microsoft.EntityFrameworkCore.Internal;
using FluentAssertions.Common;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ServerDbContext>(options => {

    options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection"));

});
builder.Services.AddScoped<ICustomer, eProsecutionGrpcServer.DbService.CustomerService>();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<CustomerService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();


