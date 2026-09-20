using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class MapGrid : MonoBehaviour
{
    private static GameObject grid;
    private Area[,] cells;
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void SpawnGrid()
    {
        if (EventManager.Instance != null)
        {
            grid = new GameObject("Grid");
            grid.transform.SetParent(EventManager.Instance.transform);
            grid.AddComponent<MapGrid>();
        }
        else
        {
            Debug.Log("Error building grid instance.");
        }
    }

    private void BuildGrid()
    {
        cells = new Area[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x,y] = new Area(x,y, true);
            }
        }
    }
}
