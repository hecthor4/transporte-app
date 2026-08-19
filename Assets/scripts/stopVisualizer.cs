using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class stopVisualizer : MonoBehaviour
{
    public bool hasFoundRoute = false;
    public string lastSavedCode = "";

    public GameObject iterationParent;
    public GameObject iteration;

    public TMP_Text paradaNombre;
    public TMP_Text paradaCodigo;

    public GameObject loading;
    public TMP_Text loadingText;

    public GameObject selection;
    public GameObject selection_iterationParent;
    public GameObject selection_iteration;
    public GameObject setTimer;
    public GameObject setTimerCustomTime;
    public TMP_Dropdown customTimeDropdown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selection(string line)
    {
        selection.SetActive(true);

        //clear all
        for (int i = selection_iterationParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(selection_iterationParent.transform.GetChild(i).gameObject);
        }

        /* comprobar el lenght de "times" en la api de los buses e iterar las instanciones del objeto iterable
         * guardar los tiempos finales hasta que pase el bus
         * hacer cosas para alarmas con el tiempo seleccionado
         */

        GameObject iteration = Instantiate(selection_iteration, iterationParent.transform);
    }




    public void Reload()
    {
        for (int i = iterationParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(iterationParent.transform.GetChild(i).gameObject);
        }

        StartCoroutine(GetRealTimes("https://api.transit.welbits.com/stops/times/" + lastSavedCode, "all"));
    }

    public void Load(string code)
    {
        Debug.Log(code);

        for (int i = iterationParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(iterationParent.transform.GetChild(i).gameObject);
        }

        StartCoroutine(GetRealTimes("https://api.transit.welbits.com/stops/times/" + code, "all"));
        lastSavedCode = code;
    }


    IEnumerator GetRealTimes(string apiURL, string targetLine)
    {
        //loading.SetActive(true);
        //Coroutine anim = StartCoroutine(RealTimeTextAnim());

        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            yield return webRequest.SendWebRequest();
            //StopCoroutine(anim);

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = webRequest.downloadHandler.text;

                Response data = JsonUtility.FromJson<Response>(jsonResult);

                ProcessRealTimes(data, targetLine);
            }
            else
            {
                Debug.Log("error api: " + webRequest.error);
            }
        }
    }

    IEnumerator RealTimeTextAnim()
    {
        float time = 0.25f;

        while (hasFoundRoute == false)
        {
            loadingText.text = "Cargando...";
            yield return new WaitForSeconds(time);
            loadingText.text = "Cargando";
            yield return new WaitForSeconds(time);
            loadingText.text = "Cargando.";
            yield return new WaitForSeconds(time);
            loadingText.text = "Cargando..";
            yield return new WaitForSeconds(time);
        }
    }


    void ProcessRealTimes(Response data, string targetLine)
    {
        hasFoundRoute = false;


        if (data == null || data.routes == null || data.routes.Count == 0)
        {
            loadingText.text = "No se encuentran tiempos.";
            return;
        }

        System.Collections.Generic.List<string> formattedTimes = new System.Collections.Generic.List<string>();
        System.Collections.Generic.List<string> substractedTime = new System.Collections.Generic.List<string>();


        //si estas buscando paradas en el dialogo de busqueda de bus main
        if (targetLine == "all")
        {
            int linesToIterate = 0;
            List<string> iteratedLines = new List<string>();
            List<string> notIteratedLines = new List<string>();

            notIteratedLines.Add(data.routes.ToString());
            linesToIterate = notIteratedLines.Count;


            //buscar los tiempos de las lineas
            for (int i = 0; i < linesToIterate; i++)
            {
                foreach (BusTrip route in data.routes)
                {
                    hasFoundRoute = true;

                    formattedTimes.Clear();
                    substractedTime.Clear();

                    if (route.times != null && route.times.Count > 0)
                    {
                        foreach (ArrivalTime time in route.times)
                        {
                            if (System.DateTime.TryParse(time.arrivalDate, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out System.DateTime UTCDateTime))
                            {
                                System.DateTime localDateTime = UTCDateTime.ToLocalTime();
                                string cleanDate = localDateTime.ToString("HH:mm");
                                formattedTimes.Add(cleanDate);

                                System.TimeSpan difference = localDateTime - System.DateTime.Now;
                                int minutesRemaining = (int)difference.TotalMinutes;

                                if (minutesRemaining > 0) { substractedTime.Add(minutesRemaining + "''"); }
                                else { substractedTime.Add(">>"); }
                            }
                            else
                            {
                                Debug.Log("no se pudo parsear la linea " + targetLine);
                            }
                        }

                        GameObject lineaIterada = Instantiate(iteration, iterationParent.transform);
                        lineaIterada.transform.localScale = new Vector3(0, 0, 0);
                        lineaIterada.transform.localScale = Vector3.one;

                        Button button = lineaIterada.GetComponent<Button>();
                        button.onClick.RemoveAllListeners();
                        button.onClick.AddListener(() => Selection(route.shortName));

                        StartCoroutine(UIPosCorrector(lineaIterada));


                        TMP_Text lineaNombre = lineaIterada.transform.Find("title").GetComponent<TMP_Text>();
                        lineaNombre.text = route.shortName;

                        TMP_Text tiempoReal = lineaIterada.transform.Find("time real hh:mm").GetComponent<TMP_Text>();
                        TMP_Text tiempoRestante = lineaIterada.transform.Find("time left mins").GetComponent<TMP_Text>();

                        if (hasFoundRoute == true && formattedTimes.Count > 0)
                        {
                            tiempoReal.text = string.Join("   /   ", formattedTimes);
                            tiempoRestante.text = string.Join("   /   ", substractedTime);
                        }
                        else
                        {
                            tiempoReal.text = "No se encuentran tiempos.";
                            tiempoRestante.text = "";
                        }
                    }
                }
                //
            }



            //poner nombre de la parada y codigo
            if (data.stopCode.Contains("_")) { paradaCodigo.text = data.stopCode.Substring(2); }
            else { paradaCodigo.text = data.stopCode; }

            for (int i = 1; i < 3; i++)
            {
                string path = "buses/paradas/";
                TextAsset paradaRaw = Resources.Load<TextAsset>(path + data.routes[0].shortName.Trim() + "/dir" + i + "_pa");
                if (paradaRaw == null) { Debug.Log("l. 615. no se encuentra el archivo de paradas para el nombre de la parada de bus que se quiere buscar"); }
                string[] paradas = paradaRaw.text.Trim().Split('\n');

                TextAsset codigoRaw = Resources.Load<TextAsset>(path + data.routes[0].shortName.Trim() + "/dir" + i + "_co");
                if (codigoRaw == null) { Debug.Log("l. 619. no se encuentra el archivo de codigos para conseguir la linea exacta para el nombre de las paradas"); }
                string[] codigos = codigoRaw.text.Trim().Split('\n');

                for (int j = 0; j < codigos.Length; j++)
                {
                    //Debug.Log("iteration " + j + " / codigo: " + codigos[j].Trim() + " / codigo parada: " + data.stopCode);
                    if (codigos[j].Trim() == data.stopCode || codigos[j].Trim() == data.stopCode.Substring(2)) { paradaNombre.text = paradas[j]; break; }
                }
            }
        }
    }

    IEnumerator UIPosCorrector(GameObject gameObject)
    {
        RectTransform rt = gameObject.GetComponent<RectTransform>();

        yield return new WaitForSeconds(0.1f);

        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 101);
        yield return new WaitForSeconds(0.1f);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 100);
    }

}
