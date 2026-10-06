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
    private Collider2D target_col;
    private GameObject target_obj;

    void Awake()
    {
        target_col = GetComponent<Collider2D>();
        target_obj = GetComponent<GameObject>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // if (eventData.pressEventCamera == null || !eventData.pressEventCamera.enabled) return;
        //  if (target_obj.scene.name != "SurvRoom_Scene")
        //  {
        //     Destroy(target_col);
        
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.setCameras(target_cam_name);
        //gameManager.setGame(target_obj);
    }
}
