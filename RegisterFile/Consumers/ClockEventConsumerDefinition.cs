using MassTransit;

namespace RegisterFile.Consumers;

public class ClockEventConsumerDefinition : ConsumerDefinition<ClockEventConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ClockEventConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        endpointConfigurator.UseInMemoryOutbox(context);
    }
}