using AurSystem.Framework;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// configure builder
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer Service API", Version = "v1" });    
});

builder.Services.AddEndpointDefinitions(typeof(Program));


// configure app
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Service API V1");
});

app.UseEndpointDefinitions();

//app.MapGet("/", () => "Hello World!");

app.Run();