using ISA.Data;
using ISA.Interfaces;

namespace RegisterFile.Services;

public class DestinationBufferService : IBufferService<(Register Dest, Constant Value)> {

    private (Register Dest, Constant Value)? _register;
    
    public void Push((Register Dest, Constant Value) value) {
        _register  = value;
    }
    
    public (Register Dest, Constant Value) Pull() => _register ??  (new (0), new (0));
    
    public void Clear() => _register = null;
}