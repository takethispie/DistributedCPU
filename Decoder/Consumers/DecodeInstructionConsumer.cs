using Decoder.Instructions;
using Decoder.Services;
using ISA.Data;
using ISA.Messages;
using MassTransit;
using Serilog;

namespace Decoder.Consumers;

public class DecodeInstructionConsumer(DecoderService decoderService) : IConsumer<InstructionLoaded>
{
    public async Task Consume(ConsumeContext<InstructionLoaded> context)
    {
        Log.Information("");
        var inst = decoderService.Decode(context.Message.Instruction) switch
        {
            AluInstruction { OperandB.IsT1: true } toAlu => new ToRegisterFile(
                context.Message.CorrelationId,
                toAlu.InstructionOperation,
                new Register(0),
                new Register(0),
                toAlu.Destination,
                toAlu.OperandB.AsT1
            ),
            AluInstruction { OperandB.IsT0: true } toAlu => new ToRegisterFile(
                context.Message.CorrelationId,
                toAlu.InstructionOperation,
                toAlu.OperandA,
                toAlu.OperandB.AsT0,
                toAlu.Destination,
                new Constant(0)
            ),
            _ => throw new ArgumentOutOfRangeException()
        };
        Log.Information("Instruction decoded");
        await context.Publish(inst);
    }
}