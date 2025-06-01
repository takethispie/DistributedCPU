using ISA.Data;
using ISA.Interfaces;
using ISA.Messages;

namespace RegisterFile.Services;

public class LoadingBufferService : IBufferService<ToRegisterFile> {
    
    private ToRegisterFile? _buffer;
    
    public void Push(ToRegisterFile value) {
        _buffer = value;
    }
    
    public ToRegisterFile? Pull() => _buffer;
    
    public void Clear() {
        _buffer = null;
    }
}