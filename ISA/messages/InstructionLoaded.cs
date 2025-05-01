using MassTransit;

namespace ISA.Messages;

public record InstructionLoaded(Guid CorrelationId, string Instruction) : CorrelatedBy<Guid>;