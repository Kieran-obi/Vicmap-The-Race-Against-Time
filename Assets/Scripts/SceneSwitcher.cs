using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour, IPointerDownHandler
{
    public Camera this_cam;
    public string this_cam_name;
    public GameObject this_game;
    [SerializeField] public string this_scene;
    public void Awake()
    {
        this_cam = GetComponent<Camera>();
        /*if(GameManager.Instance != null )
        {
            GameManager.Instance.findCameras(this_cam);
        } */   
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        /* //string prev_scene = SceneManager.GetActiveScene().name;

         //Scene current_scene = SceneManager.GetSceneByName(this_scene);
         //SceneManager.LoadScene(this_scene, LoadSceneMode.Additive);
         gameManager.allCams = Resources.FindObjectsOfTypeAll<Camera>();
         foreach (Camera cam in gameManager.allCams)
         {
             if(cam.gameObject.scene.name != null && cam.name == this_cam_name)
             {
                 this_cam = cam;
             }
         }

         if(this_cam != null)
         {

         }

         //SceneManager.SetActiveScene(current_scene);
        */
        gameManager.setCameras(this_cam_name);
        gameManager.setGame(this_game);

    }

}
