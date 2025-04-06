using MassTransit;

namespace ISA.Messages;

public record ToRegisterFile(Guid CorrelationId, int ReadA, int ReadB, int Write, bool WriteEnable) : CorrelatedBy<Guid>;