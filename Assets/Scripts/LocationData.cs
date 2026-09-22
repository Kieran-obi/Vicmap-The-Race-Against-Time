using UnityEngine;


//represents one real-world location
//one asset per place so it can be reused
[CreateAssetMenu(fileName = "LocationData", menuName = "Vicmap/Location")]
public class LocationData : ScriptableObject
{
    public string locationName;
    public float gameX;
    public float gameY;
    //
}