using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class trayecto : MonoBehaviour
{
    public TMP_Text transportType;

    public GameObject siguienteButton;
    public TMP_Text siguienteText;
    public GameObject cantContinueNextButton;
    public TMP_Text cantContinueText;

    public string infoTransportType;
    public string lineaPrimera;
    public int paradaPrimera;
    public string lineaFinal;
    public int paradaFinal;
    public string direccion;

    public GameObject m_all;
    public GameObject r_all;
    public GameObject b_all;
    public GameObject m_metroLineas;
    public GameObject recorridoViajes;

    public GameObject paso1;
    public TMP_Text paso1_currentLinea;
    public TMP_Text paso1_paradaInicial;
    public TMP_Text paso1_paradaFinal;
    public TMP_Text paso1_direccion;
    public TMP_Text paso1_paradasDiferencia;

    public GameObject paso1_5;
    public TMP_Text paso1_5_text;

    public GameObject paso2;
    public TMP_Text paso2_currentLinea;
    public TMP_Text paso2_paradaInicial;
    public TMP_Text paso2_paradaFinal;
    public TMP_Text paso2_direccion;
    public TMP_Text paso2_paradasDiferencia;

    public GameObject paso2_5;
    public TMP_Text paso2_5_text;

    public GameObject paso3;
    public TMP_Text paso3_currentLinea;
    public TMP_Text paso3_paradaInicial;
    public TMP_Text paso3_paradaFinal;
    public TMP_Text paso3_direccion;
    public TMP_Text paso3_paradasDiferencia;

    public GameObject paso3_5;
    public TMP_Text paso3_5_text;

    public GameObject paso4;
    public TMP_Text paso4_currentLinea;
    public TMP_Text paso4_paradaInicial;
    public TMP_Text paso4_paradaFinal;
    public TMP_Text paso4_direccion;
    public TMP_Text paso4_paradasDiferencia;

    public GameObject paso4_5;
    public TMP_Text paso4_5_text;

    public GameObject paso5;
    public TMP_Text paso5_currentLinea;
    public TMP_Text paso5_paradaInicial;
    public TMP_Text paso5_paradaFinal;
    public TMP_Text paso5_direccion;
    public TMP_Text paso5_paradasDiferencia;



    public GameObject datos;
    public TMP_Text tiempoEstimado;
    public TMP_Text paradasTotales;

    public GameObject pasoError;
    public bool isWayPossible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FetchInfo(int par1, string lin1, int par2, string lin2, string infoType)
    {
        transportType.text = infoType;
        infoTransportType = infoType.Trim();

        paradaPrimera = par1;
        lineaPrimera = lin1;

        paradaFinal = par2;
        lineaFinal = lin2;

        ContarParadas();
    }

    public void ContarParadas()
    {
        paso1.SetActive(false);
        paso1_5.SetActive(false);
        paso2.SetActive(false);
        paso2_5.SetActive(false);
        paso3.SetActive(false);
        paso3_5.SetActive(false);
        paso4.SetActive(false);
        paso4_5.SetActive(false);
        paso5.SetActive(false);


        m_all.SetActive(false);
        b_all.SetActive(false);
        r_all.SetActive(false);
        m_metroLineas.SetActive(false);
        recorridoViajes.SetActive(true);

        goBack goback = FindFirstObjectByType<goBack>();
        goback.OpenScreen();

        indicators indicators = FindFirstObjectByType<indicators>();
        indicators.AllToNormal();


        //el principio y el final están en la misma línea
        if (lineaPrimera == lineaFinal)
        {
            string location = "";
            string location_extra = "";
            int numbersToSubstract = 1;
            if (infoTransportType == "metro") { location = "lineas/"; numbersToSubstract = 1; }
            if (infoTransportType == "renfe") { location = "renfe/"; numbersToSubstract = 1; }
            if (infoTransportType == "bus_dir1") { transportType.text = "bus"; location = "buses/paradas/"; location_extra = "/dir1_pa"; numbersToSubstract = 0; }
            if (infoTransportType == "bus_dir2") { transportType.text = "bus"; location = "buses/paradas/"; location_extra = "/dir2_pa"; numbersToSubstract = 0; }

            TextAsset paradasEnLinea = Resources.Load<TextAsset>(location + lineaPrimera.Trim() + location_extra);
            string[] paradas = paradasEnLinea.text.Trim().Split('\n');

            string cleanText(string texto) => texto.Split('[')[0].Trim();

            paso1.SetActive(true);

            if (infoTransportType == "metro") { paso1_currentLinea.text = "Línea " + lineaPrimera; }
            if (infoTransportType == "renfe") { paso1_currentLinea.text = "" + lineaPrimera; }
            if (infoTransportType.Contains("bus")) { paso1_currentLinea.text = "Bus " + lineaPrimera; }
            paso1_paradaInicial.text = cleanText(paradas[paradaPrimera - numbersToSubstract]);
            paso1_paradaFinal.text = cleanText(paradas[paradaFinal - numbersToSubstract]);
            paso1_paradasDiferencia.text = Mathf.Abs(paradaPrimera - paradaFinal) + " paradas";


            paradasTotales.text = "Paradas totales: " + Mathf.Abs(paradaPrimera - paradaFinal);
            //tiempoEstimado.text = "Tiempo estimado: " + Mathf.Abs(paradaPrimera - paradaFinal) * 3 + "''";

            if ((Mathf.Abs(paradaPrimera - paradaFinal) * 3) > 60) {
                tiempoEstimado.text = "Tiempo estimado: 1h y " + ((Mathf.Abs(paradaPrimera - paradaFinal) * 3) - 60) + "''"; }
            else {
                tiempoEstimado.text = "Tiempo estimado: " + (Mathf.Abs(paradaPrimera - paradaFinal) * 3) + "''"; }


            if (paradaPrimera > paradaFinal)
            {
                if (transportType.text != "bus")
                {
                    paso1_direccion.text = "Dirección " + cleanText(paradas[0]);
                    direccion = "negative";
                }
                else //buses solo
                {
                    string municipiosPath = location_extra.Substring(0, location_extra.Length - 2); //quitar ultimas 2 letras

                    TextAsset municipiosRaw = Resources.Load<TextAsset>(location + lineaPrimera + municipiosPath + "mu");
                    string[] municipios = municipiosRaw.text.Split('\n');

                    paso1_direccion.text = "Dirección " + cleanText(municipios[0]);
                    direccion = "negative";
                }
            }
            else
            {
                if (transportType.text != "bus")
                {
                    paso1_direccion.text = "Dirección " + cleanText(paradas[paradas.Length - 1]);
                    direccion = "positive";
                }
                else //buses solo
                {
                    string municipiosPath = location_extra.Substring(0, location_extra.Length - 2); //quitar ultimas 2 letras

                    TextAsset municipiosRaw = Resources.Load<TextAsset>(location + lineaPrimera + municipiosPath + "mu");
                    string[] municipios = municipiosRaw.text.Split('\n');

                    paso1_direccion.text = "Dirección " + cleanText(municipios[municipios.Length - 1]);
                    direccion = "positive";
                }
            }

            if (paradaPrimera == paradaFinal) { paso1_direccion.text = ""; }


            ButtonMath(paradas.Length);
        }

        //hay que hacer transbordos
        else
        {
            List<string> rutaLineas = BuscarRuta(lineaPrimera, lineaFinal);

            if (rutaLineas != null && rutaLineas.Count > 1)
            {
                isWayPossible = true;

                paso1.SetActive(true);
                paso1_5.SetActive(true);
                paso2.SetActive(true);

                string location = "";
                string location_extra = "";
                int numbersToSubstract = 1;
                if (infoTransportType == "metro") { location = "lineas/"; numbersToSubstract = 1; }
                if (infoTransportType == "renfe") { location = "renfe/c"; numbersToSubstract = 1; }
                if (infoTransportType == "bus_dir1") { transportType.text = "bus"; location = "buses/paradas/"; location_extra = "/dir1_pa"; numbersToSubstract = 0; }
                if (infoTransportType == "bus_dir2") { transportType.text = "bus"; location = "buses/paradas/"; location_extra = "/dir2_pa"; numbersToSubstract = 0; }

                TextAsset paradasEnLineaPrimera = Resources.Load<TextAsset>(location + lineaPrimera.Trim() + location_extra);
                string[] paradasPrimera = paradasEnLineaPrimera.text.Split('\n');

                string siguienteLineaEnRuta = rutaLineas[1];
                TextAsset paradasEnLineaSegunda= Resources.Load<TextAsset>(location + lineaFinal + location_extra);
                string[] paradasSegunda = paradasEnLineaSegunda.text.Split('\n');

                string cleanText(string texto) => texto.Split('[')[0].Trim();


                if (infoTransportType == "metro") { paso1_currentLinea.text = "Línea " + lineaPrimera; }
                if (infoTransportType == "renfe") { paso1_currentLinea.text = "C" + lineaPrimera; }
                if (infoTransportType.Contains("bus")) { paso1_currentLinea.text = "Bus " + lineaPrimera; }


                paso1_paradaInicial.text = cleanText(paradasPrimera[paradaPrimera - numbersToSubstract]);

                int indexEnLineaPrimera = -1;
                for (int i = 0; i < paradasPrimera.Length; i++)
                {
                    if (paradasPrimera[i].Contains('[' + siguienteLineaEnRuta + ']'))
                    {
                        indexEnLineaPrimera = i;
                        break;
                    }
                }
                if (indexEnLineaPrimera != 1)
                {
                    paso1_paradaFinal.text = cleanText(paradasPrimera[indexEnLineaPrimera]);

                    int indexEnParadasFinal = -1;
                    for (int j = 0; j < paradasPrimera.Length; j++)
                    {
                        if (cleanText(paradasSegunda[j]) == paso1_paradaFinal.text)
                        {
                            indexEnParadasFinal = j;
                            break;
                        }
                    }


                    if (paradaPrimera > indexEnLineaPrimera + 1) { paso1_direccion.text = "Dirección " + cleanText(paradasPrimera[0]); direccion = "negative"; }
                    else { paso1_direccion.text = "Dirección " + cleanText(paradasPrimera[paradasPrimera.Length - 1]); direccion = "positive"; }

                    if (paso1_paradaInicial.text == paso1_paradaFinal.text) { paso1_direccion.text = ""; }


                    if ((indexEnParadasFinal + 1) > paradaFinal) { paso2_direccion.text = "Dirección " + cleanText(paradasSegunda[0]); }
                    else { paso2_direccion.text = "Dirección " + cleanText(paradasSegunda[paradasSegunda.Length - 1]); }

                    if (paso2_paradaInicial.text == paso2_paradaFinal.text) { paso2_direccion.text = ""; }



                    int dif1 = Mathf.Abs(paradaPrimera - (indexEnLineaPrimera + 1));
                    int dif2 = Mathf.Abs(paradaFinal - (indexEnParadasFinal + 1));
                    int totalParadas = dif1 + dif2;

                    paso1_paradasDiferencia.text = dif1 + " paradas";
                    paso2_paradasDiferencia.text = dif2 + " paradas";

                    paradasTotales.text = "Paradas totales: " + totalParadas;
                    if ((totalParadas * 3) > 60) { tiempoEstimado.text = "Tiempo estimado: 1h y " + ((totalParadas * 3) - 60) + "''"; }
                    else { tiempoEstimado.text = "Tiempo estimado: " + (totalParadas * 3) + "''"; }



                    //transbordo
                    paso1_5_text.text = "Transbordo a línea " + lineaFinal;
                    //fin transbordo


                    if (infoTransportType == "metro") { paso2_currentLinea.text = "Línea " + lineaFinal; }
                    if (infoTransportType == "renfe") { paso2_currentLinea.text = "C" + lineaFinal; }
                    if (infoTransportType.Contains("bus")) { paso2_currentLinea.text = "Bus " + lineaFinal; }
                    paso2_paradaInicial.text = paso1_paradaFinal.text;
                    paso2_paradaFinal.text = cleanText(paradasSegunda[paradaFinal - 1]);
                }
            }
        }
    }

    IEnumerator ErrorIfTripIsntPossible()
    {
        yield return new WaitForSeconds(1);

        //empezar de nuevo otra vez
        if (isWayPossible == false)
        {
            /*
            paso3.SetActive(true);
            paso2_5.SetActive(true);

            TextAsset paradasEnLineaPrimera = Resources.Load<TextAsset>("lineas/" + lineaPrimera);
            string[] paradasPrimera = paradasEnLineaPrimera.text.Split('\n');

            TextAsset paradasEnLineaFinal = Resources.Load<TextAsset>("lineas/" + lineaFinal);
            string[] paradasFinal = paradasEnLineaFinal.text.Split('\n');

            for (int i = 0; i < paradasPrimera.Length; i++) //busca en todas las paradas de la línea primera
            {
                for (int j = 0; i <= 12; j++)
                {
                    if (paradasPrimera[j].Contains("[" + j)) //busca en todas las paradas todas las líneas
                    {
                        if (paradasPrimera[j].Contains("[" + paradaFinal))
                        {
                            //isWayPossible = true;
                        }
                    }

                }
            }
            */
        }

        yield return new WaitForSeconds(1);

        if (isWayPossible == false)
        {
            paso1.SetActive(false);
            paso2.SetActive(false);
            paso1_5.SetActive(false);
            pasoError.SetActive(true);
        }
    }
    
    
    public void SaveRouteToFavs()
    {
        favorites favorites = FindFirstObjectByType<favorites>();
        favorites.SaveRoute(paradaPrimera, lineaPrimera, paradaFinal, lineaFinal, infoTransportType);
    }

    public void GetRouteFromFavs(int paradaInicial, string lineaInicial, int paradaFinal2, string lineaFinal2, string type)
    {
        paradaPrimera = paradaInicial;
        lineaPrimera = lineaInicial;
        paradaFinal = paradaFinal2;
        lineaFinal = lineaFinal2;

        //lineaNumb = lineaFinal2;
        //ParadaNumb = paradaFinal2;

        //isContarParadas = true;

        transportType.text = type;
        infoTransportType = type;
        ContarParadas();
    }


    public void ContarParadasReset()
    {
        //isContarParadas = false;
        lineaPrimera = "0";
        paradaPrimera = 0;
        lineaFinal = "0";
        paradaFinal = 0;

        metro metro = FindFirstObjectByType<metro>();
        metro.ContarParadasReset();

        renfe renfe = FindFirstObjectByType<renfe>();
        renfe.ContarParadasReset();

        bus bus = FindFirstObjectByType<bus>();
        bus.ContarParadasReset();

        indicators indicators = FindFirstObjectByType<indicators>();
        indicators.AllToNormal();
    }




    void ButtonMath(int finalLenght)
    {
        cantContinueNextButton.SetActive(false);
        siguienteButton.SetActive(true);


        /*   -- comentado porque resuelve el "A-T9" --
        if (paradaFinal == finalLenght - 1) { siguienteButton.SetActive(false); cantContinueNextButton.SetActive(true);
            cantContinueText.text = "No hay más paradas en la línea."; }
        else { siguienteButton.SetActive(true); cantContinueNextButton.SetActive(false); }
        */


        if ((paso1_paradaInicial.text == paso1_paradaFinal.text) && paso1_5.activeInHierarchy == true) { siguienteText.text = "Hacer transbordo"; }
        else { siguienteText.text = "Siguiente parada"; }

        if ((paso1_paradaInicial.text == paso1_paradaFinal.text) && paso1_5.activeInHierarchy == false) { siguienteButton.SetActive(false); 
            cantContinueNextButton.SetActive(true); cantContinueText.text = "Ha llegado a su destino."; Debug.Log("trayecto destino"); }
    }

    public void NextStop()
    {
        if (paso1_paradaInicial.text == paso1_paradaFinal.text)
        {
            lineaPrimera = lineaFinal;

            TextAsset paradasEnLinea = Resources.Load<TextAsset>("lineas/" + lineaFinal);
            string[] paradas = paradasEnLinea.text.Split('\n');

            string cleanText(string texto) => texto.Split('[')[0].Trim();

            for (int i = 0; i < paradas.Length; i++)
            {
                if (paso2_paradaInicial.text == cleanText(paradas[i]))
                {
                    paradaPrimera = i + 1;
                    ContarParadas();
                    break;
                }
            }
        }
        else
        {
            if (direccion == "positive")
            {
                paradaPrimera = paradaPrimera + 1;
            }
            else if (direccion == "negative")
            {
                paradaPrimera = paradaPrimera - 1;
            }
            ContarParadas();
        }
    }
    /*
    public void PrevStop()
    {
        paradaPrimera = paradaPrimera - 1;
        ContarParadas();
    }
    */


    public List<string> BuscarRuta(string lineaPrimera, string lineaFinal)
    {
        Queue<List<string>> cola = new Queue<List<string>>();
        HashSet<string> visitadas = new HashSet<string>();

        cola.Enqueue(new List<string> { lineaPrimera });
        visitadas.Add(lineaPrimera);

        while (cola.Count > 0)
        {
            List<string> ruta = cola.Dequeue();
            string lineaActual = ruta[ruta.Count - 1];

            if (lineaActual == lineaFinal)
            {
                return ruta;
            }

            List<string> neighbors = GetCorrespondencies(lineaActual);
            foreach (string neighbor in neighbors)
            {
                if (visitadas.Contains(neighbor) == false)
                {
                    visitadas.Add(neighbor);
                    List<string> nuevaRuta = new List<string>(ruta);
                    nuevaRuta.Add(neighbor);
                    cola.Enqueue(nuevaRuta);
                }
            }
        }
        return null;
    }
    public List<string> GetCorrespondencies(string linea)
    {
        List<string> conexiones = new List<string>();

        string location = "";
        string location_extra = "";
        int numbersToSubstract = 1;
        if (infoTransportType == "metro") { location = "lineas/"; numbersToSubstract = 1; }
        if (infoTransportType == "renfe") { location = "renfe/c"; numbersToSubstract = 1; }
        if (infoTransportType == "bus_dir1") { transportType.text = "bus"; location = "buses/paradas/"; location_extra = "/dir1_pa"; numbersToSubstract = 0; }
        if (infoTransportType == "bus_dir2") { transportType.text = "bus"; location = "buses/paradas/"; location_extra = "/dir2_pa"; numbersToSubstract = 0; }

        TextAsset paradasEnLinea = Resources.Load<TextAsset>(location + linea + location_extra);
        string[] paradas = paradasEnLinea.text.Split('\n');

        foreach (string parada in paradas)
        {
            int indexCorcheteAbrir = parada.IndexOf('[');

            while (indexCorcheteAbrir != -1)
            {
                int indexCorcheteCerrar = parada.IndexOf(']', indexCorcheteAbrir);

                if (indexCorcheteCerrar != -1)
                {
                    string lineaCorrespondencia = parada.Substring(indexCorcheteAbrir + 1, indexCorcheteCerrar - indexCorcheteAbrir - 1).Trim();

                    if (conexiones.Contains(lineaCorrespondencia) == false && lineaCorrespondencia != linea)
                    {
                        conexiones.Add(lineaCorrespondencia);
                    }
                    indexCorcheteAbrir = parada.IndexOf('[', indexCorcheteCerrar);
                }
                else
                {
                    break;
                }
            }
        }

        return conexiones;
    }
}
