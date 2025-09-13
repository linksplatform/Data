using System;
using System.Numerics;

// Analysis of what AbsoluteValue does vs setting MSB to 0
// For unsigned types, setting MSB to 0 effectively removes the sign bit
// This should be equivalent to Math.Abs for the conversion from signed to unsigned

// Current approach:
// 1. Convert TLinkAddress (unsigned) to signed long 
// 2. Take Math.Abs to ensure positive value
// 3. Convert back to TLinkAddress

// Simplified approach:
// Just clear the most significant bit (set to 0)
// For unsigned types, this removes the "sign" interpretation

// Example with ulong (64-bit):
// MSB mask: 0x7FFFFFFFFFFFFFFF (all bits 1 except MSB)
// value & mask clears the MSB

public class AnalysisExample
{
    public static void Test()
    {
        ulong value = 0xFFFFFFFFFFFFFFFF; // All bits set
        ulong withMsbCleared = value & 0x7FFFFFFFFFFFFFFF; // Clear MSB
        
        Console.WriteLine($"Original: {value:X}");
        Console.WriteLine($"MSB cleared: {withMsbCleared:X}");
        
        // This should be equivalent to taking absolute value of the signed interpretation
        long signed = (long)value; // -1 in signed
        long abs = Math.Abs(signed); // 1 in absolute
        Console.WriteLine($"Math.Abs approach: {abs:X}");
    }
}