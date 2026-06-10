using NUnit.Framework.Internal.Builders;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
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


    //selection
    public string selection_direction;
    public GameObject selection;
    public TMP_Text se_paradaName;
    public GameObject se_corresObj;
    public TMP_Text se_corresText;
    public GameObject se_trObj;
    public TMP_Text se_trText;
    public TMP_Text selection_contarParadas;


    //info para enviar
    public int lineaNumb;
    public int ParadaNumb;

    public string lineaInicial;
    public int paradaInicial;
    public string lineaFinal;
    public int paradaFinal;

    public bool isContarParadas = false;
    public bool readyToSendInfo = false;


    //credenciales emt
    private string clientId = "a9da16a1-e6ee-4226-a4c0-19956b36c3b4";
    private string passKey = "989D85EC811654F83FB0F4F08DFBAA6FF6EC156C6B47F0A41432832B3A56E223837BE0D113795567D673C321BFA1A189F3D8C5C26CF6C46215880000CB4B1666";
    private string currentAccessToken = "";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(InitEmtAuthentication());

        //credenciales emt
        clientId = "a9da16a1-e6ee-4226-a4c0-19956b36c3b4";
        passKey = "989D85EC811654F83FB0F4F08DFBAA6FF6EC156C6B47F0A41432832B3A56E223837BE0D113795567D673C321BFA1A189F3D8C5C26CF6C46215880000CB4B1666";
        currentAccessToken = "";

    }

    IEnumerator InitEmtAuthentication()
    {
        yield return new WaitForEndOfFrame(); //StartCoroutine(LoginEmt());
        IterateList();
    }

    IEnumerator LoginEmt()
    {
        // FIX: Cambiada la ruta 404 por el endpoint de autenticación oficial de la EMT
        string url = "https://emtmadrid.es";

        // FIX: El login de MobilityLabs requiere obligatoriamente un método GET
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Pasamos las credenciales fijas únicamente por las cabeceras HTTP de la llamada
            webRequest.SetRequestHeader("X-ClientId", clientId);
            webRequest.SetRequestHeader("passKey", passKey);

            if (se_trText != null) se_trText.text = "Autenticando EMT...";

            yield return webRequest.SendWebRequest();

            Debug.Log("Status Code: " + webRequest.responseCode);

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en Login EMT: " + webRequest.error + " | Código: " + webRequest.responseCode);
                if (se_trText != null) se_trText.text = "Error de autenticación.";
            }
            else
            {
                string jsonResult = webRequest.downloadHandler.text;
                Debug.Log("RESPUESTA REAL DE LA API: " + jsonResult);

                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResult);

                // Accedemos a response.data directamente ya que mapea la colección del JSON de la EMT
                if (response != null && response.data != null)
                {
                    currentAccessToken = response.data[0].accessToken;
                    Debug.Log("¡Token EMT obtenido con éxito!: " + currentAccessToken);
                }
                else
                {
                    Debug.LogError("El JSON se leyó pero la estructura 'data' del token venía vacía.");
                }
            }
        }
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

        if (search.text.Length < 4 && search.text.All(char.IsDigit))
        {
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
        string basePath = "buses/paradas/" + line.Trim() + "/" + line.Trim() + "_dir" + dir.Trim();
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
        string basePath = "buses/paradas/" + line.Trim() + "/" + line.Trim() + "_dir" + dir.Trim();

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


        if (!string.IsNullOrEmpty(conexion[index].Trim()))
        {
            se_corresObj.SetActive(true);
            se_corresText.text = conexion[index];
        }
        else
        {
            se_corresObj.SetActive(false);
        }

        StartCoroutine(GetRealTimeBusArrival(codigoParada, line));


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

    IEnumerator GetRealTimeBusArrival(string codigoParada, string lineID)
    {
        string url = "https://openapi.emtmadrid.es/v2/transport/buses/stations/" + codigoParada + "/arrivals";

        string jsonBody = "{\"line\":\"" + lineID + "\",\"timeDate\":\"\"}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        { 
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("accessToken", currentAccessToken);

            se_trText.text = "Cargando...";

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                se_trText.text = "Error.";
            }
            else
            {
                string jsonResult = webRequest.downloadHandler.text;
                ParseBusResponse(jsonResult);
            }

        }
    }

    void ParseBusResponse(string json)
    {
        try
        {
            EmtResponse response = JsonUtility.FromJson<EmtResponse>(json);

            if (response != null && response.data != null && response.data.Length > 0)
            {
                EmtArriveData proximoBus = response.data[0];

                int segundosRestantes = proximoBus.estimateArrive;

                if (segundosRestantes == 999999)
                {
                    se_trText.text = "Sin previsión (Bus parado o fin de servicio)";
                }
                else if (segundosRestantes < 30)
                {
                    se_trText.text = "Inminente / En parada";
                }
                else
                {
                    int minutos = segundosRestantes / 60;
                    se_trText.text = "Próximo bus: " + minutos + " min (" + proximoBus.destination + ")";
                }
            }
            else
            {
                se_trText.text = "No hay autobuses programados.";
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al procesar el JSON: " + e.Message);
            se_trText.text = "Error de formato.";
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
    public void Iterate76767676767676(int linea)
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




[System.Serializable]
public class EmtResponse
{
    public string code;
    public string description;
    public EmtData datetime;
    public EmtArriveData[] data; // Aquí vienen los autobuses
}



[System.Serializable]
public class EmtData
{
    public string date;
}



[System.Serializable]
public class EmtArriveData
{
    public string line;
    public string stop;
    public int estimateArrive; // Tiempo restante en segundos
    public int DistanceBus;    // Distancia en metros
    public string destination; // Destino del autobús
    public string bus;         // Número identificador del vehículo
}


[System.Serializable]
public class TokenData
{
    public string accessToken;
    public string tokenSecuencial;
}

