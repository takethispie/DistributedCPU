using Decoder.Instructions;
using Decoder.Services;
using ISA.Data;
using ISA.Messages;
using MassTransit;
using Serilog;

namespace Decoder.Consumers;

public class DecodeInstructionConsumer(
    DecoderService decoderService, 
    InstructionTransitionerService instructionTransitionerService
) : IConsumer<InstructionLoaded>
{
    public async Task Consume(ConsumeContext<InstructionLoaded> context)
    {
        Log.Information($"starting decoding of {context.Message.Instruction}");
        var aluInst = decoderService.Decode(context.Message.Instruction.AsSpan());
        var inst = instructionTransitionerService.FromAluToRegisterFile(aluInst, context.Message.CorrelationId);
        Log.Information("Instruction decoded");
        await context.Publish(inst);
    }
}