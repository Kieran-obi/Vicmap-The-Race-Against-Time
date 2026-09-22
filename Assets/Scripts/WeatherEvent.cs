using System;
using UnityEngine;

[Serializable]
public class WeatherEvent
{
    [SerializeField] private string eventName = "Storm";

    // Limits the slider in the Inspector between 0.0 and 1.0
    [Range(0f, 1f)][SerializeField] private double intensity = 0.5;
    [Range(0f, 1f)][SerializeField] private double chance = 1.0;

    // Public properties to allow WeatherManager to read the data securely
    public string EventName => eventName;
    public double Intensity => intensity;
    public double Chance => chance;
}
