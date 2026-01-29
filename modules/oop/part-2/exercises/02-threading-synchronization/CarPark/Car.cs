using System.Threading;
using System.Threading.Tasks;

namespace CarParkSim;

public sealed class Car
{
    private readonly int _arrivalDelayMs;
    private readonly int _stayMs;
    private readonly ILogger _log;

    public int Id { get; }

    public Car(int id, int arrivalDelayMs, int stayMs, ILogger log)
    {
        Id = id;
        _arrivalDelayMs = arrivalDelayMs;
        _stayMs = stayMs;
        _log = log;
    }

    public async Task RunAsync(CarPark park, CancellationToken ct)
    {
        await Task.Delay(_arrivalDelayMs, ct);
        _log.Log($"Car {Id} arrives.");

        int spot = await park.EnterAsync(Id, ct);
        _log.Log($"Car {Id} ENTERED, spot {spot}.");

        await Task.Delay(200, ct); // drive in
        _log.Log($"Car {Id} parked at {spot} for {_stayMs}ms.");
        await Task.Delay(_stayMs, ct);

        await Task.Delay(200, ct); // drive out
        park.Exit(Id, spot);
        _log.Log($"Car {Id} EXITED.");
    }
}
