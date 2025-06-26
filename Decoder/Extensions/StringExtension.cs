namespace Decoder.Extensions;

public static class StringExtension {
    private static int GetDestinationRegister(this string instruction) 
        => Convert.ToInt32(instruction.Substring(2,1), 16);

    private static int GetFirstOperandRegister(this string instruction) 
        => Convert.ToInt32(instruction.Substring(3,1), 16);

    private static int GetSecondOperandRegister(this string instruction) 
        => Convert.ToInt32(instruction.Substring(4,1), 16);

    private static int GetConstant(this string instruction) 
        => Convert.ToInt32(instruction.Substring(4,4), 16);

    public static int GetDestinationRegister(this ReadOnlySpan<char> instruction)
        => instruction.ToString().GetDestinationRegister();
    
    public static int GetFirstOperandRegister(this ReadOnlySpan<char> instruction)
        => instruction.ToString().GetFirstOperandRegister();
    
    public static int GetSecondOperandRegister(this ReadOnlySpan<char> instruction)
        => instruction.ToString().GetSecondOperandRegister();
    
    public static int GetConstant(this ReadOnlySpan<char> instruction)
        => instruction.ToString().GetConstant();
    
    public static int FromHex(this char part)
        => Convert.ToInt32($"{part}", 16);
    
    public static int FromHex(this ReadOnlySpan<char> slice)
        => Convert.ToInt32(slice.ToString(), 16);
}