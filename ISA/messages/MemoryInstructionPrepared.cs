using MassTransit;
using ISA.Data;

namespace ISA.Messages;

public record MemoryInstructionPrepared(Guid CorrelationId, int Address, int Value) : CorrelatedBy<Guid>;
