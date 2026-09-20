using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public Sprite gridSprite;
    public int cell_width = 10;
    public int cell_height = 10;

    [Header("CRT Camera Scenes")]
    [SerializeField] private List<string> scenes = new List<string>();
    void Start()
    {
        foreach (var scene in scenes)
        {
            if (!SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }
    }
}
