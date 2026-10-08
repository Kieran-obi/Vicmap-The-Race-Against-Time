using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string scene;
    public void LoadGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }
}
