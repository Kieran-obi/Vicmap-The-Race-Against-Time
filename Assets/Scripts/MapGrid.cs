using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class MapGrid : MonoBehaviour
{
    private static GameObject grid;
    private Area[,] cells;
    

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void SpawnGrid()
    {
        if (EventManager.Instance != null)
        {
            grid = new GameObject("Grid");
            grid.transform.SetParent(EventManager.Instance.transform);
            MapGrid map = grid.AddComponent<MapGrid>();
            EventManager.OnGameInitialised.AddListener(map.BuildGrid);
        }
        else
        {
            Debug.Log("Error building grid instance.");
        }
    }

    private void BuildGrid()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        int cell_width = gameManager.cell_width;
        int cell_height = gameManager.cell_height;
        cells = new Area[cell_width, cell_height];
        float screenPosX = cell_width / 2f;
        float screenPosY = cell_height / 2f;

        for (int x = 0; x < cell_width; x++)
        {
            for (int y = 0; y < cell_height; y++)
            {
                cells[x,y] = new Area(x,y, true);
                GameObject gridTile = new GameObject($"Cell_{x}_{y}");
                gridTile.transform.SetParent(this.transform);
                gridTile.transform.localPosition = new Vector3((x - screenPosX) * 1f, (y - screenPosY) * 1f, 0);
                SpriteRenderer renderer = gridTile.AddComponent<SpriteRenderer>();
                renderer.sprite = FindAnyObjectByType<GameManager>().gridSprite;
                renderer.color = Color.white;
            }
        }
    }

    private void OnDestroy()
    {
        if(EventManager.Instance != null)
        {
            EventManager.OnGameInitialised.RemoveListener(BuildGrid);
        }
    }
}
