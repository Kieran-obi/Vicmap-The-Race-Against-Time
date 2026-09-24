using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static event Action OnGridChanged;
    public static event Action NewCivilianCall;

    public static EventManager Instance {  get; private set; }
    public static UnityEvent OnGameInitialised = new UnityEvent();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnGameInitialised.Invoke();
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
