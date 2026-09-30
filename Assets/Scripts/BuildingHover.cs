using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class BuildingHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Identity")]
    public string buildingName;

    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite hoverSprite;

    [Header("Sizes")]
    public Vector2 normalSize = new Vector2(100f, 100f);
    public Vector2 hoverSize = new Vector2(124f, 155f);


    private Image image;
    private RectTransform rt;

    void Awake()
    {
        image = GetComponent<Image>();
        rt = GetComponent<RectTransform>();

        if (normalSprite != null) image.sprite = normalSprite;
        rt.sizeDelta = normalSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null) image.sprite = hoverSprite;
        rt.sizeDelta = hoverSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (normalSprite != null) image.sprite = normalSprite;
        rt.sizeDelta = normalSize;
    }
}
