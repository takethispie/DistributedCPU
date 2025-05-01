using Decoder.Instructions;
using Decoder.Services;
using ISA.Messages;
using MassTransit;

namespace Decoder.Consumers;

public class DecodeInstructionConsumer(DecoderService decoderService) : IConsumer<InstructionLoaded>
{
    public async Task Consume(ConsumeContext<InstructionLoaded> context) =>
        await context.Publish(decoderService.Decode(context.Message.Instruction) switch {
            AluInstruction { OperandB.IsT1: true } toAlu => new AluInstruction(
                toAlu.InstructionOperation,
                toAlu.Destination,
                toAlu.OperandB.AsT1
            ),
            AluInstruction { OperandB.IsT0: true } toAlu => new AluInstruction(
                toAlu.InstructionOperation,
                toAlu.Destination,
                toAlu.OperandA,
                toAlu.OperandB.AsT0
            ),
            _ => throw new ArgumentOutOfRangeException()
        });
}