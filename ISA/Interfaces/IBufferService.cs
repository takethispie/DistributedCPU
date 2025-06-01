namespace ISA.Interfaces;

public interface IBufferService<T> {
    void Push(T value);
    T? Pull();
}