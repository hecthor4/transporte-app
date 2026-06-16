using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class bus : MonoBehaviour
{
    //escena
    public GameObject busParent;
    public GameObject busLineParent;


    public TMP_Text direccion1;
    public TMP_Text direccion2;

    public RectTransform d1_parent;
    public RectTransform d2_parent;

    public bool isDir1Shown;
    public bool isDir2Shown;

    public GameObject dir1Obj;
    public GameObject dir2Obj;


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
    public GameObject paradaButton;
    public GameObject paradaButton_corresMetro;
    public GameObject paradaButton_corresRenfe;


    public GameObject paradaLineasTiempoReal;
    public GameObject pltr_parent;
    public GameObject pltr_linea;


    //selection
    public string selection_direction;              //direccion de la parada seleccionada
    public bool hasFoundRoute;                      //si ha parseado datos de tiempo real bus
    public GameObject selection;                    //objeto parent seleccion
    public TMP_Text se_paradaName;                  //nombre parada
    public GameObject se_corresObj;                 //correspondencia titulo texto
    public TMP_Text se_corresText;                  //correspondencia texto
    public GameObject se_trObj;                     //tiempo real objeto parent
    public TMP_Text se_trText;                      //tiempo real en hh:mm
    public TMP_Text se_trMinsText;                  //tiempo real restante en minutos
    public TMP_Text selection_contarParadas;
    public TMP_Text se_codigoParada;


    //info para enviar
    public int lineaNumb;
    public int ParadaNumb;

    public string lineaInicial;
    public int paradaInicial;
    public string lineaFinal;
    public int paradaFinal;

    public bool isContarParadas = false;
    public bool readyToSendInfo = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IterateList();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveInfo()
    {
        if (readyToSendInfo == true)
        {
            trayecto trayecto = FindFirstObjectByType<trayecto>();
            Debug.LogWarning("bus_dir" + selection_direction);
            trayecto.FetchInfo(paradaInicial, lineaInicial, paradaFinal, lineaFinal, "bus_dir" + selection_direction);

            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.AllToNormal();
        }

        else if (isContarParadas == true)
        {
            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.ShowIndicator(paradaInicial + "", lineaInicial, "bus_dir" + selection_direction);
        }
    }
    public void SaveLineaToFavs()
    {
        favorites favorites = FindFirstObjectByType<favorites>();
        favorites.SaveLinea(busLinea.text, "bus");
    }


    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void ToggleDirectionVisibility(string dir)
    {
        if (dir == "1")
        {
            isDir1Shown = !isDir1Shown;
            dir1Obj.SetActive(isDir1Shown);
        }
        if (dir == "2")
        {
            isDir2Shown = !isDir2Shown;
            dir2Obj.SetActive(isDir2Shown);
        }
    }

    public void GoBack()
    {
        //borrar paradas de antes
        for (int i = 0; i < d1_parent.childCount; i++)
        {
            GameObject child = d1_parent.GetChild(i).gameObject;
            Destroy(child);
        }
        for (int i = 0; i < d2_parent.childCount; i++)
        {
            GameObject child = d2_parent.GetChild(i).gameObject;
            Destroy(child);
        }
    }

    public void Search()
    {
        string searchText = search.text.ToString();

        if (search.text.Length <= 3 && search.text.All(char.IsDigit))
        {
            foreach (GameObject item in elementsInList)
            {
                //TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
                TMP_Text nombreLinea = item.transform.Find("nombre").GetComponent<TMP_Text>();

                bool matches = nombreLinea.text.ToLower().Contains(searchText);
                item.SetActive(matches);
            }
        }
        if (search.text.Length >= 3 && search.text.All(char.IsDigit))
        {
            paradaLineasTiempoReal.SetActive(true);



            //buscar tambien las lineas de bus, por si acaso
            foreach (GameObject item in elementsInList)
            {
                //TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
                TMP_Text nombreLinea = item.transform.Find("nombre").GetComponent<TMP_Text>();

                bool matches = nombreLinea.text.ToLower().Contains(searchText);
                item.SetActive(matches);
            }
        }
        if (search.text.All(char.IsLetter))
        {
            foreach (GameObject item in elementsInList)
            {
                TMP_Text nombreLinea = item.transform.Find("info").GetComponent<TMP_Text>();

                bool matches = nombreLinea.text.ToLower().Contains(searchText);
                item.SetActive(matches);
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
    }


    public void OpenLineInfo(string line)
    {
        busParent.SetActive(false);
        busLineParent.SetActive(true);
        Debug.Log(line);

        goBack goback = FindFirstObjectByType<goBack>();
        goback.OpenScreen();


        //info nombre texto arriba
        string cleanText(string texto) => texto.Replace("emt_", "").Trim();
        if (line.Contains("_"))
        {
            busLinea.text = "EMT " + cleanText(line);
        }
        else
        {
            busLinea.text = cleanText(line);
        }

        OpenAndLoadDirection(line, "1", d1_parent);
        OpenAndLoadDirection(line, "2", d2_parent);
    }

    void OpenAndLoadDirection(string line, string dir, RectTransform parentTransform)
    {
        string basePath = "buses/paradas/" + line.Trim() + "/dir" + dir.Trim();
        Debug.Log(basePath);

        //coger paradas iterar
        TextAsset codigoRaw = Resources.Load<TextAsset>(basePath + "_co");
        if (codigoRaw == null) { Debug.Log("no hay"); }
        string[] codigos = codigoRaw.text.Trim().Split('\n');

        TextAsset municipioRaw = Resources.Load<TextAsset>(basePath + "_mu");
        if (municipioRaw == null) { Debug.Log("no hay"); }
        string[] municipios = municipioRaw.text.Trim().Split('\n');

        TextAsset paradaRaw = Resources.Load<TextAsset>(basePath + "_pa");
        if (paradaRaw == null) { Debug.Log("no hay"); }
        string[] paradas = paradaRaw.text.Trim().Split('\n');

        TextAsset conexionRaw = Resources.Load<TextAsset>(basePath + "_tr");     //"tr" por transbordo,
        if (conexionRaw == null) { Debug.Log("no hay"); }
        string[] conexion = conexionRaw.text.Split('\n');    //"co" estaba pillado.

        //direccion texto
        if (dir == "1") { direccion1.text = "Dirección " + municipios[municipios.Length - 1]; }
        if (dir == "2") { direccion2.text = "Dirección " + municipios[municipios.Length - 1]; }

        //paradas iterar en texto
        string currentMunicipio = null;
        GameObject currentCiudadObj = null;

        for (int i = 0; i < municipios.Length; i++)
        {
            string municipio = municipios[i].Trim();
            string parada = paradas[i].Trim();
            string conexionActual = "";

            if (i < conexion.Length && conexion[i] != null)
            {
                conexionActual = conexion[i].Trim();
            }

            if (municipio != currentMunicipio)
            {
                currentMunicipio = municipio;
                currentCiudadObj = Instantiate(ciudadPrefab, parentTransform);

                TMP_Text nombreCiudad = currentCiudadObj.transform.Find("nombre").GetComponent<TMP_Text>();
                nombreCiudad.text = municipio;
            }

            Transform buttonParent = currentCiudadObj.transform.Find("box");

            if (buttonParent != null)
            {
                int indexForButton = i;

                GameObject instantiateButtonPrefab = paradaButton;

                if (string.IsNullOrEmpty(conexionActual))
                {
                    instantiateButtonPrefab = paradaButton;
                }
                else if (conexionActual.Contains("Metro"))
                {
                    instantiateButtonPrefab = paradaButton_corresMetro;
                }
                else if (conexionActual.Contains("Renfe"))
                {
                    instantiateButtonPrefab = paradaButton_corresRenfe;
                }
                GameObject iteratedButton = Instantiate(instantiateButtonPrefab, buttonParent);
                iteratedButton.name = indexForButton + "," + dir;

                UnityEngine.UI.Button button = iteratedButton.GetComponent<UnityEngine.UI.Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnParadaButtonClick(indexForButton, dir));

                TMP_Text nombreParada = iteratedButton.GetComponentInChildren<TMP_Text>();

                if (nombreParada != null)
                {
                    nombreParada.text = parada;
                }
            }
        }


        /*
        for (int i = 0; i < municipios.Length; i++)
        {
            string municipio = municipios[i];
            string parada = paradas[i];

            if (municipio != currentMunicipio)
            {
                currentMunicipio = municipio;
                currentCiudadObj = Instantiate(ciudadPrefab, new Vector3(0, 0, 0), Quaternion.identity, parentTransform);
                TMP_Text nombreCiudad = currentCiudadObj.transform.Find("nombre").GetComponent<TMP_Text>();
                nombreCiudad.text = municipio;
            }

            GameObject buttonParent = currentCiudadObj.transform.Find("box").gameObject;

            GameObject iteratedButton = Instantiate(paradaButton, new Vector3(0, 0, 0), Quaternion.identity, buttonParent.transform);
            TMP_Text nombreParada = iteratedButton.GetComponent<TMP_Text>();
            nombreParada.text = parada;
        }
        */


        /*
        for (int i = 0; i < municipios.Length - 1; i++)
        {
            if (i == 0 || municipios[i] != municipios[i - 1])
            {
                GameObject iterated = Instantiate(ciudadPrefab, new Vector3(0, 0, 0), Quaternion.identity, parentTransform);
                TMP_Text nombreCiudad = iterated.transform.Find("nombre").GetComponent<TMP_Text>();
                nombreCiudad.text = municipios[i];

                for (int j = 0; j < paradas.Length; j++)
                {
                    if (j == 0 || municipios[j] == municipios[j - 1])
                    {
                        if (municipios[j] == municipios[i])
                        {
                            GameObject buttonParent = iterated.transform.Find("box").gameObject;

                            GameObject iteratedButton = Instantiate(paradaButton, new Vector3(0, 0, 0), Quaternion.identity, buttonParent.transform);
                            TMP_Text nombreParada = iteratedButton.transform.Find("box").GetComponent<TMP_Text>();
                            nombreParada.text = paradas[j];
                        }
                    }
                }
            }
        }
        */
    }

    public void ContarParadasReset()
    {
        lineaNumb = 0;
        ParadaNumb = 0;

        lineaInicial = "0";
        paradaInicial = 0;
        lineaFinal = "0";
        paradaFinal = 0;

        isContarParadas = false;
        readyToSendInfo = false;
    }

    public void OnParadaButtonClick(int paradaID, string dir)
    {
        selection.SetActive(true);

        selection_direction = dir;
        string line = busLinea.text;
        string basePath = "buses/paradas/" + line.Trim() + "/dir" + dir.Trim();

        TextAsset paradaRaw = Resources.Load<TextAsset>(basePath + "_pa");
        if (paradaRaw == null) { Debug.Log("no hay"); }
        string[] paradas = paradaRaw.text.Trim().Split('\n');

        TextAsset codigoRaw = Resources.Load<TextAsset>(basePath + "_co");
        if (codigoRaw == null) { Debug.Log("no hay"); }
        string[] codigos = codigoRaw.text.Trim().Split('\n');

        TextAsset conexionRaw = Resources.Load<TextAsset>(basePath + "_tr");
        if (conexionRaw == null) { Debug.Log("no hay"); }
        string[] conexion = conexionRaw.text.Split('\n');

        int index = paradaID;
        string codigoParada = codigos[index].Trim();


        se_paradaName.text = paradas[index];
        se_codigoParada.text = codigos[paradaID];


        //conseguir realtimes
        StopAllCoroutines();
        hasFoundRoute = false;
        StartCoroutine(GetRealTimes("https://api.transit.welbits.com/stops/times/" + codigos[paradaID]));


        if (!string.IsNullOrEmpty(conexion[index].Trim()))
        {
            se_corresObj.SetActive(true);
            se_corresText.text = conexion[index];
        }
        else
        {
            se_corresObj.SetActive(false);
        }


        if (isContarParadas == false)
        {
            isContarParadas = true;
            paradaInicial = paradaID;
            lineaInicial = busLinea.text;

            selection_contarParadas.text = "Viajar desde aquí hasta...";
        }
        else if (isContarParadas == true)
        {
            paradaFinal = paradaID;
            lineaFinal = busLinea.text;
            readyToSendInfo = true;
            //SaveInfo();

            selection_contarParadas.text = "Viajar hasta aquí";
        }
    }


    IEnumerator GetRealTimes(string apiURL)
    {
        se_trText.text = "Cargando...";
        se_trMinsText.text = "";
        Coroutine anim = StartCoroutine(RealTimeTextAnim());

        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            yield return webRequest.SendWebRequest();
            StopCoroutine(anim);

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = webRequest.downloadHandler.text;

                Response data = JsonUtility.FromJson<Response>(jsonResult);

                ProcessRealTimes(data);
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
            se_trText.text = "Cargando...";
            yield return new WaitForSeconds(time);
            se_trText.text = "Cargando";
            yield return new WaitForSeconds(time);
            se_trText.text = "Cargando.";
            yield return new WaitForSeconds(time);
            se_trText.text = "Cargando..";
            yield return new WaitForSeconds(time);
        }
    }


    void ProcessRealTimes(Response data)
    {
        hasFoundRoute = false;


        if (data == null || data.routes == null || data.routes.Count == 0)
        {
            se_trText.text = "No se encuentran tiempos.";
            return;
        }

        System.Collections.Generic.List<string> formattedTimes = new System.Collections.Generic.List<string>();
        System.Collections.Generic.List<string> substractedTime = new System.Collections.Generic.List<string>();

        foreach (BusTrip route in data.routes)
        {
            if (route.shortName != lineaInicial.ToString())
            {
                continue;
            }

            hasFoundRoute = true;

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
                        Debug.Log("no se pudo parsear. " + time.arrivalDate);
                    }

                    /*
                    if (string.IsNullOrEmpty(time.arrivalDate) == false && time.arrivalDate.Contains("T"))
                    {
                        string[] split = time.arrivalDate.Split('T')[1].Split(':');
                        int timeUTC = int.Parse(split[0]);
                        string minutes = split[1];

                        int localTime = (timeUTC + 2) % 24;
                        string cleanDate = localTime.ToString();

                        Debug.Log("hora est: " + cleanDate);

                        if (se_trText.text != "") { se_trText.text = se_trText.text + "   /   " + cleanDate; }
                        else { se_trText.text = cleanDate; }
                    }
                    */
                }
            }

            break;
        }

        if (hasFoundRoute == true && formattedTimes.Count > 0)
        {
            se_trText.text = string.Join("   /   ", formattedTimes);
            se_trMinsText.text = string.Join("   /   ", substractedTime);
        }
        else
        {
            se_trText.text = "No se encuentran tiempos.";
            se_trMinsText.text = "";
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




    //depricado. usar OpenLineInfo
    void Iterate76767676767676(int linea)
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
