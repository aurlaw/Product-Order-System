using System.Reflection;
using AurSystem.Framework;
using AurSystem.Framework.Models.Options;
using AurSystem.Framework.Services;
using MassTransit;
using AurSystem.Framework.Configuration;
using Marten;
using Microsoft.OpenApi.Models;
using Weasel.Core;


var builder = WebApplication.CreateBuilder(args);

// configure builder
builder.Services.AddLogging(x => x.AddConsole());

// Marten config
builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("Postgres"));

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
    
});

// mass transit
//TODO

// swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory Service API", Version = "v1" });    
});

// API endpoints
builder.Services.AddEndpointDefinitions(typeof(Program));

// configure app
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Service API V1");
});
app.UseEndpointDefinitions();

app.Run();

/*
 *    // This is the absolute, simplest way to integrate Marten into your
    // .Net Core application with Marten's default configuration
    services.AddMarten(options =>
    {
        // Establish the connection string to your Marten database
        options.Connection(Configuration.GetConnectionString("Marten"));

        // If we're running in development mode, let Marten just take care
        // of all necessary schema building and patching behind the scenes
        if (Environment.IsDevelopment())
        {
            options.AutoCreateSchemaObjects = AutoCreate.All;
        }
    });
 * 
 */