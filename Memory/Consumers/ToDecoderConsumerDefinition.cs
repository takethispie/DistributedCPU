using MassTransit;

namespace Memory.Consumers
{
    public class ToDecoderConsumerDefinition :
        ConsumerDefinition<ToDecoderConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ToDecoderConsumer> consumerConfigurator, IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
            endpointConfigurator.UseInMemoryOutbox(context);
        }
    }
}