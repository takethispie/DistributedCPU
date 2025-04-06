using ISA.Messages;
using MassTransit;
using Memory.Services;

namespace Memory.Consumers;

public class ClockEventConsumer(MemoryService memoryService) : IConsumer<ClockEvent>
{
    public Task Consume(ConsumeContext<ClockEvent> context)
    {
        return Task.CompletedTask;
    }
}