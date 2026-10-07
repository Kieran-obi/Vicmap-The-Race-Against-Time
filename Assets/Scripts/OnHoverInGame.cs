using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OnHoverInGame : MonoBehaviour, IPointerDownHandler
{
    private Collider2D target_col;
    private GameObject target_obj;

    void Awake()
    {
        target_col = GetComponent<Collider2D>();
        target_obj = GetComponent<GameObject>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       // target_obj.
    }
}
