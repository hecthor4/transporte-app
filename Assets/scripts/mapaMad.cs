using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class mapaMad : MonoBehaviour
{
    public RectTransform content;
    public RectTransform locator;
    public ScrollRect scroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Locate()
    {
        Canvas.ForceUpdateCanvases();

        RectTransform viewport = scroll.viewport;

        Vector2 locatorLocalPos = content.InverseTransformPoint(locator.position);
        Vector2 viewportCenter = viewport.rect.center;

        Vector2 offset = viewportCenter - locatorLocalPos;

        content.anchoredPosition += offset;
    }
}
