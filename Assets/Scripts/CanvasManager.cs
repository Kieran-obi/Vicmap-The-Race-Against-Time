using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Canvas canvas;
    public static bool paused;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
        }

        if(canvas.enabled == true)
        {
            paused = true; ;
        }
        else { paused = false; }

        if(paused == true)
        {
            pauseGame();
        }
        else
        {
            resumeGame();
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        canvas.enabled = true;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        canvas.enabled = false;
    }
}
