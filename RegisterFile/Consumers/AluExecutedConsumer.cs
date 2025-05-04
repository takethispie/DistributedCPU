using ISA.Messages;
using MassTransit;
using RegisterFile.Services;

namespace RegisterFile.Consumers;
public class AluExecutedConsumer(DestinationBufferService destinationBufferService) : IConsumer<AluExecuted>
{
    public Task Consume(ConsumeContext<AluExecuted> context)
    {
        var msg = context.Message;
        destinationBufferService.Push((msg.Dest, msg.Result));
        return Task.CompletedTask;
    }
}