using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class indicators : MonoBehaviour
{
    /*
    public GameObject main;
    public GameObject metro;
    public GameObject renfe;
    public GameObject bus;
    public GameObject mapas;
    public GameObject tutorial;
    public GameObject recorridos;
    public GameObject metro_2;
    public GameObject renfe_2;
    public GameObject bus_2;
    public GameObject mapa_madrid;
    public GameObject changelog;
    */

    public List<GameObject> all = new List<GameObject>();

    public GameObject firstParada;
    public TMP_Text fp_text;

    public GameObject noMemory;
    public TMP_Text nm_text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstParada.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Q)) { AllToNormal(); }
        if (Input.GetKeyDown(KeyCode.W)) { ShowIndicator("parada", "linea", "transporte"); }
        */
    }

    public void AllToNormal()
    {
        Debug.Log("indicator set to normal");

        firstParada.SetActive(false);
        noMemory.SetActive(false);

        for (int i = 0; i < all.Count; i++)
        {
            RectTransform rect = all[i].GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(rect.offsetMin.x, 0);
        }
    }

    public void ShowIndicator(string parada, string linea, string type)
    {
        Debug.Log("indicator set to active");

        firstParada.SetActive(true);



        string location = "";
        string location_extra = "";
        int numbersToSubstract = 1;

        if (type == "metro") { location = "lineas/"; numbersToSubstract = 1; }
        if (type == "renfe") { location = "renfe/"; numbersToSubstract = 1; } //ya meto el 'C' en 'linea'
        if (type == "bus_dir1") { location = "buses/paradas/"; location_extra = "/dir1_pa"; numbersToSubstract = 0; }
        if (type == "bus_dir2") { location = "buses/paradas/"; location_extra = "/dir2_pa"; numbersToSubstract = 0; }

        TextAsset paradasRaw = Resources.Load<TextAsset>(location + linea + location_extra);
        string[] paradasList = paradasRaw.text.Split('\n');



        string cleanText(string texto) => texto.Split('[')[0].Trim();

        fp_text.text = "Parada “" + cleanText(paradasList[int.Parse(parada) - numbersToSubstract].Trim()) + "” (línea " + linea + ") seleccionada.";

        for (int i = 0; i < all.Count; i++)
        {
            RectTransform rect = all[i].GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(rect.offsetMin.x, 150);
        }
    }


    public void NoMemoryWarning(string type)
    {
        noMemory.SetActive(true);


        if (type == "save_route")
        {
            nm_text.text = "No se ha podido guardar la ruta porque intentas exceder el máximo (5).";
        }
        if (type == "save_line")
        {
            nm_text.text = "No se ha podido guardar la línea porque intentas exceder el máximo (5).";
        }


        for (int i = 0; i < all.Count; i++)
        {
            RectTransform rect = all[i].GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(rect.offsetMin.x, 150);
        }
    }
}
