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
    List<CallData> currentStageQueue;

    //filters calls down to the current storm stage
    public void StartStage(int stage)
    {
        currentStageQueue = allCalls.Where(c => c.stormStage == stage).ToList();
        AudioManager.Instance.SetStormStage(stage);
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
}