using ISA.Data;
using ISA.Messages;
using MassTransit;
using RegisterFile.Services;
using Serilog;

namespace RegisterFile.Consumers;

public class ClockEventConsumer(
    DestinationBufferService destinationBufferService, 
    RegisterService registerService,
    LoadingBufferService loadingBufferService
) : IConsumer<ClockFired>
{
    public Task Consume(ConsumeContext<ClockFired> context) {
        var (dest, val) = destinationBufferService.Pull();
        Log.Information(@$" value {val.Value} to be stored in ${dest.Index}");
        registerService.Set(dest, val);
        destinationBufferService.Clear();
        var inst = loadingBufferService.Pull();
        var regA = registerService.Get(inst.ReadA);
        var regB = registerService.Get(inst.ReadB);
        var toAlu = inst.Op switch {
            InstructionOperation.Load => 
                new AluInstructionPrepared(
                    Guid.NewGuid(), 
                    inst.Op, 
                    inst.Data, 
                    new Constant(0),  
                    inst.Dest
                ),
            _ => new AluInstructionPrepared(Guid.NewGuid(), inst.Op, regA, regB,  inst.Dest)
        };
        loadingBufferService.Clear();
        context.Publish(toAlu);
        return Task.CompletedTask;
    }
}