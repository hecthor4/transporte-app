using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class renfe : MonoBehaviour
{
    public TMP_Text renfeLinea;

    public int renfeButtonHeight;
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

    public TMP_Text parada1_corres;
    public TMP_Text parada2_corres;
    public TMP_Text parada3_corres;
    public TMP_Text parada4_corres;
    public TMP_Text parada5_corres;
    public TMP_Text parada6_corres;
    public TMP_Text parada7_corres;
    public TMP_Text parada8_corres;
    public TMP_Text parada9_corres;
    public TMP_Text parada10_corres;
    public TMP_Text parada11_corres;
    public TMP_Text parada12_corres;
    public TMP_Text parada13_corres;
    public TMP_Text parada14_corres;
    public TMP_Text parada15_corres;
    public TMP_Text parada16_corres;
    public TMP_Text parada17_corres;
    public TMP_Text parada18_corres;
    public TMP_Text parada19_corres;
    public TMP_Text parada20_corres;
    public TMP_Text parada21_corres;
    public TMP_Text parada22_corres;
    public TMP_Text parada23_corres;
    public TMP_Text parada24_corres;
    public TMP_Text parada25_corres;
    public TMP_Text parada26_corres;
    public TMP_Text parada27_corres;
    public TMP_Text parada28_corres;
    public TMP_Text parada29_corres;
    public TMP_Text parada30_corres;

    public string lineaNumb;
    public int paradaNumb;

    public bool isContarParadas = false;
    public bool readyToSendInfo = false;
    public GameObject selection;
    public TMP_Text selection_parada;
    public GameObject selection_corresObj;
    public TMP_Text selection_corres;
    public TMP_Text selection_contarParadas;

    public string lineaPrimera;
    public int paradaPrimera;
    public string lineaFinal;
    public int paradaFinal;

    public GameObject r_all;
    public GameObject r_renfeLineas;
    public GameObject recorridoViajes;


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
        paradaNumb = 0;

        lineaPrimera = "0";
        paradaPrimera = 0;
        lineaFinal = "0";
        paradaFinal = 0;

        isContarParadas = false;
        readyToSendInfo = false;
    }

    public void SaveInfo()
    {
        if (readyToSendInfo == true)
        {
            trayecto trayecto = FindFirstObjectByType<trayecto>();
            trayecto.FetchInfo(paradaPrimera, lineaPrimera, paradaFinal, lineaFinal, "renfe");

            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.AllToNormal();
        }
        /*
        else if (isContarParadas == false)
        {
            isContarParadas = true;
        }
        */
        else if (isContarParadas == true)
        {
            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.ShowIndicator(paradaPrimera + "", "C" + lineaPrimera, "renfe");
        }
    }
    public void SaveLineaToFavs()
    {
        favorites favorites = FindFirstObjectByType<favorites>();
        favorites.SaveLinea(lineaNumb + "", "renfe");
    }

    public void Selection(int paradaNumb2)
    {
        TextAsset paradasEnLinea = Resources.Load<TextAsset>("renfe/c" + lineaNumb);
        string[] paradas = paradasEnLinea.text.Split('\n');


        paradaNumb = paradaNumb2;
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
            isContarParadas = true;

            selection_contarParadas.text = "Viajar desde aquí hasta...";

            lineaPrimera = lineaNumb;
            paradaPrimera = paradaNumb2;
        }
        else if (isContarParadas == true)
        {
            readyToSendInfo = true;

            selection_contarParadas.text = "Viajar hasta aquí";

            lineaFinal = lineaNumb;
            paradaFinal = paradaNumb2;
        }

        //SaveInfo();
    }

    public void Iterate(string lineaNumb1)
    {
        renfeLinea.text = "C" + lineaNumb1;
        lineaNumb = lineaNumb1;

        goBack goback = FindFirstObjectByType<goBack>();
        goback.OpenScreen();

        TextAsset paradasEnLinea = Resources.Load<TextAsset>("renfe/c" + lineaNumb);
        string[] paradas = paradasEnLinea.text.Split('\n');

        string cleanText(string texto) => texto.Split('[')[0].Trim();
        string getCorres(string texto) => Regex.Match(texto, @"\[.+\]").Value.Trim();

        TMP_Text[] paradasTexts = { parada1_text, parada2_text, parada3_text, parada4_text, parada5_text, parada6_text, parada7_text, parada8_text, parada9_text, parada10_text, parada11_text, parada12_text, parada13_text, parada14_text, parada15_text, parada16_text, parada17_text, parada18_text, parada19_text, parada20_text, parada21_text, parada22_text, parada23_text, parada24_text, parada25_text, parada26_text, parada27_text, parada28_text, parada29_text, parada30_text };
        TMP_Text[] paradasCorres = { parada1_corres, parada2_corres, parada3_corres, parada4_corres, parada5_corres, parada6_corres, parada7_corres, parada8_corres, parada9_corres, parada10_corres, parada11_corres, parada12_corres, parada13_corres, parada14_corres, parada15_corres, parada16_corres, parada17_corres, parada18_corres, parada19_corres, parada20_corres, parada21_corres, parada22_corres, parada23_corres, parada24_corres, parada25_corres, parada26_corres, parada27_corres, parada28_corres, parada29_corres, parada30_corres };
        GameObject[] paradasObj = { parada1, parada2, parada3, parada4, parada5, parada6, parada7, parada8, parada9, parada10, parada11, parada12, parada13, parada14, parada15, parada16, parada17, parada18, parada19, parada20, parada21, parada22, parada23, parada24, parada25, parada26, parada27, parada28, parada29, parada30 };

        for (int i = 0; i < paradas.Length; i++)
        {
            paradasObj[i].SetActive(true);

            RectTransform rt = paradasObj[i].GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, renfeButtonHeight);

            paradasTexts[i].text = cleanText(paradas[i].Trim());
            paradasCorres[i].text = getCorres(paradas[i].Trim());

            if (paradasCorres[i].text == "")
            {
                paradasCorres[i].text = "<i>Sin correspondencias</i>";
            }
        }
        for (int i = paradas.Length; i < 30; i++)
        {
            paradasObj[i].SetActive(false);
        }
    }

    public void ModifyButtonHeight(int newHeight)
    {
        renfeButtonHeight = newHeight;
    }
}
