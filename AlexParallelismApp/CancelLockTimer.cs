using AlexParallelismApp.Controllers;

namespace AlexParallelismApp;

public class CancelLockTimer
{
    private const int TimeOfWaiting = 300000;

    private readonly Dictionary<int, System.Timers.Timer> _timers;
    private readonly YEntityController _controller;

    public CancelLockTimer(YEntityController controller)
    {
        _timers = new Dictionary<int, System.Timers.Timer>();
        _controller = controller;
    }

    public void StartTimer(int yEntityId)
    {
        System.Timers.Timer timer = new System.Timers.Timer(TimeOfWaiting);
        timer.Elapsed += (_, _) => _controller.CancelLock(yEntityId).Wait();
        timer.Start();
        _timers.Add(yEntityId, timer);
    }

    public void StopTimer(int yEntityId)
    {
        _timers[yEntityId].Stop();
        _timers[yEntityId].Dispose();
        _timers.Remove(yEntityId);
    }

    public void UpdateTimer(int yEntityId)
    {
        StopTimer(yEntityId);
        StartTimer(yEntityId);
    }
}