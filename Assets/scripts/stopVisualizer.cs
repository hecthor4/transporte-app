using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class stopVisualizer : MonoBehaviour
{
    public string mode;
    public GameObject visorDeParadas;

    public GameObject realtimeScrollview;
    public GameObject mapScrollview;

    public GameObject apiError;
    public GameObject apiErrorMap;

    public bool hasFoundRoute = false;
    public string lastSavedCode = ""; //sirve para las dos
    public string lastSavedCorp = ""; //empresa en pos

    public GameObject rt_iterationParent;
    public GameObject rt_iteration;

    public GameObject ps_iterationParent;
    public RectTransform ps_iterationParentRect;
    public RectTransform ps_content;
    public GameObject ps_iteration;
    public ScrollRect ps_scroll;

    public TMP_Text paradaNombre;
    public TMP_Text paradaCodigo;

    public GameObject loading;
    public TMP_Text loadingText;

    public ResponseRT rt_jsonData;

    public GameObject selection;
    public TMP_Text selection_lineName;
    public GameObject selection_iterationParent;
    public GameObject selection_iteration;
    public GameObject setTimer;
    public GameObject setTimerCustomTime;
    public TMP_Dropdown customTimeDropdown;

    public bool hasSetAlarm = false;


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
        foreach (BusTripRT route in rt_jsonData.routes)
        {
            if (route.shortName == line)
            {
                selection_lineName.text = route.shortName;

                for (int i = 0; i < route.times.Count; i++)
                {
                    ArrivalTimeRT time = route.times[i];
                    if (DateTime.TryParse(time.arrivalDate, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime utcTime))
                    {
                        DateTime localtime = utcTime.ToLocalTime();

                        GameObject iteration = Instantiate(selection_iteration, selection_iterationParent.transform);

                        TMP_Text nextBusTime = iteration.transform.Find("siguiente bus hh:mm").GetComponent<TMP_Text>();
                        TMP_Text nextBusTimeLeft = iteration.transform.Find("siguiente bus time left").GetComponent<TMP_Text>();

                        nextBusTime.text = localtime.ToString("HH:mm");
                        nextBusTimeLeft.text = time.arrivalDate;

                        System.TimeSpan difference = localtime - System.DateTime.Now;
                        int minutesRemaining = (int)difference.TotalMinutes;
                        if (minutesRemaining > 0) { nextBusTimeLeft.text = minutesRemaining + "'"; }
                        else { nextBusTimeLeft.text = ">>"; }

                        Button button = iteration.GetComponent<Button>();
                        button.onClick.RemoveAllListeners();
                        button.onClick.AddListener(() => { SetAlertForBus(route.shortName, time.arrivalDate); });
                    }
                }
            }
        }
    }

    public void SetAlertForBus(string line, string arrivalDate)
    {
        StartCoroutine(SetAlertForBusCoroutine(line, arrivalDate));

        setTimer.SetActive(true);
        selection.SetActive(false);
    }

    public void SetAlarm()
    {
        hasSetAlarm = true;
    }

    IEnumerator SetAlertForBusCoroutine(string line, string arrivalDate)
    {
        hasSetAlarm = false;
        float minutesOff = 0;

        while (hasSetAlarm == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        string customTimeDropdownSelect = customTimeDropdown.options[customTimeDropdown.value].text;
        if (customTimeDropdownSelect == "30''") { minutesOff = 0.30f; }
        else if (customTimeDropdownSelect.Contains("'")) { minutesOff = float.Parse(customTimeDropdownSelect.Replace("'", "")); }

        notifications notifications = FindFirstObjectByType<notifications>();

        if (DateTime.TryParse(arrivalDate, out DateTime utcTime))
        {
            DateTime localtime = utcTime.ToLocalTime();
            TimeSpan minutes = localtime - DateTime.Now;
            notifications.Create("canal_aviso_autobuses", (line + paradaNombre.text), (float)(minutes.TotalMinutes) - minutesOff);

            timers timers = FindFirstObjectByType<timers>();
            timers.CreateTimer((float)(minutes.TotalMinutes) - minutesOff, line, paradaNombre.text);
        }
    }




    public void Reload()
    {
        if (mode == "rt")
        {
            for (int i = rt_iterationParent.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(rt_iterationParent.transform.GetChild(i).gameObject);
            }

            StartCoroutine(GetRealTimes("https://api.transit.welbits.com/stops/times/" + lastSavedCode, "all"));
        }
        if (mode == "ps")
        {
            StartCoroutine(GetPositions("https://apisvt.avanzagrupo.com/lineas/getTrayectos?empresa=" + lastSavedCorp + "&linea=" + lastSavedCode + "&sentido=V", lastSavedCode, "0"));
        }
    }

    public void Load(string code)
    {
        mode = "rt"; //realtimes

        visorDeParadas.SetActive(true);
        realtimeScrollview.SetActive(true);
        mapScrollview.SetActive(false);

        Debug.Log(code);

        for (int i = rt_iterationParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(rt_iterationParent.transform.GetChild(i).gameObject);
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

                ResponseRT data = JsonUtility.FromJson<ResponseRT>(jsonResult);

                rt_jsonData = data;

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


    void ProcessRealTimes(ResponseRT data, string targetLine)
    {
        hasFoundRoute = false;


        if (data == null || data.routes == null || data.routes.Count == 0)
        {
            loadingText.text = "No se encuentran tiempos.";
            apiError.SetActive(true);
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
                foreach (BusTripRT route in data.routes)
                {
                    hasFoundRoute = true;

                    formattedTimes.Clear();
                    substractedTime.Clear();

                    if (route.times != null && route.times.Count > 0)
                    {
                        foreach (ArrivalTimeRT time in route.times)
                        {
                            if (System.DateTime.TryParse(time.arrivalDate, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out System.DateTime UTCDateTime))
                            {
                                System.DateTime localDateTime = UTCDateTime.ToLocalTime();
                                string cleanDate = localDateTime.ToString("HH:mm");
                                formattedTimes.Add(cleanDate);

                                System.TimeSpan difference = localDateTime - System.DateTime.Now;
                                int minutesRemaining = (int)difference.TotalMinutes;

                                if (minutesRemaining > 0) { substractedTime.Add(minutesRemaining + "'"); }
                                else { substractedTime.Add(">>"); }
                            }
                            else
                            {
                                Debug.Log("no se pudo parsear la linea " + targetLine);
                            }
                        }

                        GameObject lineaIterada = Instantiate(rt_iteration, rt_iterationParent.transform);
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




        yield return new WaitForSeconds(0.1f);

        rt_iterationParent.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        rt_iterationParent.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        rt_iterationParent.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        rt_iterationParent.SetActive(true);
    }










    public void KillChildren()
    {
        for (int i = rt_iterationParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(rt_iterationParent.transform.GetChild(i).gameObject);
        }

        for (int i = ps_iterationParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ps_iterationParent.transform.GetChild(i).gameObject);
        }
    }














    public void LoadOnMap(string line, string code)
    {
        paradaCodigo.text = code;

        mode = "ps"; //positions

        Debug.Log(line);

        for (int i = ps_iterationParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ps_iterationParent.transform.GetChild(i).gameObject);
        }

        string empresa = "58";
        int lineI = int.Parse(line);
        /*
        if (lineI > 279 && lineI < 360) { empresa = "58"; }
        else if (lineI > 609 && lineI < 697) { empresa = "4"; }
        else if (lineI > 400 && lineI < 500) { empresa = "25"; }
        */


        StartCoroutine(GetPositions("https://apisvt.avanzagrupo.com/lineas/getTrayectos?empresa=" + empresa + "&linea=" + line + "&sentido=V", line, code));
        lastSavedCode = line;
        lastSavedCorp = empresa;
    }

    IEnumerator GetPositions(string apiURL, string targetLine, string code)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = webRequest.downloadHandler.text;

                ResponsePS data = JsonUtility.FromJson<ResponsePS>(jsonResult);

                ProcessPositions(data, targetLine, code);
            }
        }
    }

    void ProcessPositions(ResponsePS data, string targetLine, string code)
    {

        if (data == null || data.data == null || data.data.trayectos == null || data.data.trayectos.Count == 0)
        {
            apiError.SetActive(true);
            return;
        }

        foreach (TrayectoPS route in data.data.trayectos)
        {
            if (route.paradas != null && route.paradas.Count > 0)
            {
                foreach (ParadaPS stop in route.paradas)
                {
                    if (stop.coordinates == null || stop.coordinates.Count < 2)
                    {
                        continue;
                    }

                    GameObject iteratedStop = Instantiate(ps_iteration, ps_iterationParent.transform);
                    RectTransform iteratedTransform = iteratedStop.GetComponent<RectTransform>();

                    Button button = iteratedStop.GetComponent<Button>();
                    stopButton script = iteratedStop.GetComponent<stopButton>();

                    //button.onClick.RemoveAllListeners();
                    //button.onClick.AddListener(script.OnClickMath);
                    

                    script.SetCode(stop.codigo);

                    float minLatitude = 40.25002f;
                    float maxLatitude = 40.56269f;
                    float minLongitude = -3.9829f;
                    float maxLongitude = -3.253411f;
                    float lon = float.Parse(stop.coordinates[0], System.Globalization.CultureInfo.InvariantCulture);
                    float lat = float.Parse(stop.coordinates[1], System.Globalization.CultureInfo.InvariantCulture);

                    float pctX = (lon - minLongitude) / (maxLongitude - minLongitude);
                    float pctY = (lat - minLatitude) / (maxLatitude - minLatitude);

                    pctX = Mathf.Clamp01(pctX);
                    pctY = Mathf.Clamp01(pctY);


                    float mapWidth = ps_iterationParentRect.rect.width;
                    float mapHeight = ps_iterationParentRect.rect.height;


                    float localX = (pctX - ps_iterationParentRect.pivot.x) * mapWidth;
                    float localY = (pctY - ps_iterationParentRect.pivot.y) * mapHeight;

                    iteratedTransform.anchoredPosition = new Vector2(localX, localY);


                    if (stop.codigo.Trim() == code.Trim())
                    {
                        Debug.Log("stop found in api");


                        Canvas.ForceUpdateCanvases();

                        RectTransform viewport = ps_scroll.viewport;
                        RectTransform content = ps_scroll.content;

                        Vector2 viewportCenter = viewport.rect.center;
                        Vector2 stopPos = iteratedTransform.anchoredPosition;

                        Vector2 offset = viewportCenter - stopPos;
                        content.anchoredPosition += offset;


                        /*
                        Canvas.ForceUpdateCanvases();

                        RectTransform viewport = ps_scroll.viewport;

                        Vector2 locatorLocalPos = ps_content.InverseTransformPoint(iteratedTransform.position);

                        Vector3 viewportWorldCenter = viewport.TransformPoint(viewport.rect.center);
                        Vector2 viewportCenterInContentSpace = ps_content.InverseTransformPoint(viewportWorldCenter);

                        Vector2 offset = viewportCenterInContentSpace - locatorLocalPos;

                        ps_content.anchoredPosition += offset;
                        */
                    }
                }
            }
        }
    }
}
