using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Platform.Data.Sequences;

namespace Platform.Data.Experiments
{
    public class SequenceWalkerBenchmark
    {
        // Test data structure - simple binary tree node
        public class TestNode
        {
            public int Value { get; set; }
            public TestNode? Left { get; set; }
            public TestNode? Right { get; set; }
            
            public TestNode(int value) { Value = value; }
        }
        
        // Create a test binary tree
        private static TestNode CreateTestTree(int depth, int startValue = 1)
        {
            if (depth == 0) return new TestNode(startValue);
            
            var node = new TestNode(startValue);
            node.Left = CreateTestTree(depth - 1, startValue * 2);
            node.Right = CreateTestTree(depth - 1, startValue * 2 + 1);
            return node;
        }
        
        // Create a large sequence (linked list structure)
        private static TestNode CreateTestSequence(int length)
        {
            var head = new TestNode(1);
            var current = head;
            
            for (int i = 2; i <= length; i++)
            {
                current.Right = new TestNode(i);
                current = current.Right;
            }
            
            return head;
        }
        
        public static void RunBenchmarks()
        {
            Console.WriteLine("=== SequenceWalker Performance Benchmarks ===\n");
            
            // Test cases
            var testCases = new[]
            {
                ("Small Tree (depth 5)", CreateTestTree(5), 1000),
                ("Medium Tree (depth 10)", CreateTestTree(10), 100),
                ("Large Tree (depth 15)", CreateTestTree(15), 10),
                ("Small Sequence (100 nodes)", CreateTestSequence(100), 1000),
                ("Medium Sequence (1000 nodes)", CreateTestSequence(1000), 100),
                ("Large Sequence (10000 nodes)", CreateTestSequence(10000), 10)
            };
            
            foreach (var (name, testData, iterations) in testCases)
            {
                Console.WriteLine($"Testing: {name} ({iterations} iterations)");
                RunSingleBenchmark(testData, iterations);
                Console.WriteLine();
            }
        }
        
        private static void RunSingleBenchmark(TestNode root, int iterations)
        {
            var results = new List<int>();
            
            // Helper functions
            Func<TestNode?, TestNode?> getLeft = node => node?.Left;
            Func<TestNode?, TestNode?> getRight = node => node?.Right;
            Func<TestNode?, bool> isLeaf = node => node != null && node.Left == null && node.Right == null;
            Action<TestNode?> collect = node => { if (node != null) results.Add(node.Value); };
            
            // Warm up JIT
            for (int i = 0; i < 5; i++)
            {
                results.Clear();
                SequenceWalker.WalkRight(root, getLeft, getRight, isLeaf, collect);
                results.Clear();
                EmitOptimizedSequenceWalker.WalkRight(root, getLeft, getRight, isLeaf, collect);
            }
            
            // Benchmark Original SequenceWalker
            results.Clear();
            var sw = Stopwatch.StartNew();
            
            for (int i = 0; i < iterations; i++)
            {
                results.Clear();
                SequenceWalker.WalkRight(root, getLeft, getRight, isLeaf, collect);
            }
            
            sw.Stop();
            var originalTime = sw.Elapsed;
            var originalNodesProcessed = results.Count;
            
            // Benchmark EmitOptimizedSequenceWalker
            results.Clear();
            sw = Stopwatch.StartNew();
            
            for (int i = 0; i < iterations; i++)
            {
                results.Clear();
                EmitOptimizedSequenceWalker.WalkRight(root, getLeft, getRight, isLeaf, collect);
            }
            
            sw.Stop();
            var optimizedTime = sw.Elapsed;
            var optimizedNodesProcessed = results.Count;
            
            // Verify results are identical
            var resultsMatch = originalNodesProcessed == optimizedNodesProcessed;
            
            // Calculate performance metrics
            var improvementRatio = originalTime.TotalMilliseconds / optimizedTime.TotalMilliseconds;
            var improvementPercent = ((originalTime.TotalMilliseconds - optimizedTime.TotalMilliseconds) / originalTime.TotalMilliseconds) * 100;
            
            // Display results
            Console.WriteLine($"  Original SequenceWalker:     {originalTime.TotalMilliseconds:F2} ms ({originalNodesProcessed} nodes)");
            Console.WriteLine($"  EmitOptimized SequenceWalker: {optimizedTime.TotalMilliseconds:F2} ms ({optimizedNodesProcessed} nodes)");
            Console.WriteLine($"  Results match: {resultsMatch}");
            Console.WriteLine($"  Performance improvement: {improvementRatio:F2}x ({improvementPercent:F1}%)");
            
            if (improvementRatio > 1)
                Console.WriteLine($"  ✓ EmitOptimized is {improvementRatio:F2}x faster");
            else if (improvementRatio < 0.9)
                Console.WriteLine($"  ⚠ EmitOptimized is {1/improvementRatio:F2}x slower");
            else
                Console.WriteLine($"  ≈ Performance is similar");
        }
        
        // Memory usage benchmark
        public static void RunMemoryBenchmark()
        {
            Console.WriteLine("=== Memory Usage Benchmark ===\n");
            
            var testData = CreateTestTree(12);
            const int iterations = 1000;
            
            // Force GC before measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            var initialMemory = GC.GetTotalMemory(false);
            
            // Test original implementation
            var results = new List<int>();
            Func<TestNode?, TestNode?> getLeft = node => node?.Left;
            Func<TestNode?, TestNode?> getRight = node => node?.Right;
            Func<TestNode?, bool> isLeaf = node => node != null && node.Left == null && node.Right == null;
            Action<TestNode?> collect = node => { if (node != null) results.Add(node.Value); };
            
            for (int i = 0; i < iterations; i++)
            {
                results.Clear();
                SequenceWalker.WalkRight(testData, getLeft, getRight, isLeaf, collect);
            }
            
            var memoryAfterOriginal = GC.GetTotalMemory(false);
            
            // Force GC
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            // Test optimized implementation
            for (int i = 0; i < iterations; i++)
            {
                results.Clear();
                EmitOptimizedSequenceWalker.WalkRight(testData, getLeft, getRight, isLeaf, collect);
            }
            
            var memoryAfterOptimized = GC.GetTotalMemory(false);
            
            Console.WriteLine($"Initial memory: {initialMemory:N0} bytes");
            Console.WriteLine($"After original: {memoryAfterOriginal:N0} bytes (diff: {memoryAfterOriginal - initialMemory:N0})");
            Console.WriteLine($"After optimized: {memoryAfterOptimized:N0} bytes (diff: {memoryAfterOptimized - memoryAfterOriginal:N0})");
        }
    }
    
    // Simple test program
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SequenceWalkerBenchmark.RunBenchmarks();
                Console.WriteLine();
                SequenceWalkerBenchmark.RunMemoryBenchmark();
                
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}