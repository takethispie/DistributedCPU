using MassTransit;
using ISA.Data;

namespace ISA.Messages;

public record ToMemory(Guid CorrelationId, int Adress, int Value) : CorrelatedBy<Guid>;
