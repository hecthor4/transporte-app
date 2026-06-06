using TMPro;
using UnityEngine;

public class renfe : MonoBehaviour
{
    public TMP_Text renfeLinea;

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

    public string lineaNumb;
    public int paradaNumb;

    public bool isContarParadas = false;
    public bool readyToSendInfo = false;
    public GameObject selection;
    public TMP_Text selection_parada;
    public GameObject selection_corresObj;
    public TMP_Text selection_corres;
    public TMP_Text selection_contarParadas;

    public int lineaPrimera;
    public int paradaPrimera;
    public int lineaFinal;
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
        lineaNumb = "0";
        paradaNumb = 0;

        lineaPrimera = 0;
        paradaPrimera = 0;
        lineaFinal = 0;
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
        }
        /*
        else if (isContarParadas == false)
        {
            isContarParadas = true;
        }
        */
    }
    public void SaveLineaToFavs()
    {
        favorites favorites = FindFirstObjectByType<favorites>();
        favorites.SaveLinea(lineaNumb + "", "renfe");
    }

    public void Selection(int paradaNumb2)
    {
        renfeLinea.text = "Línea " + lineaNumb;

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

            lineaPrimera = int.Parse(lineaNumb);
            paradaPrimera = paradaNumb2;
        }
        else if (isContarParadas == true)
        {
            readyToSendInfo = true;

            selection_contarParadas.text = "Viajar hasta aquí";

            lineaFinal = int.Parse(lineaNumb);
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


        TMP_Text[] paradasTexts = { parada1_text, parada2_text, parada3_text, parada4_text, parada5_text, parada6_text, parada7_text, parada8_text, parada9_text, parada10_text, parada11_text, parada12_text, parada13_text, parada14_text, parada15_text, parada16_text, parada17_text, parada18_text, parada19_text, parada20_text, parada21_text, parada22_text, parada23_text, parada24_text, parada25_text, parada26_text, parada27_text, parada28_text, parada29_text, parada30_text };
        GameObject[] paradasObj = { parada1, parada2, parada3, parada4, parada5, parada6, parada7, parada8, parada9, parada10, parada11, parada12, parada13, parada14, parada15, parada16, parada17, parada18, parada19, parada20, parada21, parada22, parada23, parada24, parada25, parada26, parada27, parada28, parada29, parada30 };

        for (int i = 0; i < paradas.Length; i++)
        {
            paradasObj[i].SetActive(true);
            paradasTexts[i].text = cleanText(paradas[i].Trim());
        }
        for (int i = paradas.Length; i < 30; i++)
        {
            paradasObj[i].SetActive(false);
        }
    }
}
