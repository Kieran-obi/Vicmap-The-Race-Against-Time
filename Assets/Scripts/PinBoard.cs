using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor;

public class PinBoard : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 _distance;
    public GameObject note;
    public Transform parent;
    public List<GameObject> notes = new List<GameObject>();
    bool selected = false;

    public void Start()
    { 
        if(note != null)
        {
            notes.Add(note);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = cursorPos - _distance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selected = true;
        //create new note
        if (notes[0])
        {
            _distance = Camera.main.ScreenToWorldPoint(eventData.position) - (Vector3)transform.position;
            if (notes.Count < 20)
            {
                GameObject new_note = GameObject.Instantiate(note);
                new_note.transform.SetParent(parent);
                notes.Add(new_note);
            }
        }
        
    }
}
