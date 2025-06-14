using ISA.Messages;
using MassTransit;
using RegisterFile.Services;
using Serilog;

namespace RegisterFile.Consumers;
public class AluExecutedConsumer(RegisterService registerService) : IConsumer<AluExecuted>
{
    public Task Consume(ConsumeContext<AluExecuted> context)
    {
        var msg = context.Message;
        Log.Information($"Storing result from ALU to ${msg.Dest.Index}");
        registerService.Set(msg.Dest, msg.Result);
        return Task.CompletedTask;
    }
}