using TMPro;
using UnityEngine;

public class metro : MonoBehaviour
{
    public TMP_Text metroLinea;

    public int metroButtonHeight;
    public GameObject parada1;
    public GameObject parada2;
    public GameObject parada3;
    public GameObject parada4;
    public GameObject parada5;
    public GameObject parada6;
    public GameObject parada7;
    public GameObject parada8;
    public GameObject parada9;
    public GameObject parada10;
    public GameObject parada11;
    public GameObject parada12;
    public GameObject parada13;
    public GameObject parada14;
    public GameObject parada15;
    public GameObject parada16;
    public GameObject parada17;
    public GameObject parada18;
    public GameObject parada19;
    public GameObject parada20;
    public GameObject parada21;
    public GameObject parada22;
    public GameObject parada23;
    public GameObject parada24;
    public GameObject parada25;
    public GameObject parada26;
    public GameObject parada27;
    public GameObject parada28;
    public GameObject parada29;
    public GameObject parada30;
    public GameObject parada31;
    public GameObject parada32;
    public GameObject parada33;
    public GameObject parada34;
    public GameObject parada35;
    public GameObject parada36;
    public GameObject parada37;
    public GameObject parada38;
    public GameObject parada39;
    public GameObject parada40;

    public TMP_Text parada1_text;
    public TMP_Text parada2_text;
    public TMP_Text parada3_text;
    public TMP_Text parada4_text;
    public TMP_Text parada5_text;
    public TMP_Text parada6_text;
    public TMP_Text parada7_text;
    public TMP_Text parada8_text;
    public TMP_Text parada9_text;
    public TMP_Text parada10_text;
    public TMP_Text parada11_text;
    public TMP_Text parada12_text;
    public TMP_Text parada13_text;
    public TMP_Text parada14_text;
    public TMP_Text parada15_text;
    public TMP_Text parada16_text;
    public TMP_Text parada17_text;
    public TMP_Text parada18_text;
    public TMP_Text parada19_text;
    public TMP_Text parada20_text;
    public TMP_Text parada21_text;
    public TMP_Text parada22_text;
    public TMP_Text parada23_text;
    public TMP_Text parada24_text;
    public TMP_Text parada25_text;
    public TMP_Text parada26_text;
    public TMP_Text parada27_text;
    public TMP_Text parada28_text;
    public TMP_Text parada29_text;
    public TMP_Text parada30_text;
    public TMP_Text parada31_text;
    public TMP_Text parada32_text;
    public TMP_Text parada33_text;
    public TMP_Text parada34_text;
    public TMP_Text parada35_text;
    public TMP_Text parada36_text;
    public TMP_Text parada37_text;
    public TMP_Text parada38_text;
    public TMP_Text parada39_text;
    public TMP_Text parada40_text;

    public string lineaNumb;
    public int ParadaNumb;

    public string lineaInicial;
    public int ParadaInicial;
    public string LineaFinal;
    public int ParadaFinal;

    public bool isContarParadas = false;
    public bool readyToSendInfo = false;
    public GameObject selection;
    public TMP_Text selection_parada;
    public GameObject selection_corresObj;
    public TMP_Text selection_corres;
    public TMP_Text selection_contarParadas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ContarParadasReset()
    {
        //lineaNumb = "0";
        ParadaNumb = 0;

        lineaInicial = "0";
        ParadaInicial = 0;
        LineaFinal = "0";
        ParadaFinal = 0;

        isContarParadas = false;
        readyToSendInfo = false;
    }


    public void SaveInfo()
    {
        if (isContarParadas == false)
        {
            isContarParadas = true;

            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.ShowIndicator(ParadaInicial + "", lineaInicial, "metro");
        }
        else
        {
            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.ShowIndicator(ParadaInicial + "", lineaInicial, "metro");
        }


        if (readyToSendInfo == true)
        {
            trayecto trayecto = FindFirstObjectByType<trayecto>();
            trayecto.FetchInfo(ParadaInicial, lineaInicial, ParadaFinal, LineaFinal, "metro");

            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.AllToNormal();
        }
    }
    public void SaveLineaToFavs()
    {
        favorites favorites = FindFirstObjectByType<favorites>();
        favorites.SaveLinea(lineaNumb + "", "metro");
    }


    public void Selection(int paradaNumb2)
    {
        if (!lineaNumb.Contains("r") && !lineaNumb.Contains("ml")) { metroLinea.text = "Línea " + lineaNumb; }
        else if (lineaNumb.Contains("r")) { metroLinea.text = "Ramal"; }
        else if (lineaNumb.Contains("ml")) { metroLinea.text = "Metro Ligero " + lineaNumb[2]; }

        TextAsset paradasEnLinea = Resources.Load<TextAsset>("lineas/" + lineaNumb);
        string[] paradas = paradasEnLinea.text.Split('\n');


        ParadaNumb = paradaNumb2;
        selection.SetActive(true);

        int indice = paradas[paradaNumb2 - 1].IndexOf('[');
        string parada_texto;
        string corres_texto;

        if (indice != -1)
        {
            parada_texto = paradas[paradaNumb2 - 1].Substring(0, indice).Trim();
            corres_texto = paradas[paradaNumb2 - 1].Substring(indice);

            selection_corresObj.SetActive(true);
            selection_corres.text = corres_texto;
        }
        else
        {
            parada_texto = paradas[paradaNumb2 - 1].Trim();

            selection_corres.text = "";
            selection_corresObj.SetActive(false);
        }

        selection_parada.text = parada_texto;



        if (isContarParadas == false)
        {
            selection_contarParadas.text = "Viajar desde aquí hasta...";

            lineaInicial = lineaNumb;
            ParadaInicial = paradaNumb2;
        }
        else if (isContarParadas == true)
        {
            readyToSendInfo = true;

            selection_contarParadas.text = "Viajar hasta aquí";

            LineaFinal = lineaNumb;
            ParadaFinal = paradaNumb2;
        }

        //SaveInfo();
    }

    public void Iterate(string lineaNumb1)
    {
        if (!lineaNumb1.Contains("r") && !lineaNumb1.Contains("ml")) { metroLinea.text = "Línea " + lineaNumb1; }
        else if (lineaNumb1.Contains("r")) { metroLinea.text = "Ramal"; }
        else if (lineaNumb1.Contains("ml")) { metroLinea.text = "Metro Ligero " + lineaNumb1[2]; }


        lineaNumb = lineaNumb1;

        goBack goback = FindFirstObjectByType<goBack>();
        goback.OpenScreen();

        TextAsset paradasEnLinea = Resources.Load<TextAsset>("lineas/" + lineaNumb);
        string[] paradas = paradasEnLinea.text.Split('\n');

        TMP_Text[] paradasTexts = { parada1_text, parada2_text, parada3_text, parada4_text, parada5_text, parada6_text, parada7_text, parada8_text, parada9_text, parada10_text, parada11_text, parada12_text, parada13_text, parada14_text, parada15_text, parada16_text, parada17_text, parada18_text, parada19_text, parada20_text, parada21_text, parada22_text, parada23_text, parada24_text, parada25_text, parada26_text, parada27_text, parada28_text, parada29_text, parada30_text, parada31_text, parada32_text, parada33_text, parada34_text, parada35_text, parada36_text, parada37_text, parada38_text, parada39_text, parada40_text };
        GameObject[] paradasObj = { parada1, parada2, parada3, parada4, parada5, parada6, parada7, parada8, parada9, parada10, parada11, parada12, parada13, parada14, parada15, parada16, parada17, parada18, parada19, parada20, parada21, parada22, parada23, parada24, parada25, parada26, parada27, parada28, parada29, parada30, parada31, parada32, parada33, parada34, parada35, parada36, parada37, parada38, parada39, parada40 };

        for (int i = 0; i < paradas.Length; i++)
        {
            paradasObj[i].SetActive(true);

            RectTransform rt = paradasObj[i].GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, metroButtonHeight);

            paradasTexts[i].text = paradas[i].Trim();
        }
        for (int i = paradas.Length; i < 40; i++)
        {
            paradasObj[i].SetActive(false);
        }
    }

    public void ModifyButtonHeight(int newHeight)
    {
        metroButtonHeight = newHeight;
    }
}
