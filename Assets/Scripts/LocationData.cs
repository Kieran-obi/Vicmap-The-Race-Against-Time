using UnityEngine;


//represents one real-world location
//one asset per place so it can be reused
[CreateAssetMenu(fileName = "LocationData", menuName = "Vicmap/Location")]
public class LocationData : ScriptableObject
{
    public string locationName;
    public float gameX; //the datasets abstract position now keeping for reference
    public float gameY;
    public Vector2 mapAnchoredPosition; //the actual pixel position on our map art
}