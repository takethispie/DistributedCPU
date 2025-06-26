using Decoder.Extensions;
using Decoder.Instructions;
using Decoder.Interfaces;
using ISA.Data;

namespace Decoder.Services;

public class DecoderService {

    private InstructionOperation GetInstructionOperation(char instruction) => instruction switch {
        '0' => InstructionOperation.Add,
        '1' => InstructionOperation.Sub,
        '2' => InstructionOperation.Div,
        '3' => InstructionOperation.Mul,
        '4' => InstructionOperation.LLogShift,
        '5' => InstructionOperation.RLogShift,
        _ => throw new ArgumentOutOfRangeException(nameof(instruction), instruction, null)
    };
    
    public IInstruction Decode(ReadOnlySpan<char> instruction) => instruction switch {
        { Length: not 8 } => throw new ArgumentException("wrong instruction size"),
        ['0', '1', var dest, _, ..{ Length: 4 } hexConst] => new AluInstruction(
        InstructionOperation.Load,
            new Register(dest.FromHex()),
            new Constant(hexConst.FromHex())
        ),
        ['A', var index, var dest, var opA, var opB, ..{ Length: 3 }] => new AluInstruction(
            GetInstructionOperation(index),
            new Register(dest.FromHex()),
            new Register(opA.FromHex()),
            new Register(opB.FromHex())
        ),
        _ => new NopeInstruction()
    };
}