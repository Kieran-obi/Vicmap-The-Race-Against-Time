using UnityEngine;

/// <summary>
/// Counts how many storm stages have fired, and reports that progress
/// (as a 0-1 fraction) to the StormProgressUI script.
/// </summary>
public class StormStageTracker : MonoBehaviour
{
    [SerializeField] private StormProgressUI progressUI;

    private const int totalStages = 3;

    private int stagesCompleted = 0;

    private void OnEnable()
    {
     
        WeatherManager.OnWeatherEventFired += HandleWeatherEventFired;
    }

    private void OnDisable()
    {
  
        WeatherManager.OnWeatherEventFired -= HandleWeatherEventFired;
    }

    private void HandleWeatherEventFired(WeatherEvent weatherEvent)
    {
        stagesCompleted++;

        float fraction = (float)stagesCompleted / totalStages;
        progressUI.SetProgress(fraction);

        Debug.Log($"[StormStageTracker] Stage {stagesCompleted}/{totalStages} complete ({weatherEvent.EventName}).");
    }
}
