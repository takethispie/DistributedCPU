using MassTransit;

namespace ISA.Messages;

public record ToDecoder(Guid CorrelationId, string Instruction) : CorrelatedBy<Guid>;