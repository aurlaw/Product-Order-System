using AurSystem.Framework;
using AurSystem.Framework.Models.Options;
using AurSystem.Framework.Services;
using AurSystem.Framework.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// configure builder
builder.Services.AddLogging(x => x.AddConsole());
builder.Services.Configure<SupabaseConfig>(builder.Configuration.GetSection("Supabase"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<SupabaseClient>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Service API", Version = "v1" });    
});

builder.Services.AddEndpointDefinitions(typeof(Program));

// configure app
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service API V1");
});
app.UseEndpointDefinitions();

app.Run();


/*
            //services.AddGenericRequestClient();
            services.AddMassTransit(x =>
            {
                    x.ApplyCustomMassTransitConfiguration();

                    x.AddDelayedMessageScheduler();

                x.AddConsumers(Assembly.GetExecutingAssembly());
                //x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                        cfg.AutoStart = true;
                        cfg.ApplyCustomBusConfiguration();
                        cfg.UseDelayedMessageScheduler();
                        cfg.ConfigureEndpoints(context);                    
                });
            })
            .AddMassTransitHostedService(); 
     
 */