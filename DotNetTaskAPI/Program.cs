using DotNetTaskAPI.Models;
using DotNetTaskAPI.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CapitalPlacementTaskDB")));


builder.Services.AddSingleton<ICosmosDbService>(serviceProvider =>
{
    var cosmosClient = new CosmosClient(builder.Configuration["CosmosDb:EndpointUri"], builder.Configuration["CosmosDb:PrimaryKey"]);
    return new CosmosDbService(cosmosClient, builder.Configuration["CosmosDb:DatabaseId"], builder.Configuration["CosmosDb:ContainerId"]);
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
