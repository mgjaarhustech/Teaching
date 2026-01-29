using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CarParkSim;

public sealed class ChannelArrivalQueue : IArrivalQueue
{
    private readonly Channel<EntryRequest> _channel =
        Channel.CreateUnbounded<EntryRequest>(
            new UnboundedChannelOptions { SingleReader = true, SingleWriter = false });

    public ValueTask EnqueueAsync(EntryRequest request, CancellationToken ct)
    {
        // TODO:
        // - Write the request to _channel.Writer using WriteAsync
        // - Return the ValueTask from WriteAsync
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<EntryRequest> ReadAllAsync(CancellationToken ct)
    {
        // TODO:
        // - Return _channel.Reader.ReadAllAsync(ct)
        throw new NotImplementedException();
    }

    public void Complete()
    {
        // TODO:
        // - Complete the writer (TryComplete)
        throw new NotImplementedException();
    }
}
