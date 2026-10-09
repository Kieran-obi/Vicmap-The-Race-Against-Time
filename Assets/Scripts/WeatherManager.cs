using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager Instance;
    public bool isPaused = false;
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

    [Header("Scene Control")]
    [Tooltip("The weather starts when this scene loads. Must match the scene name exactly.")]
    [SerializeField] private string gameplaySceneName = "VicmapRoom";
    [Tooltip("The weather resets when this scene loads. Must match the scene name exactly.")]
    [SerializeField] private string menuSceneName = "Main_Menu";

    // Global event hook that other scripts can listen to
    public static event Action<WeatherEvent> OnWeatherEventFired;

    // Fired when a new run begins, so other systems (e.g. the storm
    // progress bar) can reset themselves.
    public static event Action OnWeatherReset;

    private float timer = 0f;
    private float elapsedRunTime = 0f;
    private bool isRunning = false;
    private List<ScheduledEvent> processedEvents = new List<ScheduledEvent>();

    private void Awake()
    {
        // The first WeatherManager keeps the slot. A duplicate (which
        // WeatherManagerPersistence destroys) must not take it over.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("[WeatherManager] Duplicate created - keeping the original as Instance.");
        }
    }

    private void OnEnable()
    {
        // Unity's own built-in event, announced every time a scene loads.
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void Start()
    {
        // A duplicate copy is about to be destroyed, so it must not act.
        if (Instance != this) return;

        // Covers pressing Play in the Editor with the gameplay scene
        // already open, where it was never "loaded" from the menu.
        if (SceneManager.GetActiveScene().name == gameplaySceneName)
        {
            StartWeather();
        }
    }

    private void Update()
    {
        // The clock only runs while a run is in progress and not paused.
        if (!isRunning || isPaused) return;
        //increment the background timer over time
        timer += Time.deltaTime;
        elapsedRunTime += Time.deltaTime;

        // Every time the timer crosses the tick rate threshold
        if (timer >= tickRate)
        {
            timer = 0f; // Reset the tick timer
            CheckTimeline(elapsedRunTime);
        }
    }

    // Starts the storm clock. Does nothing if a run is already in progress,
    // so returning from a CCTV scene can't restart a storm that's underway.
    public void StartWeather()
    {
        if (isRunning) return;

        isRunning = true;
        Debug.Log("[WeatherManager] Weather started.");
    }

    // Stops the storm and wipes its progress, ready for a new run.
    // Clearing processedEvents matters: without it, a replay would think
    // every event had already fired and never fire any.
    public void ResetWeather()
    {
        isRunning = false;
        isPaused = false;
        timer = 0f;
        elapsedRunTime = 0f;
        processedEvents.Clear();

        OnWeatherReset?.Invoke();
        Debug.Log("[WeatherManager] Weather reset.");
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // A duplicate copy is about to be destroyed, so it must not act.
        if (Instance != this) return;

        if (scene.name == menuSceneName)
        {
            ResetWeather();
        }
        else if (scene.name == gameplaySceneName)
        {
            StartWeather();
        }
    }

    private void CheckTimeline(float currentRuntime)
    {
        // Loop through all scheduled weather events
        foreach (var entry in timeline)
        {
            if (isPaused) break;// a fired event (an incoming call) may have just paused us

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
