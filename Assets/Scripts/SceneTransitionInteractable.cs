using UnityEngine;
using UnityEngine.SceneManagement;

// Attack to interactable object that should load a different scene
public class SceneTransitionInteractable : InteractableObject
{
    public string target_cam_name;

    protected override void Interact()
    {
        Debug.Log($"[SceneTransition] {gameObject.name} triggered - would load scene: {target_cam_name}");
        //SceneManager.LoadScene(targetSceneName);
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.setCameras(target_cam_name);
    }
}
