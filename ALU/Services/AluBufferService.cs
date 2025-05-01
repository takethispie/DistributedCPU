using ISA.Interfaces;
using ISA.Messages;

namespace ALU.Services;

public class AluBufferService : IBufferService<AluInstructionPrepared> {
    
    private AluInstructionPrepared _aluInstructionPrepared;
    
    public void Push(AluInstructionPrepared value) => _aluInstructionPrepared = value;
    
    public AluInstructionPrepared Pull() => _aluInstructionPrepared;
}