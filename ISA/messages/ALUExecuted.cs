using ISA.Data;
using MassTransit;

namespace ISA.Messages;

public record AluExecuted(Guid CorrelationId, Constant Result, Register Dest) : CorrelatedBy<Guid>;
