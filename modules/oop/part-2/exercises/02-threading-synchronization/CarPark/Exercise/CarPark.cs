using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarParkSim;

public sealed class CarPark
{
    private readonly IArrivalQueue _arrivals;
    private readonly ICapacityGate _gate;
    private readonly ISpotPool _spots;
    private readonly IStats _stats;
    private readonly ILogger _log;

    public CarPark(IArrivalQueue arrivals, ICapacityGate gate, ISpotPool spots, IStats stats, ILogger log)
    {
        _arrivals = arrivals ?? throw new ArgumentNullException(nameof(arrivals));
        _gate = gate ?? throw new ArgumentNullException(nameof(gate));
        _spots = spots ?? throw new ArgumentNullException(nameof(spots));
        _stats = stats ?? throw new ArgumentNullException(nameof(stats));
        _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public void CompleteArrivals() => _arrivals.Complete();

    public async Task<int> EnterAsync(int carId, CancellationToken ct)
    {
        // TODO:
        // 1) Create a TaskCompletionSource<int>(RunContinuationsAsynchronously)
        // 2) Enqueue new EntryRequest(carId, tcs) into _arrivals
        // 3) Await tcs.Task with cancellation: return await tcs.Task.WaitAsync(ct)
        throw new NotImplementedException();
    }

    public void Exit(int carId, int spotId)
    {
        // TODO:
        // 1) ReturnSpot(spotId)
        // 2) _gate.Release()
        // 3) _stats.MarkExited()
        // 4) Optional log: inside count
        throw new NotImplementedException();
    }

    public async Task RunManagerAsync(CancellationToken ct)
    {
        _log.Log("Manager started.");

        // TODO:
        // foreach await request in _arrivals.ReadAllAsync(ct):
        //   1) await _gate.WaitAsync(ct)
        //   2) int spot = _spots.TakeSpot()
        //   3) _stats.MarkEntered()
        //   4) request.SpotTcs.TrySetResult(spot)
        //   5) optional Task.Delay(100, ct) for gate animation
        throw new NotImplementedException();
    }
}
