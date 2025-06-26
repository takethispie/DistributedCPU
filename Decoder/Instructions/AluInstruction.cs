using Decoder.Interfaces;
using ISA.Data;
using ISA.Exceptions;
using OneOf;

namespace Decoder.Instructions;

public sealed class AluInstruction : IInstruction {
    public InstructionOperation InstructionOperation { get; }
    public Register OperandA { get; }
    public OneOf<Register, Constant> OperandB { get; }
    public Register Destination { get; }

    public AluInstruction(InstructionOperation instructionOperation, Register destination, Register operandA, Register operandB) {
        InstructionOperation = instructionOperation switch {
            InstructionOperation.Add 
                or InstructionOperation.Div 
                or InstructionOperation.Mul 
                or InstructionOperation.Sub
                or InstructionOperation.Nope => instructionOperation,
            _ => throw new IncorrectInstructionStructureException()
        };
        OperandA = operandA;
        OperandB = operandB;
        Destination = destination;
    }
    
    public AluInstruction(InstructionOperation instructionOperation, Register destination, Constant constant) {
        InstructionOperation = instructionOperation switch {
            InstructionOperation.Load 
                or InstructionOperation.LUpper 
                or InstructionOperation.Out 
                or InstructionOperation.Nope => instructionOperation,
            _ => throw new IncorrectInstructionStructureException()
        };
        OperandA = new Register(0);
        OperandB = constant;
        Destination = destination;
    }
}