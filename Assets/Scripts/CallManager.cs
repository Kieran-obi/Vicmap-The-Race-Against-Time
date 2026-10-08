using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class CallManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public RectTransform dialogueBox;
    public GameObject dialoguePanel; 
    public PhoneInteractable phone;
    public List<CallData> allCalls; //drag all call assets here
    private List<CallData> currentStageQueue;
    private int currentStage = 1;
    private CallData pendingCall;

    //filters calls down to the current storm stage
    public void StartStage(int stage)
    {
        currentStageQueue = allCalls.Where(c => c.stormStage == stage).ToList();
        if (AudioManager.Instance != null) AudioManager.Instance.SetStormStage(stage);
        currentStage++;

        if (currentStageQueue.Count > 0)
            QueueNextCall();
    }

    private void QueueNextCall()
    {
        pendingCall = currentStageQueue[0];
        phone.SetRinging(true);

        // Freeze the storm clock from the moment the phone rings.
        // OnContinuePressed() releases it when the call ends.
        if (WeatherManager.Instance != null) WeatherManager.Instance.isPaused = true;

    }

    public void AnswerPhone()
    {
        phone.SetRinging(false);
        DisplayCall(pendingCall);
    }

    //gets the sentence for whatever call is active
    public string GetCurrentDialogue(CallData call) =>
        DialogueTemplates.Build(call.claimType, call.claimVars);

    //textbox 
    public void DisplayCall(CallData call)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = GetCurrentDialogue(call);
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueBox);
        if (WeatherManager.Instance != null) WeatherManager.Instance.isPaused = true;
    }
    public void OnContinuePressed()
    {
        dialoguePanel.SetActive(false);
        if (WeatherManager.Instance != null) WeatherManager.Instance.isPaused = false;
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