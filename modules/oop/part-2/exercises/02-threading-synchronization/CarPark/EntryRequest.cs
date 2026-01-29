using System.Threading.Tasks;

namespace CarParkSim;

public sealed record EntryRequest(int CarId, TaskCompletionSource<int> SpotTcs);
