using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class search : MonoBehaviour
{
    public TMP_InputField searchBar;
    private List<GameObject> elementsInList = new List<GameObject>();


    public int searchButtonHeightX;
    public int searchButtonHeightY;
    public GameObject iterationChildren;
    public GameObject parentReference;


    public GameObject noResultError;


    public GridLayoutGroup botonesBusLineaParent;
    public VerticalLayoutGroup pltr_parent_layout;
    public VerticalLayoutGroup viewportContentLayout;


    public GameObject searchResult;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noResultError.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Search()
    {
        string searchText = searchBar.text.ToString();
        noResultError.SetActive(false);


        //clear all
        for (int i = parentReference.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(parentReference.transform.GetChild(i).gameObject);
        }


        if (string.IsNullOrEmpty(searchText))
        {
            //clear all
            for (int i = parentReference.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(parentReference.transform.GetChild(i).gameObject);
            }
            noResultError.SetActive(true);
        }
        else
        {
            //metro
            List<string> metroIterationOrder = new() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "r", "ml1", "ml2", "ml3" };

            for (int i = 0; i < metroIterationOrder.Count; i++)
            {
                TextAsset paradasLinea = Resources.Load<TextAsset>("lineas/" + metroIterationOrder[i]);
                string[] paradas = paradasLinea.text.Split('\n');

                for (int j = 0; j < paradas.Length; j++)
                {
                    if (paradas[j].ToLower().Contains(searchText.ToLower()))
                    {
                        //que salga lo que sa buscao
                        GameObject instantiated = Instantiate(searchResult, parentReference.transform);


                        TMP_Text linea = instantiated.transform.Find("linea numero").GetComponent<TMP_Text>();
                        TMP_Text nombre = instantiated.transform.Find("linea nombre largo").GetComponent<TMP_Text>();
                        TMP_Text parada = instantiated.transform.Find("parada encontrada").GetComponent<TMP_Text>();


                        string cleanText(string texto) => texto.Split('[')[0].Trim();

                        linea.text = metroIterationOrder[i].ToUpper();

                        if (metroIterationOrder[i].Contains("ml")) { nombre.text = "Línea de metro ligero " + linea.text[2]; }
                        else { nombre.text = "Línea de metro " + linea.text; }

                        parada.text = cleanText(paradas[j].ToString().Trim());
                    }
                }
            }


            //renfe
            List<string> renfeIterationOrder = new() { "1", "2", "3", "4", "4a", "4b", "5", "7", "8", "8a", "8b", "9", "10" };

            for (int i = 0; i < renfeIterationOrder.Count; i++)
            {
                TextAsset paradasLinea = Resources.Load<TextAsset>("renfe/c" + renfeIterationOrder[i]);
                string[] paradas = paradasLinea.text.Split('\n');

                for (int j = 0; j < paradas.Length; j++)
                {
                    if (paradas[j].ToLower().Contains(searchText.ToLower()))
                    {
                        //que salga lo que sa buscao
                        GameObject instantiated = Instantiate(searchResult, parentReference.transform);


                        TMP_Text linea = instantiated.transform.Find("linea numero").GetComponent<TMP_Text>();
                        TMP_Text nombre = instantiated.transform.Find("linea nombre largo").GetComponent<TMP_Text>();
                        TMP_Text parada = instantiated.transform.Find("parada encontrada").GetComponent<TMP_Text>();


                        string cleanText(string texto) => texto.Split('[')[0].Trim();

                        linea.text = "C" + renfeIterationOrder[i].ToUpper();

                        nombre.text = "Línea " + linea.text;

                        parada.text = cleanText(paradas[j].ToString().Trim());
                    }
                }
            }


            //bus
            List<string> busIterationOrder = new List<string>();

            TextAsset busTotalLineas = Resources.Load<TextAsset>("buses/iterar/bus11");
            string[] busLineas = busTotalLineas.text.Trim().Split('\n');

            for (int i = 0; i  < busLineas.Length; i++)
            {
                busIterationOrder.Add(busLineas[i].Trim());
            }

            for (int i = 0; i < busLineas.Length; i++)
            {
                TextAsset checkDir1 = Resources.Load<TextAsset>("buses/paradas/" + busIterationOrder[i] + "/dir1_pa");

                if (checkDir1 != null)
                {
                    SearchBus("buses/paradas/" + busIterationOrder[i] + "/dir1_pa", searchText, busIterationOrder[i], "1");
                    SearchBus("buses/paradas/" + busIterationOrder[i] + "/dir2_pa", searchText, busIterationOrder[i], "2");
                }
            }
        }
    }

    void SearchBus(string path, string searchTextParsed, string busIterationOrderIterated, string dir)
    {
        TextAsset paradasLinea = Resources.Load<TextAsset>(path);
        string[] paradas = paradasLinea.text.Split('\n');

        for (int j = 0; j < paradas.Length; j++)
        {
            if (paradas[j].ToLower().Contains(searchTextParsed.ToLower()))
            {
                //que salga lo que sa buscao
                GameObject instantiated = Instantiate(searchResult, parentReference.transform);


                TMP_Text linea = instantiated.transform.Find("linea numero").GetComponent<TMP_Text>();
                TMP_Text nombre = instantiated.transform.Find("linea nombre largo").GetComponent<TMP_Text>();
                TMP_Text parada = instantiated.transform.Find("parada encontrada").GetComponent<TMP_Text>();


                string cleanText(string texto) => texto.Split('[')[0].Trim();


                linea.text = "" + busIterationOrderIterated.ToUpper();

                nombre.text = "Línea de interurbano " + linea.text + " (dirección " + dir + ")";

                parada.text = cleanText(paradas[j].ToString().Trim());
            }
        }
    }


    public void SortBy(string type)
    {
        foreach (GameObject item in elementsInList)
        {
            //TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
            TMP_Text nombreLinea = item.transform.Find("nombre").GetComponent<TMP_Text>();

            bool matches = nombreLinea.text.ToLower().StartsWith(type);
            item.SetActive(matches);
        }

        StartCoroutine(RefreshLayoutGroups());
    }

    IEnumerator RefreshLayoutGroups()
    {
        //change
        botonesBusLineaParent.cellSize = new Vector2(499, 199);
        pltr_parent_layout.padding.left = 14;
        viewportContentLayout.padding.left = 1;

        yield return new WaitForSeconds(0.1f);

        //restore
        botonesBusLineaParent.cellSize = new Vector2(500, 200);
        pltr_parent_layout.padding.left = 15;
        viewportContentLayout.padding.left = 0;

        Debug.Log("layout groups refreshed - forced redrawn");
    }
}
