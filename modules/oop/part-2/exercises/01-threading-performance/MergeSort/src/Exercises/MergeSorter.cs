using System;
using System.Threading;
using System.Threading.Tasks;

namespace MergeSort;

public static class MergeSorter
{
    // ----------------------- Public entry points (students DON'T change) -----------------------

    public static void Sequential(int[] a)
    {
        ArgumentNullException.ThrowIfNull(a);
        if (a.Length <= 1) return;

        var temp = new int[a.Length];
        SortSeq(a, temp, 0, a.Length);
    }

    // Exercise 2: Threads (depth-limited)
    public static void ThreadsDepthLimited(int[] a, int maxDepth)
    {
        // Exercise 5: Fail fast
        ArgumentNullException.ThrowIfNull(a);
        if (maxDepth < 0) throw new ArgumentOutOfRangeException(nameof(maxDepth), "maxDepth must be >= 0");
        if (a.Length <= 1) return;

        var temp = new int[a.Length];
        SortThreads(a, temp, 0, a.Length, depthLeft: maxDepth);
    }

    // Exercise 3: Task/ThreadPool (cutoff)
    public static void TaskPoolCutoff(int[] a, int cutoff)
    {
        // Exercise 5: Fail fast
        ArgumentNullException.ThrowIfNull(a);
        if (cutoff <= 1) throw new ArgumentOutOfRangeException(nameof(cutoff), "cutoff must be > 1 to allow splitting");
        if (a.Length <= 1) return;

        var temp = new int[a.Length];
        SortTaskPool(a, temp, 0, a.Length, cutoff);
    }

    // Exercise 4: Parallel.Invoke (cutoff) (Optional)
    public static void ParallelInvokeCutoff(int[] a, int cutoff)
    {
        // Exercise 5: Fail fast
        ArgumentNullException.ThrowIfNull(a);
        if (cutoff <= 1) throw new ArgumentOutOfRangeException(nameof(cutoff), "cutoff must be > 1 to allow splitting");
        if (a.Length <= 1) return;

        var temp = new int[a.Length];
        SortParallelInvoke(a, temp, 0, a.Length, cutoff);
    }

    // ----------------------- Given helpers (students DON'T change) -----------------------

    private static void SortSeq(int[] a, int[] temp, int lo, int hi)
    {
        int len = hi - lo;
        if (len <= 1) return;

        int mid = lo + len / 2;
        SortSeq(a, temp, lo, mid);
        SortSeq(a, temp, mid, hi);
        Merge(a, temp, lo, mid, hi);
    }

    private static void Merge(int[] a, int[] temp, int lo, int mid, int hi)
    {
        int i = lo, j = mid, k = lo;

        // Stable merge: keep left when equal
        while (i < mid && j < hi)
            temp[k++] = (a[i] <= a[j]) ? a[i++] : a[j++];

        while (i < mid) temp[k++] = a[i++];
        while (j < hi) temp[k++] = a[j++];

        Array.Copy(temp, lo, a, lo, hi - lo);
    }

    // ----------------------- Exercise 2: Threads (students FILL IN) -----------------------

    /// <summary>
    /// Requirements:
    /// - If depthLeft <= 0: SortSeq(a, temp, lo, hi)
    /// - Else:
    ///   * split at mid
    ///   * start ONE Thread for ONE half (e.g., left)
    ///   * sort the other half inline (e.g., right)
    ///   * Join() before Merge(...)
    /// - Use locals to avoid capturing outer variables in the lambda
    /// - Result must be correct (sorted=True)
    /// </summary>
    private static void SortThreads(int[] a, int[] temp, int lo, int hi, int depthLeft)
    {
        int len = hi - lo;
        if (len <= 1) return;

        if (depthLeft <= 0)
        {
            SortSeq(a, temp, lo, hi);
            return;
        }

        int mid = lo + len / 2;

        // TODO: Exercise 2 implementation goes here

        throw new NotImplementedException("Exercise 2: Implement Thread-based split + join + merge.");
    }

    // ----------------------- Exercise 3: Task/ThreadPool (students FILL IN) -----------------------

    /// <summary>
    /// Requirements:
    /// - If len <= cutoff: SortSeq(a, temp, lo, hi)
    /// - Else:
    ///   * split at mid
    ///   * Task.Run(...) ONE half (ThreadPool)
    ///   * sort the other half inline
    ///   * Wait before Merge(...), e.g., task.GetAwaiter().GetResult()
    /// - Use locals to avoid capturing outer variables in the lambda
    /// - Result must be correct (sorted=True)
    /// </summary>
    private static void SortTaskPool(int[] a, int[] temp, int lo, int hi, int cutoff)
    {
        int len = hi - lo;
        if (len <= 1) return;

        if (len <= cutoff)
        {
            SortSeq(a, temp, lo, hi);
            return;
        }

        int mid = lo + len / 2;

        // TODO: Exercise 3 implementation goes here

        throw new NotImplementedException("Exercise 3: Implement Task.Run-based split + wait + merge.");
    }

    // ----------------------- Exercise 4: Parallel.Invoke (students FILL IN) -----------------------

    /// <summary>
    /// Requirements:
    /// - If len <= cutoff: SortSeq(a, temp, lo, hi)
    /// - Else:
    ///   * Parallel.Invoke(() => left, () => right)
    ///   * Merge(...) after both complete
    /// - Use locals to avoid capturing outer variables in lambdas
    /// - Result must be correct (sorted=True)
    /// </summary>
    private static void SortParallelInvoke(int[] a, int[] temp, int lo, int hi, int cutoff)
    {
        int len = hi - lo;
        if (len <= 1) return;

        if (len <= cutoff)
        {
            SortSeq(a, temp, lo, hi);
            return;
        }

        int mid = lo + len / 2;

        // TODO: Exercise 4 implementation goes here

        throw new NotImplementedException("Exercise 4: Implement Parallel.Invoke-based fork-join + merge.");
    }
}
