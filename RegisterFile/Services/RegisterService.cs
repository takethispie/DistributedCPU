using ISA.Data;

namespace RegisterFile.Services;

public class RegisterService {
    private readonly Constant[] _registers;

    public RegisterService(int width = 16) {
        _registers = new Constant[width];
        _registers[0] = new Constant(0);
    }

    public Constant Get(Register register) => _registers[register.Index];
    
    public void Set(Register register, Constant value) 
        => _registers[register.Index] = register.Index > 0 ? value : new Constant(0);
}