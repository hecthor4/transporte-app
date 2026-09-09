using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mapaMetro : MonoBehaviour
{
    public TMP_Text zoomNumber;
    public Slider zoomSlider;
    public RectTransform scrollContent;

    public bool isZooming = false;

    private float minZoom = 0.25f;
    private float maxZoom = 5.5f;
    private float zoomSpeed = 0.0025f;
    private bool isActive;

    private Vector2 pivotInViewport;
    private Vector3 localPointBeforeInContent;

    public RectTransform contentRect;
    public RectTransform viewportRect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeInHierarchy) return;

        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            contentRect.localScale += new Vector3(0.5f, 0.5f, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            contentRect.localScale -= new Vector3(0.5f, 0.5f, 0);
        }
        */

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began || !isZooming)
            {
                isZooming = true;

                Vector2 touchPointScreen = (touchZero.position + touchOne.position) / 2f;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(viewportRect, touchPointScreen, null, out pivotInViewport);

                Vector3 worldPointBeforeZoom = viewportRect.TransformPoint(pivotInViewport);
                localPointBeforeInContent = contentRect.InverseTransformPoint(worldPointBeforeZoom);
                return;
            }

            Vector2 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrev - touchOnePrev).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagDiff = touchDeltaMag - prevTouchDeltaMag;

            float currentScale = contentRect.localScale.x;
            float newScale = currentScale + (deltaMagDiff * zoomSpeed);
            newScale = Mathf.Clamp(newScale, minZoom, maxZoom);
            contentRect.localScale = new Vector3(newScale, newScale, 1f);

            Vector3 worldPointAfterZoom = viewportRect.TransformPoint(pivotInViewport);
            Vector3 localPointAfterInContent = contentRect.InverseTransformPoint(worldPointAfterZoom);

            Vector3 deltaPivot = localPointAfterInContent - localPointBeforeInContent;
            contentRect.localPosition += contentRect.TransformDirection(deltaPivot);
        }
        else
        {
            isZooming = false;
        }
    }
    public void OnZoomSliderChange()
    {
        /*
        mapa280.transform.localScale = new Vector3(zoomSlider.value, zoomSlider.value, zoomSlider.value);
        mapa282.transform.localScale = new Vector3(zoomSlider.value, zoomSlider.value, zoomSlider.value);
        mapa284.transform.localScale = new Vector3(zoomSlider.value, zoomSlider.value, zoomSlider.value);
        mapa285.transform.localScale = new Vector3(zoomSlider.value, zoomSlider.value, zoomSlider.value);
        mapa341.transform.localScale = new Vector3(zoomSlider.value, zoomSlider.value, zoomSlider.value);
        mapaTodo.transform.localScale = new Vector3(zoomSlider.value, zoomSlider.value, zoomSlider.value);
        */

        scrollContent.localScale = new Vector3(zoomSlider.value, zoomSlider.value, zoomSlider.value);
        zoomNumber.text = Mathf.RoundToInt(zoomSlider.value) + "";
    }
}
