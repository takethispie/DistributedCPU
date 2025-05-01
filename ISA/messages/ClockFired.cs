using MassTransit;

namespace ISA.Messages;

public record ClockFired(Guid CorrelationId) : CorrelatedBy<Guid>;