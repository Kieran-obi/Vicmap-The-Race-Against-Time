using UnityEngine;

/// <summary>
/// Counts how many storm stages have fired, and reports that progress
/// (as a 0-1 fraction) to the StormProgressUI script.
///
/// This script doesn't touch any visuals itself - it only listens for
/// WeatherManager's broadcast, counts, and hands the result to whatever
/// script actually knows how to display it.
/// </summary>
public class StormStageTracker : MonoBehaviour
{
    [SerializeField] private StormProgressUI progressUI;

    // Fixed, because this game always has exactly three storm stages.
    // This only works correctly if all three scheduled stages in
    // WeatherManager have their Chance set to 1.0 (guaranteed) - a
    // skipped stage would mean this count never reaches 3.
    private const int totalStages = 3;

    private int stagesCompleted = 0;

    private void OnEnable()
    {
        // Subscribing here (rather than Start) pairs naturally with
        // unsubscribing in OnDisable below, so this object never listens
        // while it's inactive or about to be destroyed.
        WeatherManager.OnWeatherEventFired += HandleWeatherEventFired;
        WeatherManager.OnWeatherReset += HandleWeatherReset;
    }

    private void OnDisable()
    {
        // Always unsubscribe when done listening. Skipping this would let
        // WeatherManager keep a reference to this object indefinitely,
        // even after it should be gone.
        WeatherManager.OnWeatherEventFired -= HandleWeatherEventFired;
        WeatherManager.OnWeatherReset -= HandleWeatherReset;
    }

    private void HandleWeatherEventFired(WeatherEvent weatherEvent)
    {
        stagesCompleted++;

        float fraction = (float)stagesCompleted / totalStages;
        progressUI.SetProgress(fraction);

        Debug.Log($"[StormStageTracker] Stage {stagesCompleted}/{totalStages} complete ({weatherEvent.EventName}).");
    }

    // A new run has begun: forget the old count and empty the bar.
    private void HandleWeatherReset()
    {
        stagesCompleted = 0;
        progressUI.ResetProgress();
    }
}
