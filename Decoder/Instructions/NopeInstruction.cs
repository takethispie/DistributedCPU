using Decoder.Interfaces;
using ISA.Data;

namespace Decoder.Instructions;

internal class NopeInstruction : IInstruction {
    public InstructionOperation InstructionOperation { get; } = InstructionOperation.Nope;
}