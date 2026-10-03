using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] game_parent;
    [Header("Grid Settings")]
    public Sprite gridSprite;
    public int cell_width = 10;
    public int cell_height = 10;

    [Header("Active Cameras")]
    //public List<Camera> allCams = new List<Camera>();
    public Camera[] allCams;
    public Camera default_cam;

    [Header("CRT Camera Scenes")]
    [SerializeField] private List<string> scenes_name = new List<string>();
    

    [SerializeField] private List<Scene> allScenes = new List<Scene>();

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        SceneManager.sceneLoaded += setGame;
        SceneManager.sceneLoaded += setDefaultCams;
    }
    void Start()
    {
        
        foreach (var name in scenes_name)
        {
            if (!SceneManager.GetSceneByName(name).isLoaded)
            {
                SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            }
        }
        
    }

    private void setGame(Scene scene, LoadSceneMode mode)
    {
        //game = GameObject.Find("Game");
        game_parent = GameObject.FindGameObjectsWithTag("Game");

        /* if (game.name == "Game")
         {
             game.SetActive(false);
         }*/

        for (int i = 0; i < SceneManager.sceneCount; i++) 
        { 
            Scene s = SceneManager.GetSceneAt(i);
            allScenes.Add(s);
        }

        foreach (var game in game_parent)
        {
            
                if (game.scene.name == "SurvRoom_Scene")
                {
                    game.SetActive(true);
                }
                else if (game.scene.name == "CamberwellHigh")
                {
                    game.SetActive(false);
                }
                else if (game.scene.name == "camera_testing2")
                {
                    game.SetActive(false);
                }
                else if (game.scene.name == "MapTest")
                {
                    game.SetActive(false);
                }

        }
        
    }

    private void setDefaultCams(Scene scene, LoadSceneMode mode)
    {
        allCams = Resources.FindObjectsOfTypeAll<Camera>();
        foreach (var cam in allCams)
        {
            if (cam.name == "Surv_MainCamera")
            {
                default_cam = cam;
                default_cam.enabled = true;
            }
            if (cam.name == "Highschool_MainCamera")
            {
                cam.enabled = false;
            }
            if (cam.name == "Test_MainCamera")
            {
                cam.enabled = false;
            }
            if (cam.name == "Map_MainCamera")
            {
                cam.enabled = false;
            }
        }
    }

    public Scene getScene(string name)
    {
        Scene current_scene = SceneManager.GetSceneByName(name);
        return current_scene;
    }

    public void setGame(GameObject obj)
    {
        foreach (var game in game_parent)
        {
            if (game == obj)
            {
                game.SetActive(false);
            }
            else
            {
                game.SetActive(true);
            }
        }
    }

    public void setCameras(string name)
    {
        foreach (var camera in allCams)
        {
            if (camera.name == name)
            { 
                camera.enabled = true;
            }
            else
            {
                camera.enabled = false;
            }
        }
    }
}
