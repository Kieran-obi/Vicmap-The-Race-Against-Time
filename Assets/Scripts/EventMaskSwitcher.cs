using UnityEngine;
using UnityEngine.EventSystems;

public class EventMaskSwitcher : MonoBehaviour
{
    //private Camera cam;
    private Physics2DRaycaster ray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        foreach (Camera cam in gameManager.allCams)
        {
            Physics2DRaycaster ray = cam.GetComponent<Physics2DRaycaster>();
            if (cam.name == "Surv_MainCamera")
            {
                ray.eventMask = LayerMask.GetMask("SurvRoom");
                foreach(Camera other_cam in gameManager.allCams)
                {
                    if(other_cam.name == "Map_MainCamera" && !cam.enabled)
                    {
                        ray.eventMask = LayerMask.GetMask("Map");
                    }
                    else if(other_cam.name == "Highschool_MainCamera" && !cam.enabled)
                    {
                        ray.eventMask = LayerMask.GetMask("CamberwellHigh");
                    }
                }
            }
        }
        
    }
}
