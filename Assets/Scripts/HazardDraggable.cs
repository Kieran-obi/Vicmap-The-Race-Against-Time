using UnityEngine;
using UnityEngine.EventSystems;

// Handles clones dragging around the map and deletion when dropped on the bin
public class HazardDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public string hazardType;
    [HideInInspector] public RectTransform binZone;

    public RectTransform dragLayer; //for bin layering issue
    
    private RectTransform rt;
    private RectTransform parentRect;
    private Transform homeParent; //where the hazard lives when NOT being dragged

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        parentRect = transform.parent.GetComponent<RectTransform>();
        homeParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Sound Effect Implication

        //keep size so it's not resizing during dragging
        rt.SetParent(dragLayer, true);
        rt.SetAsLastSibling();
        parentRect = dragLayer;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        //convert mouse position to a position relative to the map
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            rt.anchoredPosition = localPoint; //move the hazard
        }
    }

    // Only runs on actual mouse up after drap
    // Never runs on hovering or because the bin happens to pass under icon
    public void OnEndDrag(PointerEventData eventData)
    {
        bool droppedOnBin = binZone != null && RectTransformUtility.RectangleContainsScreenPoint(
                binZone, eventData.position, eventData.pressEventCamera);
        
        if (droppedOnBin)
        {
            transform.SetParent(null); //detach first so save doesn't include this object

            HazardManager.Instance.NotifyChanged();
            Destroy(gameObject);
            return;
        }
        //not dropped on bin = return to map to sit behind the bin
        rt.SetParent(homeParent, true);
        parentRect = homeParent.GetComponent<RectTransform>();
        
        HazardManager.Instance.NotifyChanged(); //save new pos
    }
}
