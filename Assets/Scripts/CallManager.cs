using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class CallManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public RectTransform dialogueBox;
    public List<CallData> allCalls; //drag all call assets here
    private List<CallData> currentStageQueue;
    private int currentStage = 1;

    //filters calls down to the current storm stage
    public void StartStage(int stage)
    {
        currentStageQueue = allCalls.Where(c => c.stormStage == stage).ToList();
        AudioManager.Instance.SetStormStage(stage);
        currentStage++;
    }

    //gets the sentence for whatever call is active
    public string GetCurrentDialogue(CallData call) =>
        DialogueTemplates.Build(call.claimType, call.claimVars);

    //textbox 
    public void DisplayCall(CallData call)
    {
        dialogueText.text = GetCurrentDialogue(call);
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueBox);
    }

    private void OnEnable()
    {
        WeatherManager.OnWeatherEventFired += HandleWeatherEventFired;
    }

    private void OnDisable()
    {
        WeatherManager.OnWeatherEventFired -= HandleWeatherEventFired;
    }

    private void HandleWeatherEventFired(WeatherEvent weatherEvent)
    {
        Debug.Log($"[CallManager] Storm stage advanced to {weatherEvent.EventName}!");
        StartStage(currentStage);

    }
}