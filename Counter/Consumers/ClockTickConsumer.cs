using ISA.Messages;
using MassTransit;

namespace Counter.Consumers;

public class ClockTickConsumer : IConsumer<ClockFired>
{
    public Task Consume(ConsumeContext<ClockFired> context)
    {
        return Task.CompletedTask;
    }
}