using ToAToa.Application;
using ToAToa.DataAccess;
using ToAToa.Domain;
using ToAToa.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomain();
builder.Services.AddDataAccess();
builder.Services.AddApplication();
builder.Services.AddPresentation();

var app = builder.Build();

app.UsePresentation();

await app.RunAsync();
