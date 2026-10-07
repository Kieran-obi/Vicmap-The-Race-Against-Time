using UnityEngine;
using UnityEngine.SceneManagement;

// Attack to interactable object that should load a different scene
public class SceneTransitionInteractable : InteractableObject
{
    public string targetSceneName = "SceneName";

    protected override void Interact()
    {
        Debug.Log($"[SceneTransition] {gameObject.name} triggered - would load scene: {targetSceneName}");
        SceneManager.LoadScene(targetSceneName);
    }
}
