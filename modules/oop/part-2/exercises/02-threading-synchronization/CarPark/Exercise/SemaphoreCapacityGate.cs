using System.Threading;
using System.Threading.Tasks;

namespace CarParkSim;

public sealed class SemaphoreCapacityGate : ICapacityGate
{
    private readonly SemaphoreSlim _semaphore;

    public SemaphoreCapacityGate(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        _semaphore = new SemaphoreSlim(capacity, capacity);
    }

    public ValueTask WaitAsync(CancellationToken ct)
    {
        // TODO:
        // - Await _semaphore.WaitAsync(ct) but return as ValueTask
        // Hint: return new ValueTask(_semaphore.WaitAsync(ct));
        throw new NotImplementedException();
    }

    public void Release()
    {
        // TODO:
        // - Release semaphore
        throw new NotImplementedException();
    }
}
