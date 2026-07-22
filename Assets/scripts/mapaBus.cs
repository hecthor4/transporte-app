using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class mapaBus : MonoBehaviour
{
    public TMP_Text busNumber;
    public Slider slider;

    public GameObject mapa280;
    public GameObject mapa282;
    public GameObject mapa284;
    public GameObject mapa285;
    public GameObject mapa341;
    public GameObject mapaTodo;


    public TMP_Text zoomNumber;
    public Slider zoomSlider;
    public RectTransform scrollContent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideEverything();
        mapa280.SetActive(true);
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

    public void OnSliderChange()
    {
        if (slider.value == 1)
        {
            HideEverything();
            busNumber.text = "280";
            mapa280.SetActive(true);
        }
        if (slider.value == 2)
        {
            HideEverything();
            busNumber.text = "282";
            mapa282.SetActive(true);
        }
        if (slider.value == 3)
        {
            HideEverything();
            busNumber.text = "284";
            mapa284.SetActive(true);
        }
        if (slider.value == 4)
        {
            HideEverything();
            busNumber.text = "285";
            mapa285.SetActive(true);
        }
        if (slider.value == 5)
        {
            HideEverything();
            busNumber.text = "341";
            mapa341.SetActive(true);
        }
        if (slider.value == 6)
        {
            HideEverything();
            busNumber.text = "todo";
            mapaTodo.SetActive(true);
        }
    }

    void HideEverything()
    {
        mapa280.SetActive(false);
        mapa282.SetActive(false);
        mapa284.SetActive(false);
        mapa285.SetActive(false);
        mapa341.SetActive(false);
    }
}
