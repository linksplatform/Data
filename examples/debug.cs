using System;
using Platform.Data;

class DebugTest
{
    static void Main()
    {
        // Test with ulong max value (all bits set)
        ulong input = 0xFFFFFFFFFFFFFFFFUL;
        Console.WriteLine($"Input: {input} (0x{input:X})");
        
        var hybrid = new Hybrid<ulong>(input);
        Console.WriteLine($"Hybrid.SignedValue: {hybrid.SignedValue}");
        Console.WriteLine($"Hybrid.AbsoluteValue: {hybrid.AbsoluteValue}");
        Console.WriteLine($"Expected MSB Clear: {input & 0x7FFFFFFFFFFFFFFFUL} (0x{input & 0x7FFFFFFFFFFFFFFFUL:X})");
        
        // Test the conversion process
        long signed = (long)input; // This should be -1
        Console.WriteLine($"Signed cast: {signed}");
        long absolute = Math.Abs(signed); // This should be 1
        Console.WriteLine($"Math.Abs: {absolute}");
    }
}