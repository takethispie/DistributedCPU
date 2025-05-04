using ISA.Data;
using MassTransit;

namespace ISA.Messages;

public record AluInstructionPrepared(
    Guid CorrelationId, 
    InstructionOperation Op, 
    Constant OperandA, 
    Constant OperandB,
    Register Dest
) : CorrelatedBy<Guid>;