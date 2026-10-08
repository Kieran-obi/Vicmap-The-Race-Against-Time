using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;
using TMPro;

public class PinBoard : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 _distance;
    private Collider2D coll;
    public Transform parent;
    private List<GameObject> notes = new List<GameObject>();
    private string static_note;

    public GameObject note;
    private GameObject new_note;
    public TextMeshPro text_obj;
    public TMP_InputField input;
    private TextMeshPro new_text;

    public Canvas canvas;
    private GameManager gameManager;
    public void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (note != null)
        {
            notes.Add(note);
        }
        coll = GetComponent<Collider2D>();
        static_note = "Note";
      
        //text_obj = GetComponentInChildren<TextMeshPro>();
      //  if(input  != null) input.onValueChanged.AddListener(text => noteText(text_obj, text));
    }

    private void Update()
    {
        Camera this_cam = Camera.main;
        canvas.enabled = this_cam != null && this_cam.name == "Pin_MainCamera" && this_cam.isActiveAndEnabled;
    }
    /*
    public void noteText(TextMeshPro obj, string text)
    {
        if(obj != null) obj.text = text;
    }*/

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
        input.text = "";
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
                    new_note = Instantiate(note);
                    new_text = new_note.GetComponentInChildren<TextMeshPro>();

                    new_note.transform.SetParent(parent);
                    for (int i = 0; i < notes.Count; i++)
                    {
                        new_note.name = $"Note{i + 1}";
                        static_note = $"Note{i}";
                        note.name = static_note;
                        new_note.name = $"text{i}";
                        
                    }
                    new_text.text = input.text;
                    coll.offset = Vector2.zero;
                    notes.Add(new_note);
                }
            }
        }
    }
}
