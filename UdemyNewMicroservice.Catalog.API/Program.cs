using UdemyNewMicroservice.Catalog.API;
using UdemyNewMicroservice.Catalog.API.Features.Categories;
using UdemyNewMicroservice.Catalog.API.Options;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExtension();
builder.Services.AddDatabaseServiceExtension();
builder.Services.AddCommonServices(typeof(CatalogAssambly));


var app = builder.Build();

app.AddCategoryGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

