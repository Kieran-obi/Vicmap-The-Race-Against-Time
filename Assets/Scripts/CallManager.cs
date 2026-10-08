using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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
    private CallData currentCall;
    private bool awaitingSubmit = false;

    [Header("Resolution")]
    public float tolerance = 130f;
    public int correctCount = 0;
    public int mistakeCount = 0;

    [Header("End Screen")]
    public GameObject endPanel;
    public TMP_Text endText;
    public int totalStages = 3;
    public int maxMistakes = 3;

    public static CallManager Instance;
    public bool CanSubmit => awaitingSubmit;

    private void Awake() => Instance = this;

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
        if (phone == null)
        {
            Debug.LogError("[CallManager] Phone slot is empty.", this);
            return;
        }
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
        currentCall = call;
        awaitingSubmit = false;
        dialoguePanel.SetActive(true);
        dialogueText.text = GetCurrentDialogue(call);
        LayoutRebuilder.ForceRebuildLayoutImmediate(dialogueBox);
        if (WeatherManager.Instance != null) WeatherManager.Instance.isPaused = true;
    }
    public void OnContinuePressed()
    {
        dialoguePanel.SetActive(false);
        awaitingSubmit = true; //storm stays paused until player presses submit

    }

    public void OnSubmitPressed()
    {
        if (!awaitingSubmit) return;
        awaitingSubmit = false;

        ResolveCall(currentCall);
        currentStageQueue.Remove(currentCall);

        if (mistakeCount >= maxMistakes) { ShowEndScreen(true); return; }

        if (currentStageQueue.Count > 0)
            QueueNextCall();
        else if (currentStage > totalStages)   
            ShowEndScreen(false);
        else if (WeatherManager.Instance != null)
            WeatherManager.Instance.isPaused = false;
    }

    private void ResolveCall(CallData call)
    {
        var hazards = HazardManager.Instance.GetCurrentHazards();
        Vector2 pos = call.actualLocation.mapAnchoredPosition;
        Debug.Log($"[Resolve] {call.name} expects '{call.ExpectedHazardType()}' at {pos} (location: {call.actualLocation.name}), tolerance {tolerance}, hazards seen: {hazards.Count}");
        foreach (var h in hazards)
        Debug.Log($"[Resolve]   '{h.type}' at ({h.x:F0}, {h.y:F0}), distance {Vector2.Distance(new Vector2(h.x, h.y), pos):F0}");
        bool NearLocation(HazardData h) => Vector2.Distance(new Vector2(h.x, h.y), pos) < tolerance;

        string expected = call.ExpectedHazardType();
        bool correct;

        if (expected != null)
            correct = hazards.Any(h => h.type == expected && NearLocation(h));
        else if (call.claimType == CallClaimType.Clear)
            correct = !hazards.Any(NearLocation); //hazard placed where nothing's wrong = false alarm
        else
            return; //due to scope and time, WrongLocation/PeopleTrapped/Uncertain arent scored yet

        results.Add(new CallResult { name = call.name, correct = correct, issue = call.issueType });

        if (correct) correctCount++; else mistakeCount++;
        Debug.Log($"[CallManager] {call.name}: {(correct ? "correct" : "mistake")} (correct {correctCount}, mistakes {mistakeCount})");
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
    private class CallResult
    {
        public string name;
        public bool correct;
        public IssueType issue;
    }
    private List<CallResult> results = new List<CallResult>();

    private void ShowEndScreen(bool failed)
    {
        if (WeatherManager.Instance != null) WeatherManager.Instance.isPaused = true;

        var sb = new System.Text.StringBuilder();
        sb.AppendLine(failed ? "Too many mistakes. The response broke down." : "The storm has passed.");
        sb.AppendLine($"Correct: {correctCount}    Mistakes: {mistakeCount}\n");

        foreach (var r in results)
            sb.AppendLine($"{(r.correct ? "Correct" : "Missed")} - {r.name}: {Lesson(r)}");

        sb.AppendLine("\nWhen time matters, knowing what is where, and trusting that information, makes the difference.");

        endText.text = sb.ToString();
        endPanel.SetActive(true);
    }

    private string Lesson(CallResult r)
    {
        if (r.correct) return "Handled correctly.";
        return r.issue switch
        {
            IssueType.Mislocated     => "The reported location didn't match the real one. Check it against another source first.",
            IssueType.Outdated       => "The map information was out of date.",
            IssueType.Missing        => "A feature was missing from the map.",
            IssueType.Misclassified  => "A place was listed as the wrong type.",
            _                        => "The report was accurate, but the response didn't match it."
        };
    }

    public void OnMainMenuPressed() => SceneManager.LoadScene(0);
}