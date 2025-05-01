using ISA.Data;
using MassTransit;

namespace ISA.Messages;

public record AluInstructionPrepared(
    Guid CorrelationId, 
    InstructionOperation Op, 
    Register OperandA, 
    Register OperandB,
    Constant Constant,
    Register Dest
) : CorrelatedBy<Guid>;