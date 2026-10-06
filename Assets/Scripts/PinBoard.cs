using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;

public class PinBoard : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 _distance;
    public GameObject note;
    private Collider2D coll;
    public Transform parent;
    private List<GameObject> notes = new List<GameObject>();
    private string static_note;
    private bool select;

    public void Start()
    {
        if (note != null)
        {
            notes.Add(note);
        }
        coll = GetComponent<Collider2D>();
        static_note = "Note";
        select = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (note.name != static_note)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = cursorPos - _distance;
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _distance = Camera.main.ScreenToWorldPoint(eventData.position) - (Vector3)transform.position;
        addNote();
    }

    public void selectImage()
    { 
        
    }

    public void addNote()
    {
        //create new note
        foreach (GameObject note in notes)
        {
            if (note.name == static_note)
            {

                if (notes.Count < 20)
                {
                    GameObject new_note = Instantiate(note);
                    new_note.transform.SetParent(parent);
                    for (int i = 0; i < notes.Count; i++)
                    {
                        new_note.name = $"Note{i + 1}";
                        static_note = $"Note{i}";
                        note.name = static_note;
                    }
                    coll.offset = Vector2.zero;
                    notes.Add(new_note);
                }
            }
        }
    }
}
