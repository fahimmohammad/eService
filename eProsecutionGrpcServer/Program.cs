global using eProsecutionGrpcServer.Model;
global using eProsecutionGrpcServer.GrpcServices;
global using Microsoft.EntityFrameworkCore;
using eProsecutionGrpcServer.DAO;
using Microsoft.EntityFrameworkCore.Internal;
using FluentAssertions.Common;

using eProsecutionGrpcServer.Repository;
using Microsoft.Extensions.Hosting.WindowsServices;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService()
        ? AppContext.BaseDirectory
        : default
});
builder.Host.UseWindowsService();
builder.Services.AddDbContext<ServerDbContext>(options => {

    options.UseOracle(builder.Configuration.GetConnectionString("dbConnection"));

});

//builder.Services.AddScoped<ICustomer, eProsecutionGrpcServer.DbService.CustomerService>();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();
//builder.Services.AddGrpcReflection();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProsecutor,ProsecutorRepo>();
builder.Services.AddScoped<IDatalist, DatalistRepo>();
builder.Services.AddScoped<ICaseProfile, CaseProfileRepo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
//app.MapGrpcService<CustomerService>();
app.MapGrpcService<ProsecutorService>();
app.MapGrpcService<DatalistService>();
app.MapGrpcService<CaseService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();


