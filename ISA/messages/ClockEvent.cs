namespace ISA.Messages;
using MassTransit;

public record ClockEvent(Guid CorrelationId) : CorrelatedBy<Guid>;