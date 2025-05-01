using MassTransit;

namespace RegisterFile.Consumers;

public class ToRegisterFileConsumerDefinition : ConsumerDefinition<ToRegisterFileConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ToRegisterFileConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        endpointConfigurator.UseInMemoryOutbox(context);
    }
}