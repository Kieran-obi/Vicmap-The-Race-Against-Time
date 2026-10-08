using UnityEngine;
using UnityEngine.EventSystems;

public class EventMaskSwitcher : MonoBehaviour
{
    private Physics2DRaycaster ray;
    private Physics2DRaycaster ray2;
    private Physics2DRaycaster ray3;

    void Update()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        foreach (Camera cam in gameManager.allCams)
        {
            ray = cam.GetComponent<Physics2DRaycaster>();
            if (cam.name == "Surv_MainCamera")
            {
                if (cam.enabled)
                {
                    if (ray != null) ray.enabled = true;
                    if (ray2 != null) ray2.enabled = false;
                    if (ray3 != null) ray3.enabled = false;
                }
                ray.eventMask = LayerMask.GetMask("SurvRoom");
                foreach(Camera other_cam in gameManager.allCams)
                {
                    if(other_cam.name == "Map_MainCamera" && !cam.enabled && other_cam.enabled)
                    {
                        ray.eventMask = LayerMask.GetMask("Map");
                    }
                    else if (other_cam.name == "Highschool_MainCamera" && !cam.enabled && other_cam.enabled)
                    {
                        ray.eventMask = LayerMask.GetMask("CamberwellHigh");
                    }
                    else if (other_cam.name == "KewAmbo_MainCamera" && !cam.enabled && other_cam.enabled)
                    {
                        ray.eventMask = LayerMask.GetMask("KewAmbo");
                    }
                    else if (other_cam.name == "Pin_MainCamera" && !cam.enabled && other_cam.enabled)
                    {
                        ray.eventMask = LayerMask.GetMask("PinBoard");
                        ray3 = other_cam.GetComponent<Physics2DRaycaster>();
                        if(ray != null )ray.enabled = false;
                        if (ray2 != null) ray2.enabled = false;
                        if (ray3 != null) ray3.enabled = true;
                    }
                    else if (other_cam.name == "Cams_MainCamera" && !cam.enabled && other_cam.enabled)
                    {
                        ray.eventMask = LayerMask.GetMask("CameraDesk");
                        ray2 = other_cam.GetComponent<Physics2DRaycaster>();
                        if (ray != null) ray.enabled = false;
                        if (ray2 != null) ray2.enabled = true;
                        if (ray3 != null) ray3.enabled = false;
                    }
                }
            }
        }
        
    }
}
