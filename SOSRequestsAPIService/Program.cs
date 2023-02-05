using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using SOSRequestsAPIService.Models;
using SOSRequestsAPIService.Repositories;
using SOSRequestsAPIService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
builder.Services.AddDbContext<SOSDbContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
    options.UseMySql(builder.Configuration.GetConnectionString("SOSdb"), serverVersion, options => options.EnableRetryOnFailure());
});

builder.Services.AddTransient<ISOSRequestService, SOSRequestService>();
builder.Services.AddTransient<ISOSRequestRepository, SOSRequestRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

