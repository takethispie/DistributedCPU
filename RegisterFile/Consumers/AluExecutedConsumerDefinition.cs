using MassTransit;

namespace RegisterFile.Consumers;

public class AluExecutedConsumerDefinition : ConsumerDefinition<AluExecutedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<AluExecutedConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        endpointConfigurator.UseInMemoryOutbox(context);
    }
}