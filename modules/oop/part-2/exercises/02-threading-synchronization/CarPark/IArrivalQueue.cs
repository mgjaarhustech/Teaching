using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarParkSim;

public interface IArrivalQueue
{
    ValueTask EnqueueAsync(EntryRequest request, CancellationToken ct);
    IAsyncEnumerable<EntryRequest> ReadAllAsync(CancellationToken ct);
    void Complete();
}
