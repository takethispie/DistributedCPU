using ISA.Messages;
using MassTransit;
using RegisterFile.Services;

namespace RegisterFile.Consumers;

public class ToRegisterFileConsumer(
    DestinationBufferService destinationBufferService
) : IConsumer<ToRegisterFile>
{
    public Task Consume(ConsumeContext<ToRegisterFile> context)
    {
        destinationBufferService.Push((context.Message.Dest, context.Message.WriteEnable));
        return Task.CompletedTask;
    }
}