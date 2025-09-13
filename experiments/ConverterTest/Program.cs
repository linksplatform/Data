using System;
using Platform.Data;
using Platform.Data.Numbers.Raw;

namespace ExperimentalTest
{
    class Program
    {
        static void Main()
        {
            // Test with byte (8-bit unsigned integer)
            var converter = new AddressToRawNumberConverter<byte>();
            
            Console.WriteLine("Testing AddressToRawNumberConverter with byte:");
            
            // Test various input values
            byte[] testValues = { 0, 1, 2, 127, 128, 255 };
            
            foreach (byte input in testValues)
            {
                var result = converter.Convert(input);
                var hybrid = new Hybrid<byte>(input, true);
                byte msbSet = (byte)(input | 0x80); // What MSB setting should be
                
                Console.WriteLine($"Input: {input} (0x{input:X2})");
                Console.WriteLine($"  New converter result: {result} (0x{result:X2})");
                Console.WriteLine($"  Old Hybrid(input, true): {hybrid.Value} (0x{hybrid.Value:X2})");
                Console.WriteLine($"  Expected MSB set: {msbSet} (0x{msbSet:X2})");
                Console.WriteLine($"  Binary: {Convert.ToString(result, 2).PadLeft(8, '0')}");
                Console.WriteLine($"  MSB set? {((result & 0x80) != 0)}");
                Console.WriteLine($"  Matches expected? {result == msbSet}");
                Console.WriteLine();
            }
            
            // Test what "set MSB to 1" would mean
            Console.WriteLine("What 'set MSB to 1' would mean:");
            foreach (byte input in testValues)
            {
                byte msbSet = (byte)(input | 0x80); // Set most significant bit
                Console.WriteLine($"Input: {input} -> MSB set: {msbSet} (0x{msbSet:X2}) Binary: {Convert.ToString(msbSet, 2).PadLeft(8, '0')}");
            }
            
        }
    }
}
