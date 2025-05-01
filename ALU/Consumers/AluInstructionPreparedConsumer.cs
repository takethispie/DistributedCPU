using ALU.Services;
using ISA.Messages;
using MassTransit;

namespace ALU.Consumers;

public class AluInstructionPreparedConsumer : IConsumer<AluInstructionPrepared>
{
    public Task Consume(ConsumeContext<AluInstructionPrepared> context)
    {
        return Task.CompletedTask;
    }
}