using UnityEngine;
using Vuforia;

public class CustomObserverEventHandler : DefaultObserverEventHandler
{
    public System.Action<string> OnModelFound;
    public System.Action<string> OnModelLost;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        OnModelFound?.Invoke(gameObject.name);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        OnModelLost?.Invoke(gameObject.name);
    }
}