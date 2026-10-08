using UnityEngine;

public class MapSubmitButton : MonoBehaviour
{
    public void Submit()
    {
        if (CallManager.Instance == null || !CallManager.Instance.CanSubmit)
        {
            Debug.LogWarning("[MapSubmit] Nothing to submit yet.");
            return;
        }
        CallManager.Instance.OnSubmitPressed();
        GameManager.Instance.setCameras("Surv_MainCamera");
    }
}
