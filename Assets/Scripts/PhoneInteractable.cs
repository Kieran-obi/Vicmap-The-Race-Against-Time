using UnityEngine;
// using UnityEngine.SceneManagement
public class PhoneInteractable : InteractableObject
{
    [Header("Phone Sprites")]
    public SpriteRenderer phoneRenderer;
    public Sprite offSprite;
    public Sprite callSprite;

    [Header("Scene")]
    public string targetSceneName = "CallScene";

    [HideInInspector] public bool isRinging = false;

    public CallManager callManager; 

    void Start()
    {
        if (phoneRenderer != null && offSprite != null)
            phoneRenderer.sprite = offSprite;
    }


    // Call this from CallManager when a call comes in
    public void SetRinging(bool ringing)
    {
        isRinging = ringing;
        if (phoneRenderer == null) return;
        phoneRenderer.sprite = ringing ? callSprite : offSprite;
        if (AudioManager.Instance != null)
        {
            if (ringing) AudioManager.Instance.StartRinging();
            else AudioManager.Instance.StopRinging();
        }
    }

    protected override void Interact()
    {
        if (!isRinging)
        {
            Debug.Log("[Phone] Nothing to answer right now.");
            return;
        }
        callManager.AnswerPhone();
    }
}
