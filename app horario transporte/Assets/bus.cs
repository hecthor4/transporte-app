using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bus : MonoBehaviour
{
    //escena
    public GameObject busParent;
    public GameObject busLineParent;


    //iterar lista
    public GameObject bus_info;
    public GameObject bus_info_emt;
    public GameObject iteratedParent;

    public TMP_InputField search;
    private List<GameObject> elementsInList = new List<GameObject>();


    //iterar paradas
    public TMP_Text busLinea;

    public GameObject ciudadPrefab;
    public TMP_Text cp_ciudadTitle;
    public TMP_Text cp_paradasText;

    public GameObject linea_info;
    public TMP_Text linea_info_direccion;
    public GameObject linea_enlace;
    public Button linea_enlace_button;

    public GameObject ciudadParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IterateList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void Search()
    {
        string searchText = search.text.ToString();

        foreach (GameObject item in elementsInList)
        {
            TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();

            bool matches = itemText.text.ToLower().Contains(searchText);
            item.SetActive(matches);
        }
    }

    void OpenLineInfo(string line)
    {
        busParent.SetActive(false);
        busLineParent.SetActive(true);

        string cleanText(string texto) => texto.Replace("emt_", "").Trim();
        if (line.Contains("_"))
        {
            busLinea.text = "EMT " + cleanText(line);
        }
        else
        {
            busLinea.text = cleanText(line);
        }
    }

    void IterateList()
    {
        //buses emt
        TextAsset todosLosNumerosDeLineas_emt = Resources.Load<TextAsset>("buses/iterar/bus11_emt");
        string[] numerosLineas_emt = todosLosNumerosDeLineas_emt.text.Trim().Split('\n');

        TextAsset todasLasParadasDeLineas_emt = Resources.Load<TextAsset>("buses/iterar/bus12_emt");
        string[] paradasLineas_emt = todasLasParadasDeLineas_emt.text.Trim().Split('\n');

        for (int i = 0; i < numerosLineas_emt.Length; i++)
        {
            GameObject iterated = Instantiate(bus_info_emt, new Vector3(0, 0, 0), Quaternion.identity, iteratedParent.transform);
            iterated.name = "emt_" + numerosLineas_emt[i];

            TMP_Text nombreLinea = iterated.transform.Find("nombre").GetComponent<TMP_Text>();
            TMP_Text infoLinea = iterated.transform.Find("info").GetComponent<TMP_Text>();

            Button button = iterated.transform.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            string lineName = iterated.name;
            button.onClick.AddListener(() => OpenLineInfo(lineName));

            nombreLinea.text = "EMT " + numerosLineas_emt[i];
            infoLinea.text = paradasLineas_emt[i];
        }



        //buses interurbanos
        TextAsset todosLosNumerosDeLineas = Resources.Load<TextAsset>("buses/iterar/bus11");
        string[] numerosLineas = todosLosNumerosDeLineas.text.Trim().Split('\n');

        TextAsset todasLasParadasDeLineas = Resources.Load<TextAsset>("buses/iterar/bus12");
        string[] paradasLineas = todasLasParadasDeLineas.text.Trim().Split('\n');

        for (int i = 0; i < numerosLineas.Length; i++)
        {
            GameObject iterated = Instantiate(bus_info, new Vector3(0, 0, 0), Quaternion.identity, iteratedParent.transform);
            iterated.name = numerosLineas[i];

            TMP_Text nombreLinea = iterated.transform.Find("nombre").GetComponent<TMP_Text>();
            TMP_Text infoLinea = iterated.transform.Find("info").GetComponent<TMP_Text>();

            Button button = iterated.transform.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            string lineName = iterated.name;
            button.onClick.AddListener(() => OpenLineInfo(lineName));

            nombreLinea.text = numerosLineas[i];
            infoLinea.text = paradasLineas[i];
        }

        //añadir a la lista para buscar
        foreach (Transform child in iteratedParent.transform)
        {
            elementsInList.Add(child.gameObject);
        }
    }

    public void Iterate(int linea)
    {
        busLinea.text = "Línea " + linea;

        TextAsset paradasEnLinea = Resources.Load<TextAsset>("buses/" + linea);
        string[] paradas = paradasEnLinea.text.Split('\n');

        //destruye todo
        for (int i = ciudadParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ciudadParent.transform.GetChild(i).gameObject);
        }

        //crea todo
        Instantiate(linea_info, new Vector3(0, 0, 0), Quaternion.identity, ciudadParent.transform);
        Instantiate(linea_enlace, new Vector3(0, 0, 0), Quaternion.identity, ciudadParent.transform);

        //var linea_info_direccion = ciudadParent.transform.Find("info").GetComponent<TMP_Text>();
        linea_info_direccion.text = paradas[0].Trim();
        linea_enlace_button.onClick.RemoveAllListeners();
        linea_enlace_button.onClick.AddListener(() => OpenLink(paradas[1].Trim()));


        for (int i = 3; i < paradas.Length; i++)
        {
            if (paradas[i].Contains("#"))
            {
                Instantiate(ciudadPrefab, new Vector3(0, 0, 0), Quaternion.identity, ciudadParent.transform);
                cp_ciudadTitle.text = paradas[i];

                for (int j = i + 1; j < paradas.Length; j++)
                {
                    if (paradas[j].Contains("#") == false)
                    {
                        break;
                    }
                    else
                    {
                        cp_paradasText.text = cp_paradasText.text + paradas[j];
                    }
                }
            }
            /*
            paradasObj[i].SetActive(true);
            paradasTexts[i].text = paradas[i].Trim();
            */
        }
    }
}
