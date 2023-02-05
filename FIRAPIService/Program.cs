using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using FIRAPIService.Models;
using FIRAPIService.Services;
using FIRAPIService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

/** KeyValut Confug

var defaultCredentials = new DefaultAzureCredential();
var keyVaultEndpoint = builder.Configuration["AzureKeyVaultEndpoint"];

//builder.AddAzureKeyVault(new Uri(keyVaultEndpoint));
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), defaultCredentials,
    new AzureKeyVaultConfigurationOptions
    {
        // Manager = new PrefixKeyVaultSecretManager(secretPrefix),
        ReloadInterval = TimeSpan.FromMinutes(5)
    });

*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// DB Config
builder.Services.AddDbContext<SOSDbContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
    options.UseMySql(builder.Configuration.GetConnectionString("SOSdb"), serverVersion, options => options.EnableRetryOnFailure());
});

builder.Services.AddTransient<IFIRRepository, FIRRepository>();
builder.Services.AddTransient<IFIRService, FIRService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

