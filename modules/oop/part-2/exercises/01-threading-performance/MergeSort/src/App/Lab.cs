using System;

namespace MergeSort;

public static class Lab
{
    public static void Run()
    {
        Console.WriteLine($".NET: {Environment.Version}");
        Console.WriteLine($"Cores: {Environment.ProcessorCount}");
        Console.WriteLine();

        var rng = new Random(123);

        Benchmarks.PrintMemory("Before data");
        var big = DataFactory.RandomInts(1_500_000, rng);
        var small = (int[])big[..300_000].Clone();
        Benchmarks.PrintMemory("After data");
        Console.WriteLine();

        // Warmup JIT: compile hot paths before timing (avoids one-time JIT cost skewing results)
        {
            var warm = (int[])small.Clone();
            MergeSorter.Sequential(warm);
        }

        
        // Baselines: measure sequential on big and small
        // - small: overhead dominates; good to show why tiny tasks/threads hurt
        // - big: work is ample; parallel variants may help

        Benchmarks.Bench("Sequential (big)", () =>
        {
            var a = (int[])big.Clone();
            MergeSorter.Sequential(a);
            return a;
        });

        Benchmarks.Bench("Sequential (small)", () =>
        {
            var a = (int[])small.Clone();
            MergeSorter.Sequential(a);
            return a;
        });

        // Threads (depth-limited): small array (OS threads are heavy)
        for (int depth = 1; depth <= 6; depth++)
        {
            int d = depth; // local capture for clarity
            Benchmarks.Bench($"Threads depth={d} (small)", () =>
            {
                var a = (int[])small.Clone();
                MergeSorter.ThreadsDepthLimited(a, maxDepth: d);
                return a;
            });
        }

        // Task/ThreadPool (cutoff): use big array
        foreach (var cutoff in new[] { 2_048, 8_192, 32_768, 131_072 })
        {
            int c = cutoff;
            Benchmarks.Bench($"TaskPool cutoff={c} (big)", () =>
            {
                var a = (int[])big.Clone();
                MergeSorter.TaskPoolCutoff(a, cutoff: c);
                return a;
            });
        }

        // Parallel.Invoke (cutoff): use big array
        foreach (var cutoff in new[] { 2_048, 8_192, 32_768, 131_072 })
        {
            int c = cutoff;
            Benchmarks.Bench($"Parallel cutoff={c} (big)", () =>
            {
                var a = (int[])big.Clone();
                MergeSorter.ParallelInvokeCutoff(a, cutoff: c);
                return a;
            });
        }
    }
}
