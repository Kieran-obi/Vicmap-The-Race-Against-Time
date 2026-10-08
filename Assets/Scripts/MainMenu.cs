using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string play_scene;
    public string settings_scene;
    public string menu_scene;
    public void LoadGame()
    {
        SceneManager.LoadScene(play_scene);
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene(settings_scene);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menu_scene);
    }


    public void QuitPressed()
    {
        Application.Quit();
    }
}
