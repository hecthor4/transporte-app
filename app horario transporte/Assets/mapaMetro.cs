using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mapaMetro : MonoBehaviour
{
    public TMP_Text zoomNumber;
    public Slider zoomSlider;
    public RectTransform scrollContent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
