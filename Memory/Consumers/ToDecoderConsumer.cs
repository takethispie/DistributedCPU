using ISA.Messages;
using MassTransit;

namespace Memory.Consumers;

public class ToDecoderConsumer : IConsumer<InstructionLoaded>
{
    public Task Consume(ConsumeContext<InstructionLoaded> context)
    {
        return Task.CompletedTask;
    }
}