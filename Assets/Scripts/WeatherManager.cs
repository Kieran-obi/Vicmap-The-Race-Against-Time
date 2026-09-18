using System;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    // A simple wrapper structure that displays nicely in Unity's Inspector
    [Serializable]
    public struct ScheduledEvent
    {
        [Tooltip("The game time (in seconds) when this event should trigger.")]
        public float timestamp;
        public WeatherEvent weatherData;
    }

    [Header("Settings")]
    [SerializeField] private float tickRate = 5.0f;
    [SerializeField] private List<ScheduledEvent> timeline = new List<ScheduledEvent>();

    // Global event hook that other scripts can listen to
    public static event Action<WeatherEvent> OnWeatherEventFired;

    private float timer = 0f;
    private List<ScheduledEvent> processedEvents = new List<ScheduledEvent>();

    private void Update()
    {
        // Increment the background timer over time
        timer += Time.deltaTime;

        // Every time the timer crosses the tick rate threshold
        if (timer >= tickRate)
        {
            timer = 0f; // Reset the tick timer
            CheckTimeline(Time.time);
        }
    }

    private void CheckTimeline(float currentRuntime)
    {
        // Loop through all scheduled weather events
        foreach (var entry in timeline)
        {
            // If the event time has passed and we haven't fired it yet
            if (entry.timestamp <= currentRuntime && !processedEvents.Contains(entry))
            {
                // Roll the dice (0.0 to 1.0) against the event's chance
                if (UnityEngine.Random.value <= entry.weatherData.Chance)
                {
                    Debug.Log($"[WeatherManager] Firing {entry.weatherData.EventName} at {currentRuntime:F1}s!");

                    // Broadcast to anyone listening
                    OnWeatherEventFired?.Invoke(entry.weatherData);
                }
                else
                {
                    Debug.Log($"[WeatherManager] Skipped {entry.weatherData.EventName} due to random chance.");
                }

                // Mark as processed so it never runs again
                processedEvents.Add(entry);
            }
        }
    }
}
