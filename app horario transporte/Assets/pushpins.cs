using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class pushpins : MonoBehaviour
{
    public TMP_Text pushpinTitle;
    public TMP_Text pushpinText;

    public GameObject mapParent;
    public Slider zoomSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick(int ID)
    {
        if (ID == 1)
        {
            pushpinTitle.text = "Retiro";
            pushpinText.text = "Retira eso";
        }
        if (ID == 2)
        {
            pushpinTitle.text = "Velilla";
            pushpinText.text = "El Patriarca de la Raya vive por ahí";
        }
        if (ID == 3)
        {
            pushpinTitle.text = "Canillejas";
            pushpinText.text = "Donde los buses y eso";
        }
        if (ID == 4)
        {
            pushpinTitle.text = "Coslada";
            pushpinText.text = "Línea 7 - pasan el 280, 282, 284...";
        }
    }

    public void OnZoomChange()
    {
        for (int i = mapParent.transform.childCount - 1; i >= 0; i--)
        {
            mapParent.transform.GetChild(i).transform.localScale = new Vector3(zoomSlider.value / 1.5f, zoomSlider.value / 1.5f, 1);
        }
    }
}
