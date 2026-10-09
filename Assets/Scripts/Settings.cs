using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Dropdown opt;
    public void SetResolution(int index)
    {
        switch(index)
        {
            case 0: 
                Screen.SetResolution(3840, 2160, true); //4K
                break;
            case 1:
                Screen.SetResolution(1920, 1080, true); //HD
                break;
            case 2:
                Screen.SetResolution(1366, 768, true); //WXGA
                break;
        }
       
    }
}
