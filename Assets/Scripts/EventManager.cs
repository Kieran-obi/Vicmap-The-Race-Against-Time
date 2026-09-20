using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static event Action OnGridChanged;
    public static event Action NewCivilianCall;

    public static EventManager Instance {  get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public static void TriggerGridChange()
    {
        OnGridChanged?.Invoke();
    }

    public static void TriggerCivilianCall()
    {
        NewCivilianCall?.Invoke();
    }
}
