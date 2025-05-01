using Decoder.Instructions;
using Decoder.Interfaces;
using ISA.Data;

namespace Decoder.Services;

public class DecoderService {
    
    public IInstruction Decode(string instruction) => instruction switch {
        { Length: not 8 } => throw new ArgumentException("wrong instruction size"),
        ['O', '1', ..] => new AluInstruction(
        InstructionOperation.Load,
            new Register(Convert.ToInt32(instruction.Substring(2,2), 16)),
            new Constant(Convert.ToInt32(instruction.Substring(3,4), 16))
        ),
        _ => new NopeInstruction()
    };
}