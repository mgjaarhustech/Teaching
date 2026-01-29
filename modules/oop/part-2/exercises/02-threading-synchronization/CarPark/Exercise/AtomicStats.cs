using System.Threading;

namespace CarParkSim;

public sealed class AtomicStats : IStats
{
    private int _entered;
    private int _exited;

    public int Entered => _entered;
    public int Exited => _exited;
    public int Inside => _entered - _exited;

    public void MarkEntered()
    {
        // TODO:
        // - Use Interlocked.Increment(ref _entered)
        throw new NotImplementedException();
    }

    public void MarkExited()
    {
        // TODO:
        // - Use Interlocked.Increment(ref _exited)
        throw new NotImplementedException();
    }
}
