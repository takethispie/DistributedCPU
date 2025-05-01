using ISA.Data;
using ISA.Interfaces;

namespace RegisterFile.Services;

public class DestinationBufferService : IBufferService<(Register Dest, bool WriteEnable)> {

    private (Register Dest, bool WriteEnable) _register;
    
    public void Push((Register Dest, bool WriteEnable) value) {
        _register  = value;
    }
    
    public (Register Dest, bool WriteEnable) Pull() => _register;
}