using UnityEngine;
using UnityEngine.EventSystems;

// Listens for drag and scroll events and moves/scales
[RequireComponent(typeof(RectTransform))]
public class MapPanZoom : MonoBehaviour, IDragHandler, IBeginDragHandler, IScrollHandler
{
    [Header("References")]
    public RectTransform mapContainer;
    public Canvas parentCanvas;

    [Header("Zoom Settings")]
    public float minZoom = 1f;
    public float maxZoom = 3f;
    public float zoomSpeed = 0.1f;

    private RectTransform viewportRect;
    private Vector2 lastDragScreenPos;

    void Awake()
    {
        viewportRect = GetComponent<RectTransform>();
        if (parentCanvas == null)
            parentCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragScreenPos = eventData.position; //get the x,y of where the mouse started
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - lastDragScreenPos; //calculate how far you've dragged
        lastDragScreenPos = eventData.position;

        float scaleFactor = parentCanvas != null ? parentCanvas.scaleFactor : 1f; //use the Canvas scale factor if applicable, otherwise set to 1
        mapContainer.anchoredPosition += delta / scaleFactor; //actually move the map

        ClampPosition();
    }

    public void OnScroll(PointerEventData eventData)
    {
        float currentScale = mapContainer.localScale.x;  //get current zoom
        float newScale = Mathf.Clamp(currentScale + eventData.scrollDelta.y * zoomSpeed, minZoom, maxZoom); //calculate new zoom
        if (Mathf.Approximately(currentScale, newScale)) return; //check if zoom changed

        // Zoom towards the cursor so point under mouse stays fixed
        Vector2 localPointBefore;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mapContainer, eventData.position, eventData.pressEventCamera, out localPointBefore); //convert screen pos to local pos before zoom
        
        mapContainer.localScale = new Vector3(newScale, newScale, 1f); //change map scale

        Vector2 localPointAfter; //get mouse pos again
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mapContainer, eventData.position, eventData.pressEventCamera, out localPointAfter); //after zoom

        Vector2 correction = (localPointAfter - localPointBefore) * newScale; //calculate how much map needs to move 
        mapContainer.anchoredPosition += correction; //move the map

        ClampPosition();
    }
    // Stop player from dragging map so far empty space shows
    void ClampPosition()
    {
        float scale = mapContainer.localScale.x; //get map scale
        Vector2 mapSize = mapContainer.rect.size * scale; //calculate map size
        Vector2 viewSize = viewportRect.rect.size; //get size of visible area

        float maxX = Mathf.Max(0, (mapSize.x - viewSize.x) / 2f); //calculate how far you can move horizontally
        float maxY = Mathf.Max(0, (mapSize.y - viewSize.y) / 2f); //vertically

        Vector2 pos = mapContainer.anchoredPosition; //get current pos
        pos.x = Mathf.Clamp(pos.x, -maxX, maxX); //prevent past X
        pos.y = Mathf.Clamp(pos.y, -maxY, maxY); //prevent past Y
        mapContainer.anchoredPosition = pos; //apply back to the map
    }
}

