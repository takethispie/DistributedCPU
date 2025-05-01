using Contracts;
using MassTransit;
using RegisterFile.Services;

namespace RegisterFile.Consumers;

public class ClockEventConsumer(
    DestinationBufferService destinationBufferService,
    RegisterService registerService
) : IConsumer<ClockEvent>
{
    public Task Consume(ConsumeContext<ClockEvent> context)
    {
        return Task.CompletedTask;
    }
}