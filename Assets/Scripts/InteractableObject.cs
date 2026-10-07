using UnityEngine;

// Class for any object the player can walk up to and press E on
// Requires a Collider2D on this GameObject set to "Is Trigger" to define the interaction range
[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [Header("Outline")]
    [Tooltip("Child GameObject with its own white SpriteRenderer and slightly larger than parent sprite")]
    public GameObject outlineObject;

    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.E;

    protected bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        if (outlineObject != null) outlineObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        if (outlineObject != null) outlineObject.SetActive(false);
    }

    // This should be Overridden in subclasses for object-specific behaviour
    protected virtual void Interact()
    {
        Debug.Log($"{gameObject.name} interacted with (no behaviour assigned yet).");
    }
}
