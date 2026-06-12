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

    public void OnClick(string ID)
    {
        if (ID == "1")
        {
            pushpinTitle.text = "Retiro";
            pushpinText.text = "Retira eso";
        }
        if (ID == "2")
        {
            pushpinTitle.text = "Velilla";
            pushpinText.text = "El Patriarca de la Raya vive por ahí";
        }
        if (ID == "3")
        {
            pushpinTitle.text = "Canillejas";
            pushpinText.text = "Donde los buses y eso";
        }
        if (ID == "4")
        {
            pushpinTitle.text = "Coslada";
            pushpinText.text = "Donde el ACE.";
        }
        if (ID == "5")
        {
            pushpinTitle.text = "San Fernando";
            pushpinText.text = "Línea 7 - pasan el 280, 282, 284...";
        }
        if (ID == "6")
        {
            pushpinTitle.text = "Arganda";
            pushpinText.text = "Línea 9 - pasan el 280, 285...";
        }
        if (ID == "7")
        {
            pushpinTitle.text = "Puerta de Toledo";
            pushpinText.text = "El Rastro.";
        }


        if (ID == "r-sanfer")
        {
            pushpinTitle.text = "Renfe San Fernando";
            pushpinText.text = "Buses 285, 287.";
        }
        if (ID == "r-coslada")
        {
            pushpinTitle.text = "Renfe Coslada";
            pushpinText.text = "Buses 280, 287, 288, 289, 290.";
        }
        if (ID == "r-torrejon")
        {
            pushpinTitle.text = "Renfe Torrejón";
            pushpinText.text = "Buses 223, 224, 340, 824.";
        }
        if (ID == "r-soto")
        {
            pushpinTitle.text = "Renfe Soto del Henares";
            pushpinText.text = "Solo buses urbanos.";
        }
        if (ID == "r-garena")
        {
            pushpinTitle.text = "Renfe La Garena";
            pushpinText.text = "Solo buses urbanos.";
        }
        if (ID == "r-alcala")
        {
            pushpinTitle.text = "Renfe Alcalá";
            pushpinText.text = "Solo buses urbanos.";
        }
        if (ID == "r-universidad")
        {
            pushpinTitle.text = "Renfe Alcalá Universidad";
            pushpinText.text = "Solo buses urbanos.";
        }
        if (ID == "r-meco")
        {
            pushpinTitle.text = "Renfe Meco";
            pushpinText.text = "No sé.";
        }
        if (ID == "r-azuqueca")
        {
            pushpinTitle.text = "Renfe Azuqueca";
            pushpinText.text = "No sé.";
        }
        if (ID == "r-guadalajara")
        {
            pushpinTitle.text = "Renfe Guadalajara";
            pushpinText.text = "No sé.";
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
