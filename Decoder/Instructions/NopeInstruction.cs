using Decoder.Interfaces;
using ISA.Data;

namespace Decoder.Instructions;

public sealed class NopeInstruction : IInstruction {
    public InstructionOperation InstructionOperation { get; } = InstructionOperation.Nope;
}