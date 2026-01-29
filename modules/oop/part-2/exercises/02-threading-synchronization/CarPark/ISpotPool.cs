namespace CarParkSim;

public interface ISpotPool
{
    int TakeSpot();
    void ReturnSpot(int spotId);
}
