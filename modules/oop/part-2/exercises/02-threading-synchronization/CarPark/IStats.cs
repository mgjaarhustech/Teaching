namespace CarParkSim;

public interface IStats
{
    int Entered { get; }
    int Exited { get; }
    int Inside { get; }

    void MarkEntered();
    void MarkExited();
}
