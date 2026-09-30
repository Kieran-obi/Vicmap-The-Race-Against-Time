using UnityEngine;
using UnityEngine.EventSystems;

public class HazardSpawner : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Must match a 'type' entry in HazardManager's prefab list")]
    public string hazardType = "RoadBlock"; //set a prefab

    [Tooltip("Where new hazards appear on the map (anchored position inside Hazards OnMap)")]
    public Vector2 spawnAnchoredPosition = Vector2.zero;

    public void OnPointerClick(PointerEventData eventData)
    {
        HazardManager.Instance.SpawnHazard(hazardType, spawnAnchoredPosition); //trigger clone creation on click
    }
}
