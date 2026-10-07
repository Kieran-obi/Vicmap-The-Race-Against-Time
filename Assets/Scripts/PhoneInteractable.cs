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
    }

    protected override void Interact()
    {
        if (!isRinging)
        {
            Debug.Log("[Phone] Nothing to answer right now.");
            return;
        }

        Debug.Log($"[Phone] Answering call - would load scene: {targetSceneName}");
        SetRinging(false);
        // SceneManager.LoadScene(targetSceneName);
    }
}
