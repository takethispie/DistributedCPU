using ISA.Messages;
using MassTransit;
using Memory.Services;

namespace Memory.Consumers;

public class ClockEventConsumer(MemoryService memoryService) : IConsumer<ClockFired>
{
    public Task Consume(ConsumeContext<ClockFired> context)
    {
        return Task.CompletedTask;
    }
}