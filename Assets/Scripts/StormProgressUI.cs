using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Reads a 0-1 progress fraction and animates the storm progress bar's
/// visuals toward it smoothly: the fill amount, and the cloud marker's
/// position.
/// </summary>
public class StormProgressUI : MonoBehaviour
{
    [Header("Visual References")]
    [SerializeField] private Image barFill;
    [SerializeField] private RectTransform cloudMarker;
    [SerializeField] private RectTransform cloudStart;
    [SerializeField] private RectTransform cloudEnd;

    [Header("Animation")]
    [SerializeField] private float glideSpeed = 0.5f;

    private float targetProgress = 0f;
    private float displayedProgress = 0f;

    private void Update()
    {
        if (Mathf.Approximately(displayedProgress, targetProgress))
        {
            return;
        }

        displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, glideSpeed * Time.deltaTime);
        ApplyProgress(displayedProgress);
    }

    public void SetProgress(float fraction)
    {
        targetProgress = Mathf.Clamp01(fraction);
    }

    private void ApplyProgress(float fraction)
    {
        barFill.fillAmount = fraction;

        cloudMarker.position = Vector3.Lerp(cloudStart.position, cloudEnd.position, fraction);
    }
}
