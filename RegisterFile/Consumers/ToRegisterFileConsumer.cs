using ISA.Messages;
using MassTransit;
using RegisterFile.Services;
using Serilog;

namespace RegisterFile.Consumers;

public class ToRegisterFileConsumer(LoadingBufferService loadingBufferService) : IConsumer<ToRegisterFile>
{
    public Task Consume(ConsumeContext<ToRegisterFile> context)
    {
        var msg = context.Message;
        Log.Information("Register File Received Instruction");
        loadingBufferService.Push(msg);
        return Task.CompletedTask;
    }
}