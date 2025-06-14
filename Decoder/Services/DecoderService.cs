using Decoder.Instructions;
using Decoder.Interfaces;
using ISA.Data;

namespace Decoder.Services;

public class DecoderService {
    
    public IInstruction Decode(ReadOnlySpan<char> instruction) => instruction switch {
        { Length: not 8 } => throw new ArgumentException("wrong instruction size"),
        ['0', '1', ..] => new AluInstruction(
        InstructionOperation.Load,
            new Register(Convert.ToInt32(instruction.ToString().Substring(2,1), 16)),
            new Constant(Convert.ToInt32(instruction.ToString().Substring(4,4), 16))
        ),
        _ => new NopeInstruction()
    };
}