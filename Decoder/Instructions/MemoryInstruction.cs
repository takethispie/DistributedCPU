using Decoder.Interfaces;
using ISA.Data;
using ISA.Exceptions;
using OneOf;

namespace Decoder.Instructions;

internal class MemoryInstruction : IInstruction {
    public InstructionOperation InstructionOperation { get; }
    public OneOf<Constant, Register> Source { get; private set; }
    public OneOf<Constant, Register> Destination { get; private set; }

    public MemoryInstruction(InstructionOperation instructionOperation, Register source, Constant destination) {
        InstructionOperation = instructionOperation switch {
            InstructionOperation.Write => instructionOperation,
            _ => throw new IncorrectInstructionStructureException(),
        };
        Source = source;
        Destination = destination;
    }
    
    public MemoryInstruction(InstructionOperation instructionOperation, Register source, Register destination) {
        InstructionOperation = instructionOperation switch {
            InstructionOperation.Write or InstructionOperation.Read => instructionOperation,
            _ => throw new IncorrectInstructionStructureException(),
        };
        Source = source;
        Destination = destination;
    }
    
    public MemoryInstruction(InstructionOperation instructionOperation, Constant source, Register destination) {
        InstructionOperation = instructionOperation switch {
            InstructionOperation.Read => instructionOperation,
            _ => throw new IncorrectInstructionStructureException(),
        };
        Source = source;
        Destination = destination;
    }
}