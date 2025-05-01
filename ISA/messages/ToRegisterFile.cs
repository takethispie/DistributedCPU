using ISA.Data;
using MassTransit;

namespace ISA.Messages;

public record ToRegisterFile(
    Guid CorrelationId, 
    Register ReadA, 
    Register ReadB, 
    Register Dest, 
    bool WriteEnable,
    Constant Data
) : CorrelatedBy<Guid>;