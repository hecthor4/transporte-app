using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mapaMetro : MonoBehaviour
{
    public TMP_Text zoomNumber;
    public Slider zoomSlider;
    public RectTransform scrollContent;

    private float minZoom = 0.25f;
    private float maxZoom = 4;
    private float zoomSpeed = 0.0025f;
    private bool isActive;

    public RectTransform rect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy == true)
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrev - touchOnePrev).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagDiff = touchDeltaMag - prevTouchDeltaMag;


                float newScaleX = rect.localScale.x + (deltaMagDiff * zoomSpeed);
                newScaleX = Mathf.Clamp(newScaleX, minZoom, maxZoom);
                rect.localScale = new Vector3(newScaleX, newScaleX, 1);
            }
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
