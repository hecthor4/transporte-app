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


    //layout groups
    public GridLayoutGroup botonesBusLineaParent;
    public VerticalLayoutGroup pltr_parent_layout;
    public VerticalLayoutGroup viewportContentLayout;


    //iterar lista
    public int busButtonHeightX;
    public int busButtonHeightY;
    public GameObject bus_info;
    public GameObject bus_info_emt;
    public GameObject bus_info_n;
    public GameObject iteratedParent;

    public TMP_InputField search;
    public bool isInterurbanPrefered;
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
    public TMP_Text pltr_parada_code;
    public TMP_Text pltr_parada_name;


    //selection
    public GameObject background;
    public string lastClickedParadaCode;

    public string selection_direction;              //direccion de la parada seleccionada
    public bool hasFoundRoute;                      //si ha parseado datos de tiempo real bus
    public GameObject selection;                    //objeto parent seleccion
    public TMP_Text se_paradaName;                  //nombre parada
    public GameObject se_corresObj;                 //correspondencia titulo texto
    public TMP_Text se_corresText;                  //correspondencia texto
    public GameObject se_trObj;                     //tiempo real objeto parent
    public GameObject se_loading;                   //tiempo real en hh:mm
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


        paradaLineasTiempoReal.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(GetStopsByAPI("https://www.crtm.es/widgets/api/GetLinesInformation.php?activeItinerary=1&codLine=8__" + "280" + "___", "280 HOSPITAL-LOECHES"));
        }
    }

    public void SaveInfo()
    {
        if (isContarParadas == false)
        {
            isContarParadas = true;

            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.ShowIndicator(paradaInicial + "", lineaInicial, "bus_dir" + selection_direction);
        }
        else
        {
            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.ShowIndicator(paradaInicial + "", lineaInicial, "bus_dir" + selection_direction);
        }


        if (readyToSendInfo == true)
        {
            trayecto trayecto = FindFirstObjectByType<trayecto>();
            trayecto.FetchInfo(paradaInicial, lineaInicial, paradaFinal, lineaFinal, "bus_dir" + selection_direction);

            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.AllToNormal();
        }
    }
    public void SaveLineaToFavs()
    {
        favorites favorites = FindFirstObjectByType<favorites>();

        string line;
        if (busLinea.text.Contains("EMT")) { line = "emt_" + busLinea.text.Substring(4); }
        else { line = busLinea.text; }

        favorites.SaveLinea(line, "bus");
    }
    public void SaveParadaFullToFavs() //guardar parada (entera: con todas las lineas) en favoritos
    {
        favorites favorites = FindFirstObjectByType<favorites>();
        favorites.SaveParada(pltr_parada_code.text);
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
        /*
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
        */
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

            StopAllCoroutines();
            hasFoundRoute = false;


            //preferencia de buses interurbanos
            if (isInterurbanPrefered == true)
            {
                if (searchText == "5617") { searchText = "8-5617"; }
            }


            StartCoroutine(GetRealTimes("https://api.transit.welbits.com/stops/times/" + searchText, "all"));


            //buscar tambien las lineas de bus, por si acaso
            foreach (GameObject item in elementsInList)
            {
                //TMP_Text itemText = item.GetComponentInChildren<TMP_Text>();
                TMP_Text nombreLinea = item.transform.Find("nombre").GetComponent<TMP_Text>();

                bool matches = nombreLinea.text.ToLower().Contains(searchText);
                item.SetActive(matches);
            }
        }
        else //quitar la parada en tiempo real
        {
            paradaLineasTiempoReal.SetActive(false);
        }
        if (search.text.All(char.IsLetter)) //si buscas una ciudad
        {
            string searchLower = search.text.ToLower();

            foreach (GameObject item in elementsInList)
            {
                string lineName = item.name.Trim();

                TextAsset checkDir1 = Resources.Load<TextAsset>("buses/paradas/" + lineName.Trim() + "/dir1_mu");
                if (checkDir1 != null && checkDir1.text.ToLower().Trim().Contains(searchLower))
                {
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }

            }
        }

        StartCoroutine(RefreshLayoutGroups());
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
        yield return new WaitForSeconds(0.1f);

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


    public void OpenLineInfo(string line)
    {
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



    // <work>
    // WORK IN PROGRESS
    IEnumerator GetStopsByAPI(string apiURL, string targetLine)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = webRequest.downloadHandler.text;

                ResponseBS data = JsonUtility.FromJson<ResponseBS>(jsonResult);

                OpenAndLoadDirectionByAPI(data, targetLine);
            }
            else
            {
                Debug.Log("error api: " + webRequest.error);
            }
        }
    }

    void OpenAndLoadDirectionByAPI(ResponseBS data, string targetLine)
    {
        var itineraries = data.lines.LineInformation.itinerary?.Itinerary;

        foreach (var itinerary in itineraries)
        {
            if (itinerary == null) { continue; }
            if (itinerary.name != targetLine) { continue; }

            foreach (var stop in itinerary.stops.StopInformation)
            {
                Debug.Log("stop code: " + stop.codStop + " / stop name: " + stop.name);
            }
        }
    }
    //
    // </work>




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
        background.SetActive(true);
        selection.SetActive(true);

        selection_direction = dir;

        string line;
        if (busLinea.text.Contains("EMT")) { line = "emt_" + busLinea.text.Substring(4); }
        else { line = busLinea.text; }

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
            paradaInicial = paradaID;
            lineaInicial = busLinea.text; //fix for emt

            selection_contarParadas.text = "Viajar desde aquí hasta...";
        }
        else if (isContarParadas == true)
        {
            paradaFinal = paradaID;
            lineaFinal = busLinea.text; //fix for emt
            readyToSendInfo = true;
            //SaveInfo();

            selection_contarParadas.text = "Viajar hasta aquí";
        }




        //conseguir realtimes
        StopAllCoroutines();
        hasFoundRoute = false;

        //arreglar los codigos
        string fixedCode = codigos[paradaID].Trim();
        if (codigos[paradaID].Length == 4 && fixedCode.Substring(0, 1) == "7") { fixedCode = "0" + fixedCode; }

        lastClickedParadaCode = fixedCode;

        StartCoroutine(GetRealTimes("https://api.transit.welbits.com/stops/times/" + fixedCode, lineaInicial));
    }


    IEnumerator GetRealTimes(string apiURL, string targetLine)
    {
        se_loading.SetActive(true);
        se_trText.text = "";
        se_trMinsText.text = "";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            yield return webRequest.SendWebRequest();
            se_loading.SetActive(false);

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = webRequest.downloadHandler.text;

                ResponseRT data = JsonUtility.FromJson<ResponseRT>(jsonResult);

                ProcessRealTimes(data, targetLine);
            }
            else
            {
                Debug.Log("error api: " + webRequest.error);
            }
        }
    }


    void ProcessRealTimes(ResponseRT data, string targetLine)
    {
        hasFoundRoute = false;


        if (data == null || data.routes == null || data.routes.Count == 0)
        {
            se_trText.text = "No se encuentran tiempos.";
            return;
        }

        System.Collections.Generic.List<string> formattedTimes = new System.Collections.Generic.List<string>();
        System.Collections.Generic.List<string> substractedTime = new System.Collections.Generic.List<string>();


        //si estas viendo el tiempo de un bus en su respectivo menu, dentro de b all
        if (targetLine == lineaInicial)
        {
            paradaLineasTiempoReal.SetActive(false); //esconder lo otro de abajo

            foreach (BusTripRT route in data.routes)
            {
                if (route.shortName != targetLine.ToString())
                {
                    continue;
                }

                hasFoundRoute = true;

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
                            Debug.Log("no se pudo parsear. " + time.arrivalDate);
                            se_trMinsText.text = "Error";
                        }
                    }
                }

                break;
            }

            if (hasFoundRoute == true && formattedTimes.Count > 0)
            {
                se_loading.SetActive(false);
                se_trText.text = string.Join("   /   ", formattedTimes);
                se_trMinsText.text = string.Join("   /   ", substractedTime);
            }
            else
            {
                se_loading.SetActive(false);
                se_trText.text = "No se encuentran tiempos.";
                se_trMinsText.text = "";
            }
        }


        //si estas buscando paradas en el dialogo de busqueda de bus main
        else if (targetLine == "all")
        {
            paradaLineasTiempoReal.SetActive(true);

            int linesToIterate = 0;
            List<string> iteratedLines = new List<string>();
            List<string> notIteratedLines = new List<string>();

            notIteratedLines.Add(data.routes.ToString());
            linesToIterate = notIteratedLines.Count;


            //borrar lo de antes
            RectTransform pltr_parent_rt = pltr_parent.GetComponent<RectTransform>();
            for (int i = 0; i < pltr_parent_rt.childCount; i++)
            {
                GameObject child = pltr_parent_rt.GetChild(i).gameObject;
                Destroy(child);
            }



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

                        GameObject lineaIterada = Instantiate(pltr_linea, pltr_parent.transform);
                        lineaIterada.transform.localScale = new Vector3(0, 0, 0);
                        lineaIterada.transform.localScale = Vector3.one;


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
            if (data.stopCode.Contains("_")) { pltr_parada_code.text = data.stopCode.Substring(2); }
            else { pltr_parada_code.text = data.stopCode; }

            for (int i = 1; i < 3; i++)
            {
                string path = "buses/paradas/";
                TextAsset paradaRaw = Resources.Load<TextAsset>(path + data.routes[0].shortName.Trim() + "/dir" + i + "_pa");
                if (paradaRaw == null) { Debug.Log("l. 615. no se encuentra el archivo de paradas para el nombre de la parada de bus que se quiere buscar"); }
                string[] paradas = paradaRaw.text.Trim().Split('\n');

                TextAsset codigoRaw = Resources.Load<TextAsset>(path + data.routes[0].shortName.Trim() + "/dir" + i + "_co");
                if (codigoRaw == null) { Debug.Log("l. 619. no se encuentra el archivo de codigos para conseguir la linea exacta para el nombre de las paradas"); }
                string[] codigos = codigoRaw.text.Trim().Split('\n');

                for (int j = 0; j < codigos.Length; j++) {
                    //Debug.Log("iteration " + j + " / codigo: " + codigos[j].Trim() + " / codigo parada: " + data.stopCode);
                    if (codigos[j].Trim() == data.stopCode || codigos[j].Trim() == data.stopCode.Substring(2)) { pltr_parada_name.text = paradas[j]; break; } }
            }
        }


        StartCoroutine(RefreshLayoutGroups());
    }



    void IterateList()
    {
        /* the code below is used to iterate every bus saved in the database
         * and render it on the on the bus scrollview, under main
         * it is not needed as the buses are now pre-rendered
         * (we ran these lines once and saved the fetched information)
         * 
         */

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


        //buses nocturnos
        TextAsset todosLosNumerosDeLineas_noc = Resources.Load<TextAsset>("buses/iterar/bus11_n");
        string[] numerosLineas_n = todosLosNumerosDeLineas_noc.text.Trim().Split('\n');

        TextAsset todasLasParadasDeLineas_noc = Resources.Load<TextAsset>("buses/iterar/bus12_n");
        string[] paradasLineas_n = todasLasParadasDeLineas_noc.text.Trim().Split('\n');

        for (int i = 0; i < numerosLineas_n.Length; i++)
        {
            GameObject iterated = Instantiate(bus_info_n, new Vector3(0, 0, 0), Quaternion.identity, iteratedParent.transform);
            iterated.name = numerosLineas_n[i];

            TMP_Text nombreLinea = iterated.transform.Find("nombre").GetComponent<TMP_Text>();
            TMP_Text infoLinea = iterated.transform.Find("info").GetComponent<TMP_Text>();

            Button button = iterated.transform.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            string lineName = iterated.name;
            button.onClick.AddListener(() => OpenLineInfo(lineName));

            nombreLinea.text = numerosLineas_n[i];
            infoLinea.text = paradasLineas_n[i];
        }


        //añadir a la lista para buscar
        foreach (Transform child in iteratedParent.transform)
        {
            elementsInList.Add(child.gameObject);
        }
    }

    public void OpenVisorDeParadas()
    {
        stopVisualizer stopVisualizer = FindFirstObjectByType<stopVisualizer>();
        stopVisualizer.Load(se_codigoParada.text);
    }

    public void OpenVisorDeParadasEnMapa()
    {
        //arreglar los codigos
        string fixedCode = "0";
        if (se_codigoParada.text.Length == 4 && fixedCode.Substring(0, 1) == "7") { fixedCode = "0" + fixedCode; }
        else { fixedCode = se_codigoParada.text; }

        stopVisualizer stopVisualizer = FindFirstObjectByType<stopVisualizer>();
        stopVisualizer.LoadOnMap(lineaInicial + "", fixedCode);
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

    public void ModifyButtonHeight(int newHeightX, int newHeightY)
    {
        busButtonHeightX = newHeightX;
        busButtonHeightY = newHeightY;
    }
}
