using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCameras : MonoBehaviour
{
    [Header("CRT Camera Scenes")]
    [SerializeField] private string scene = "";
    void Start()
    {
        if(!SceneManager.GetSceneByName(scene).isLoaded)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }
    }

}
