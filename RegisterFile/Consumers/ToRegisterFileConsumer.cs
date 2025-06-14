using ISA.Data;
using ISA.Messages;
using MassTransit;
using RegisterFile.Services;
using Serilog;

namespace RegisterFile.Consumers;

public class ToRegisterFileConsumer(RegisterService registerService) : IConsumer<ToRegisterFile>
{
    public Task Consume(ConsumeContext<ToRegisterFile> context)
    {
        var inst = context.Message;
        Log.Information("Register File Received Instruction");
        var regA = registerService.Get(inst.ReadA);
        var regB = registerService.Get(inst.ReadB);
        var toAlu = inst.Op switch {
            InstructionOperation.Load => new AluInstructionPrepared(
                Guid.NewGuid(), 
                inst.Op, 
                new Constant(0),
                inst.Data,
                inst.Dest
            ),
            _ => new AluInstructionPrepared(
                Guid.NewGuid(), 
                inst.Op, 
                registerService.Get(inst.ReadA), 
                registerService.Get(inst.ReadB),  
                inst.Dest
            )
        };
        return context.Publish(toAlu);
        return Task.CompletedTask;
    }
}