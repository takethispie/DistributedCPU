using Decoder.Instructions;
using Decoder.Interfaces;
using ISA.Data;
using ISA.Exceptions;
using ISA.Messages;

namespace Decoder.Services;

public class InstructionTransitionerService {
    public ToRegisterFile FromAluToRegisterFile(IInstruction instruction, Guid correlationId) => instruction switch
    {
        AluInstruction { OperandB.IsT1: true } toAlu => new ToRegisterFile(
            correlationId,
            toAlu.InstructionOperation,
            new Register(0),
            new Register(0),
            toAlu.Destination,
            toAlu.OperandB.AsT1
        ),
        AluInstruction { OperandB.IsT0: true } toAlu => new ToRegisterFile(
            correlationId,
            toAlu.InstructionOperation,
            toAlu.OperandA,
            toAlu.OperandB.AsT0,
            toAlu.Destination,
            new Constant(0)
        ),
            
        _ => throw new IncorrectInstructionStructureException()
    };
}