using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class MapGrid : MonoBehaviour
{
    public static event Action OnGridChanged;
    public static event Action NewCivilianCall;

    public static void TriggerGridChange()
    {
        OnGridChanged?.Invoke();
    }

    public static void TriggerCivilianCall()
    {
        NewCivilianCall?.Invoke();
    }
}
