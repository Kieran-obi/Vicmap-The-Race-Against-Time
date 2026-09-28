using UnityEngine;

/// <summary>
/// Keeps the WeatherManager GameObject alive across scene loads, in case
/// leaving survRoom for a camera view (or anywhere else) turns out to
/// fully unload survRoom while doing so.
/// </summary>
public class WeatherManagerPersistence : MonoBehaviour
{

    public static WeatherManagerPersistence Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
