using MassTransit;

namespace ALU.Consumers;

public class AluInstructionPreparedConsumerDefinition : ConsumerDefinition<AluInstructionPreparedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<AluInstructionPreparedConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        endpointConfigurator.UseInMemoryOutbox(context);
    }
}