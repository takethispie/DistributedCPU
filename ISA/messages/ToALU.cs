using ISA.Data;
using MassTransit;

namespace ISA.Messages;

public record ToAlu(Guid CorrelationId, OperationTypes op, int OperandA, int OperandB) : CorrelatedBy<Guid>;