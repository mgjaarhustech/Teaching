using System;

namespace MergeSort;

public static class DataFactory
{

// Note: rng is passed in so Labs can seed it for reproducibility.
// Using rng.Next() gives non-negative ints; that's fine here.

    public static int[] RandomInts(int count, Random rng)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        ArgumentNullException.ThrowIfNull(rng);

        var a = new int[count];
        for (int i = 0; i < a.Length; i++) a[i] = rng.Next();
        return a;
    }
}
