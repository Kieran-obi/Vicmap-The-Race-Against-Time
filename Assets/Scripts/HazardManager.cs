using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class HazardData //represents one daved Hazard
{
    public string type;
    public float x;
    public float y;
}

[Serializable]
public class HazardSaveFile //stores a list of all Hazards
{
    public List<HazardData> hazards = new List<HazardData>();
}

public class HazardManager : MonoBehaviour
{
    public static HazardManager Instance;

    [Serializable]
    public class HazardPrefabEntry
    {
        public string type; //must match type string used by HazardSpawner buttons
        public GameObject prefab; //draggable brick-sprite prefab for this hazard type
    }

    [Header("Setup")]
    public List<HazardPrefabEntry> prefabs;
    public RectTransform binZone; 
    public RectTransform dragLayer;

    private Dictionary<string, GameObject> prefabLookup;
    private string SavePath => Application.persistentDataPath + "/hazards.json"; //created json file

    void Awake()
    {
        Instance = this;
        prefabLookup = new Dictionary<string, GameObject>();
        foreach (var entry in prefabs)
            prefabLookup[entry.type] = entry.prefab;
    }

    void Start()
    {
        LoadHazards(); //Load any Hazards that were previously created
    }

    public GameObject SpawnHazard(string type, Vector2 anchoredPosition) //creating the hazard clones
    {
        if (!prefabLookup.ContainsKey(type))
        {
            Debug.LogWarning($"No prefab registered for hazard type '{type}'");
            return null;
        }

        GameObject clone = Instantiate(prefabLookup[type], transform);
        clone.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        HazardDraggable draggable = clone.GetComponent<HazardDraggable>();
        draggable.hazardType = type;
        draggable.binZone = binZone;
        draggable.dragLayer = dragLayer;

        SaveHazards(); //save the new state
        return clone;
    }

    // Called by HazardDraggable whenever a hazard block is moved or deleted
    public void NotifyChanged()
    {
        SaveHazards();
    }

    void SaveHazards()
    {
        HazardSaveFile file = new HazardSaveFile();
        foreach (Transform child in transform)
        {
            HazardDraggable d = child.GetComponent<HazardDraggable>();
            if (d == null) continue;
            RectTransform rt = child.GetComponent<RectTransform>();
            file.hazards.Add(new HazardData { type = d.hazardType, x = rt.anchoredPosition.x, y = rt.anchoredPosition.y });
        }
        File.WriteAllText(SavePath, JsonUtility.ToJson(file));
    }

    void LoadHazards()
    {
        if (!File.Exists(SavePath)) return;

        string json = File.ReadAllText(SavePath);
        HazardSaveFile file = JsonUtility.FromJson<HazardSaveFile>(json); //converts file into HazardSaveFile object
        if (file == null) return;

        foreach (var data in file.hazards)
        {
            if (!prefabLookup.ContainsKey(data.type)) continue;

            GameObject clone = Instantiate(prefabLookup[data.type], transform);
            clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(data.x, data.y);

            HazardDraggable draggable = clone.GetComponent<HazardDraggable>();
            draggable.hazardType = data.type;
            draggable.binZone = binZone;
            draggable.dragLayer = dragLayer;
        }
    }
}
