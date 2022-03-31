using MassTransit;

namespace AurSystem.Framework.Configuration;

public static class CustomConfigurationExtensions
{
    /// <summary>
    /// UsingRabbitMq custom formatter
    /// </summary>
    /// <param name="configurator"></param>
    public static void ApplyCustomBusConfiguration(this IBusFactoryConfigurator configurator)
    {
        var entityNameFormatter = configurator.MessageTopology.EntityNameFormatter;
        configurator.MessageTopology.SetEntityNameFormatter(new CustomEntityNameFormatter(entityNameFormatter));
        
    }
    /// <summary>
    /// AddMassTransit custom formatter
    /// </summary>
    /// <param name="configurator"></param>
    public static void ApplyCustomMassTransitConfiguration(this IBusRegistrationConfigurator configurator)
    {
        configurator.SetEndpointNameFormatter(new CustomEndpointNameFormatter());
    }
}