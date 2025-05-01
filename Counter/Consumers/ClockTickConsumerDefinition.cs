using MassTransit;

namespace Counter.Consumers;

public class ClockTickConsumerDefinition : ConsumerDefinition<ClockTickConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ClockTickConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        endpointConfigurator.UseInMemoryOutbox(context);
    }
}