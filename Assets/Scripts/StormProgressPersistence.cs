using UnityEngine;

/// <summary>
/// Keeps the storm progress UI alive across scene loads, and makes sure
/// only one copy of it ever exists at a time.
/// </summary>
public class StormProgressPersistence : MonoBehaviour
{
    // The one accepted copy of this object. Any other script can reach it
    // as StormProgressPersistence.Instance, without needing a scene reference.
    public static StormProgressPersistence Instance;

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
