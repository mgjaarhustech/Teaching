using System;

namespace CarParkSim;

public sealed class ConsoleLogger : ILogger
{
    public void Log(string message)
        => Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} [T{Environment.CurrentManagedThreadId}] {message}");
}
