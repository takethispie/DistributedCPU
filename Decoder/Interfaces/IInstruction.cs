using ISA.Data;

namespace Decoder.Interfaces;

public interface IInstruction {
    InstructionOperation InstructionOperation { get; }
}