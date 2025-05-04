using ALU.Services;
using ISA.Data;
using ISA.Messages;
using MassTransit;

namespace ALU.Consumers;

public class AluInstructionPreparedConsumer : IConsumer<AluInstructionPrepared>
{
    public Task Consume(ConsumeContext<AluInstructionPrepared> context)
    {
        var message = context.Message;
        var result = Execute(message.Op, message.OperandA, message.OperandB, message.Dest);
        return context.Publish(result);
    }

    private AluExecuted Execute(InstructionOperation op, Constant operandA, Constant operandB, Register dest) =>
        op switch {
            InstructionOperation.Add => new AluExecuted(Guid.NewGuid(), new Constant(operandA.Value + operandB.Value), dest),
            InstructionOperation.Sub => new AluExecuted(Guid.NewGuid(), new Constant(operandA.Value - operandB.Value), dest),
            InstructionOperation.Div  => new AluExecuted(Guid.NewGuid(), new Constant(operandA.Value / operandB.Value), dest),
            InstructionOperation.Mul => new AluExecuted(Guid.NewGuid(), new Constant(operandA.Value * operandB.Value), dest),
            _ => new AluExecuted(Guid.NewGuid(), new Constant(0), new Register(0)),
        };
}