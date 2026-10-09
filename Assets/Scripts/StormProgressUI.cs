using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Reads a 0-1 progress fraction and animates the storm progress bar's
/// visuals toward it smoothly: the fill amount, and the cloud marker's
/// position.
///
/// Attach this to the "StormProgressUI" object (the one grouping
/// BarBackground, BarFill and CloudMarker) - it does NOT need to be on
/// a root object, since it doesn't call DontDestroyOnLoad itself. It
/// survives automatically because its parent Canvas already does.
/// </summary>
public class StormProgressUI : MonoBehaviour
{
    [Header("Visual References")]
    [SerializeField] private Image barFill;
    [SerializeField] private RectTransform cloudMarker;
    [SerializeField] private RectTransform cloudStart;
    [SerializeField] private RectTransform cloudEnd;

    [Header("Animation")]
    // How much of the bar (as a fraction per second) the cloud glides
    // across. 0.5 means a full 0-to-1 journey takes about 2 seconds.
    [SerializeField] private float glideSpeed = 0.5f;

    // Where the bar is ultimately headed, versus what's currently shown.
    // SetProgress only ever changes the target - Update() is what
    // actually moves displayedProgress toward it, frame by frame.
    private float targetProgress = 0f;
    private float displayedProgress = 0f;

    private void Update()
    {
        // Mathf.Approximately compares floats safely, since two floats
        // that are "supposed" to be equal are sometimes off by a tiny
        // rounding error - a plain == check could stay false forever.
        if (Mathf.Approximately(displayedProgress, targetProgress))
        {
            return;
        }

        displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, glideSpeed * Time.deltaTime);
        ApplyProgress(displayedProgress);
    }

    // Public interface - StormStageTracker calls this; it doesn't need
    // to know that the result is animated instead of instant.
    public void SetProgress(float fraction)
    {
        // Clamp01 keeps the value safely between 0 and 1, in case
        // something ever passes in a number outside that range.
        targetProgress = Mathf.Clamp01(fraction);
    }

    // Snaps the bar and cloud back to the start with no glide. Called by
    // StormStageTracker when a new run begins, so a replay starts empty.
    public void ResetProgress()
    {
        targetProgress = 0f;
        displayedProgress = 0f;
        ApplyProgress(0f);
    }

    // The actual visual work, shared by every frame of the glide.
    private void ApplyProgress(float fraction)
    {
        barFill.fillAmount = fraction;

        // Blends between CloudStart and CloudEnd's actual world positions.
        // fraction = 0 -> exactly at CloudStart. fraction = 1 -> exactly
        // at CloudEnd. fraction = 0.5 -> exactly halfway between them.
        cloudMarker.position = Vector3.Lerp(cloudStart.position, cloudEnd.position, fraction);
    }
}
