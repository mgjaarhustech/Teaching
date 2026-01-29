using CarParkSim;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

ILogger logger = new ConsoleLogger();

IArrivalQueue arrivals = new ChannelArrivalQueue();
ICapacityGate gate = new SemaphoreCapacityGate(capacity: 5);
ISpotPool spots = new ConcurrentSpotPool(capacity: 5);
IStats stats = new AtomicStats();

var park = new CarPark(arrivals, gate, spots, stats, logger);

logger.Log("Starting manager...");
var managerTask = park.RunManagerAsync(cts.Token);

var rng = new Random(123);
var tasks = new List<Task>();

for (int id = 1; id <= 20; id++)
{
    int arrivalDelay = rng.Next(100, 900);
    int stayMs = rng.Next(800, 2500);

    var car = new Car(id, arrivalDelay, stayMs, logger);
    tasks.Add(car.RunAsync(park, cts.Token));
}

try { await Task.WhenAll(tasks); }
catch (OperationCanceledException) { }

park.CompleteArrivals();

try { await managerTask; }
catch (OperationCanceledException) { }

logger.Log($"Done. Entered={stats.Entered}, Exited={stats.Exited}, Inside={stats.Inside}");
