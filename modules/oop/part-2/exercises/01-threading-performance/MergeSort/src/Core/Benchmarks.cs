using System;
using System.Diagnostics;

namespace MergeSort;


// NOTE: GC.GetAllocatedBytesForCurrentThread() is per-thread.
// For ThreadPool/Parallel work, some allocations happen on worker threads and won't be fully reflected here. 
// This is fine for this lab; we mainly compare time and correctness. (Advanced: also sample GC.GetTotalMemory)

public static class Benchmarks
{
    public static void Bench(string name, Func<int[]> runAndReturn, int iterations = 3)
    {
        long bestTicks = long.MaxValue;
        long bestAlloc = long.MaxValue;
        bool ok = true;

        for (int i = 0; i < iterations; i++)
        {
            long allocBefore = GC.GetAllocatedBytesForCurrentThread();
            long t0 = Stopwatch.GetTimestamp();

            var sorted = runAndReturn();

            long t1 = Stopwatch.GetTimestamp();
            long allocAfter = GC.GetAllocatedBytesForCurrentThread();

            ok &= IsSorted(sorted);

            long ticks = t1 - t0;
            long alloc = allocAfter - allocBefore;

            if (ticks < bestTicks) bestTicks = ticks;
            if (alloc < bestAlloc) bestAlloc = alloc;
        }

        double ms = bestTicks * 1000.0 / Stopwatch.Frequency;
        Console.WriteLine($"{name,-30} {ms,8:0.00} ms   alloc={bestAlloc,10} B   sorted={ok}");
    }

    public static void PrintMemory(string label)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        Console.WriteLine($"{label,-12} totalMem={GC.GetTotalMemory(false) / (1024.0 * 1024.0):0.0} MiB");
    }

    private static bool IsSorted(int[] a)
    {
        for (int i = 1; i < a.Length; i++)
            if (a[i - 1] > a[i]) return false;
        return true;
    }
}
