using System.Threading;
using System.Threading.Tasks;

namespace CarParkSim;

public interface ICapacityGate
{
    ValueTask WaitAsync(CancellationToken ct);
    void Release();
}
