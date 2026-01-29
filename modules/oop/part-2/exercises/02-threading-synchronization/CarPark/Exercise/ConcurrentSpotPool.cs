using System.Collections.Concurrent;

namespace CarParkSim;

public sealed class ConcurrentSpotPool : ISpotPool
{
    private readonly ConcurrentQueue<int> _free = new();

    public ConcurrentSpotPool(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        for (int i = 1; i <= capacity; i++) _free.Enqueue(i);
    }

    public int TakeSpot()
    {
        // TODO:
        // - TryDequeue from _free
        // - If it fails, throw InvalidOperationException (logic error)
        throw new NotImplementedException();
    }

    public void ReturnSpot(int spotId)
    {
        // TODO:
        // - Enqueue back into _free
        throw new NotImplementedException();
    }
}
