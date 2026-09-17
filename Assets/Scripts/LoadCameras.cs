using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LoadCameras : MonoBehaviour
{
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
