using System;
using Platform.Data;

namespace Platform.Data.Tests
{
    public class IndexerTest
    {
        public static void Main()
        {
            Console.WriteLine("Testing Point<ulong> indexer...");
            var point = new Point<ulong>(42UL, 3);
            
            // Test valid indices
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"point[{i}] = {point[i]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error with valid indices: {ex.Message}");
            }
            
            // Test negative index (should throw)
            try
            {
                var result = point[-1];
                Console.WriteLine($"ERROR: Negative index should have thrown exception, but got: {result}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("✓ Negative index correctly throws IndexOutOfRangeException");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Unexpected exception type: {ex.GetType().Name}: {ex.Message}");
            }
            
            // Test out-of-bounds index (should throw)
            try
            {
                var result = point[3];
                Console.WriteLine($"ERROR: Out-of-bounds index should have thrown exception, but got: {result}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("✓ Out-of-bounds index correctly throws IndexOutOfRangeException");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Unexpected exception type: {ex.GetType().Name}: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.WriteLine("Testing LinkAddress<ulong> indexer...");
            var linkAddress = new LinkAddress<ulong>(123UL);
            
            // Test valid index (0)
            try
            {
                Console.WriteLine($"linkAddress[0] = {linkAddress[0]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error with valid index 0: {ex.Message}");
            }
            
            // Test negative index (should throw)
            try
            {
                var result = linkAddress[-1];
                Console.WriteLine($"ERROR: Negative index should have thrown exception, but got: {result}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("✓ Negative index correctly throws IndexOutOfRangeException");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Unexpected exception type: {ex.GetType().Name}: {ex.Message}");
            }
            
            // Test positive index other than 0 (should throw)
            try
            {
                var result = linkAddress[1];
                Console.WriteLine($"ERROR: Index 1 should have thrown exception, but got: {result}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("✓ Index 1 correctly throws IndexOutOfRangeException");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Unexpected exception type: {ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}