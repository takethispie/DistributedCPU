namespace Memory.Services;

public class MemoryService(int size) {
    private string[] Content { get; } = new string[size];

    public string Read(int adress) {
        if(adress > Content.Length) 
            throw new ArgumentException("out of bound memory exception", nameof(adress));
        return Content[adress];
    }

    public void Write(int adress, string content) {
        if(adress > Content.Length) 
            throw new ArgumentException("out of bound memory exception", nameof(adress));
        Content[adress] = content;
    }
}