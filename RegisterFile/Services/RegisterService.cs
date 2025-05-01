using ISA.Data;

namespace RegisterFile.Services;

public class RegisterService {
    private Constant[] _registers;

    public RegisterService(int width) {
        _registers = new Constant[width];
    }

    public Constant Get(Register register) => _registers[register.Index];
    
    public void Set(Register register, Constant value) => _registers[register.Index] = value;
}