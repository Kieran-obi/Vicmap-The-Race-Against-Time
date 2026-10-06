using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour, IPointerDownHandler
{
    public string target_cam_name;
    public string target_obj;
    //public string target_scene;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pressEventCamera == null || !eventData.pressEventCamera.enabled) return;

        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.setCameras(target_cam_name);
        //gameManager.setGame(target_obj);
    }
}
