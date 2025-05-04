using ISA.Data;
using MassTransit;

namespace ISA.Messages;

public record ToRegisterFile(
    Guid CorrelationId,
    InstructionOperation Op,
    Register ReadA, 
    Register ReadB, 
    Register Dest,
    Constant Data
) : CorrelatedBy<Guid>;