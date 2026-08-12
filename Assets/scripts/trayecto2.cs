using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class trayecto2 : MonoBehaviour
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
    public bool isWayPossible = false;

    public GameObject m_all;
    public GameObject r_all;
    public GameObject b_all;
    public GameObject m_metroLineas;
    public GameObject recorridoViajes;
    public GameObject pasoError;

    public GameObject[] pasosVisuales;
    public GameObject[] pasosTransbordoVisuales;

    public TMP_Text[] pasoCurrentLinea;
    public TMP_Text[] pasoParadaInicial;
    public TMP_Text[] pasoParadaFinal;
    public TMP_Text[] pasoDireccion;
    public TMP_Text[] pasoParadasDiferencia;
    public TMP_Text[] pasoTransbordoText;

    public GameObject datos;
    public TMP_Text tiempoEstimado;
    public TMP_Text paradasTotales;

    public void FetchInfo(int par1, string lin1, int par2, string lin2, string infoType)
    {
        transportType.text = infoType;
        infoTransportType = infoType;
        paradaPrimera = par1;
        lineaPrimera = lin1;
        paradaFinal = par2;
        lineaFinal = lin2;
        ContarParadas();


        m_all.SetActive(false);
        b_all.SetActive(false);
        r_all.SetActive(false);
        m_metroLineas.SetActive(false);
        recorridoViajes.SetActive(true);

        goBack goback = FindFirstObjectByType<goBack>();
        goback.OpenScreen();
    }

    private (string path, string extra, int substract) GetConfig()
    {
        if (infoTransportType == "metro") return ("lineas/", "", 1);
        if (infoTransportType == "renfe") return ("renfe/c", "", 1);
        if (infoTransportType == "bus_dir1") return ("buses/paradas/", "/dir1_pa", 0);
        if (infoTransportType == "bus_dir2") return ("buses/paradas/", "/dir2_pa", 0);
        return ("lineas/", "", 1);
    }

    private string CleanText(string texto) => texto.Split('[')[0].Trim();

    public void ContarParadas()
    {
        foreach (var p in pasosVisuales) p.SetActive(false);
        foreach (var t in pasosTransbordoVisuales) t.SetActive(false);


        indicators indicators = FindFirstObjectByType<indicators>();
        indicators.AllToNormal();

        List<string> rutaLineas = BuscarRuta(lineaPrimera, lineaFinal);

        if (rutaLineas == null || rutaLineas.Count == 0)
        {
            isWayPossible = false;
            StartCoroutine(ErrorIfTripIsntPossible());
            return;
        }

        isWayPossible = true;
        var config = GetConfig();
        int totalParadasAcumuladas = 0;

        for (int i = 0; i < rutaLineas.Count; i++)
        {
            if (i >= pasosVisuales.Length) break;

            string lineaActual = rutaLineas[i];
            pasosVisuales[i].SetActive(true);

            if (infoTransportType == "metro") pasoCurrentLinea[i].text = "Línea " + lineaActual;
            else if (infoTransportType == "renfe") pasoCurrentLinea[i].text = "C" + lineaActual;
            else pasoCurrentLinea[i].text = "Bus " + lineaActual;

            TextAsset fileAsset = Resources.Load<TextAsset>(config.path + lineaActual + config.extra);
            string[] paradas = fileAsset.text.Split('\n');

            int idxInicial = 0;
            int idxFinal = 0;

            if (i == 0)
            {
                idxInicial = paradaPrimera - config.substract;
            }
            else
            {
                idxInicial = BuscarIndexPorNombre(paradas, pasoParadaFinal[i - 1].text);
            }

            if (i == rutaLineas.Count - 1)
            {
                idxFinal = paradaFinal - config.substract;
            }
            else
            {
                string siguienteLinea = rutaLineas[i + 1];
                idxFinal = BuscarIndexPorCorrespondencia(paradas, siguienteLinea);
            }

            idxInicial = Mathf.Clamp(idxInicial, 0, paradas.Length - 1);
            idxFinal = Mathf.Clamp(idxFinal, 0, paradas.Length - 1);

            pasoParadaInicial[i].text = CleanText(paradas[idxInicial]);
            pasoParadaFinal[i].text = CleanText(paradas[idxFinal]);

            int difParadas = Mathf.Abs(idxInicial - idxFinal);
            pasoParadasDiferencia[i].text = difParadas + " paradas";
            totalParadasAcumuladas += difParadas;

            CalcularDireccionTramo(i, idxInicial, idxFinal, paradas, lineaActual, config.path, config.extra);

            if (i < rutaLineas.Count - 1 && i < pasosTransbordoVisuales.Length)
            {
                pasosTransbordoVisuales[i].SetActive(true);
                pasoTransbordoText[i].text = "Transbordo a línea " + rutaLineas[i + 1];
            }
        }

        paradasTotales.text = "Paradas totales: " + totalParadasAcumuladas;
        int tiempoMinutos = totalParadasAcumuladas * 3;
        if (tiempoMinutos > 60)
        {
            tiempoEstimado.text = $"Tiempo estimado: {tiempoMinutos / 60}h y {tiempoMinutos % 60}''";
        }
        else
        {
            tiempoEstimado.text = $"Tiempo estimado: {tiempoMinutos}''";
        }

        ButtonMath(totalParadasAcumuladas);
    }

    private int BuscarIndexPorNombre(string[] paradas, string nombreBuscar)
    {
        for (int i = 0; i < paradas.Length; i++)
        {
            if (CleanText(paradas[i]) == nombreBuscar) return i;
        }
        return 0;
    }

    private int BuscarIndexPorCorrespondencia(string[] paradas, string lineaSiguiente)
    {
        for (int i = 0; i < paradas.Length; i++)
        {
            if (paradas[i].Contains("[" + lineaSiguiente + "]")) return i;
        }
        return 0;
    }

    private void CalcularDireccionTramo(int pasoIdx, int idxInicial, int idxFinal, string[] paradas, string linea, string path, string extra)
    {
        if (idxInicial == idxFinal)
        {
            pasoDireccion[pasoIdx].text = "";
            if (pasoIdx == 0) direccion = "none";
            return;
        }

        bool isNegative = idxInicial > idxFinal;
        if (pasoIdx == 0) direccion = isNegative ? "negative" : "positive"; // Mantiene compatibilidad con el sistema original

        if (!infoTransportType.Contains("bus"))
        {
            int targetIdx = isNegative ? 0 : paradas.Length - 1;
            pasoDireccion[pasoIdx].text = "Dirección " + CleanText(paradas[targetIdx]);
        }
        else
        {
            string cleanExtra = extra.Replace("/dir1_pa", "/dir1_").Replace("/dir2_pa", "/dir2_");
            TextAsset municipiosRaw = Resources.Load<TextAsset>(path + linea + cleanExtra + "mu");

            if (municipiosRaw != null)
            {
                string[] municipios = municipiosRaw.text.Split('\n');
                int targetMuni = isNegative ? 0 : municipios.Length - 1;
                pasoDireccion[pasoIdx].text = "Dirección " + CleanText(municipios[targetMuni]);
            }
            else
            {
                int targetIdx = isNegative ? 0 : paradas.Length - 1;
                pasoDireccion[pasoIdx].text = "Dirección " + CleanText(paradas[targetIdx]);
            }
        }
    }

    IEnumerator ErrorIfTripIsntPossible()
    {
        yield return new WaitForSeconds(1);
        if (!isWayPossible)
        {
            pasoError.SetActive(true);
        }
    }

    public List<string> BuscarRuta(string inicio, string destino)
    {
        if (inicio == destino) return new List<string> { inicio };

        Queue<List<string>> cola = new Queue<List<string>>();
        HashSet<string> visitadas = new HashSet<string>();

        cola.Enqueue(new List<string> { inicio });
        visitadas.Add(inicio);

        while (cola.Count > 0)
        {
            List<string> ruta = cola.Dequeue();
            string lineaActual = ruta[ruta.Count - 1];

            if (lineaActual == destino) return ruta;

            foreach (string vecino in GetCorrespondencies(lineaActual))
            {
                if (!visitadas.Contains(vecino))
                {
                    visitadas.Add(vecino);
                    List<string> nuevaRuta = new List<string>(ruta) { vecino };
                    cola.Enqueue(nuevaRuta);
                }
            }
        }
        return null;
    }

    public List<string> GetCorrespondencies(string linea)
    {
        List<string> conexiones = new List<string>();
        var config = GetConfig();

        TextAsset paradasEnLinea = Resources.Load<TextAsset>(config.path + linea + config.extra);
        if (paradasEnLinea == null) return conexiones;

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
                    if (!conexiones.Contains(lineaCorrespondencia) && lineaCorrespondencia != linea)
                    {
                        conexiones.Add(lineaCorrespondencia);
                    }
                    indexCorcheteAbrir = parada.IndexOf('[', indexCorcheteCerrar);
                }
                else break;
            }
        }
        return conexiones;
    }

    void ButtonMath(int totalParadas)
    {
        cantContinueNextButton.SetActive(false);
        siguienteButton.SetActive(true);

        bool transbordoActivo = ComprobarTransbordoActivo();

        if (pasoParadaInicial[0].text == pasoParadaFinal[pasosVisuales.Length - 1].text || totalParadas == 0)
        {
            siguienteButton.SetActive(false);
            cantContinueNextButton.SetActive(true);
            cantContinueText.text = "Ha llegado a su destino.";
        }
        else if (pasoParadaInicial[0].text == pasoParadaFinal[0].text && transbordoActivo)
        {
            siguienteText.text = "Hacer transbordo";
        }
        else
        {
            siguienteText.text = "Siguiente parada";
        }
    }

    private bool ComprobarTransbordoActivo()
    {
        foreach (var t in pasosTransbordoVisuales) if (t.activeInHierarchy) return true;
        return false;
    }

    public void NextStop()
    {
        if (pasoParadaInicial[0].text == pasoParadaFinal[0].text)
        {
            List<string> ruta = BuscarRuta(lineaPrimera, lineaFinal);
            if (ruta != null && ruta.Count > 1)
            {
                lineaPrimera = ruta[1];
                var config = GetConfig();
                TextAsset paradasEnLinea = Resources.Load<TextAsset>(config.path + lineaPrimera + config.extra);
                string[] paradas = paradasEnLinea.text.Split('\n');

                for (int i = 0; i < paradas.Length; i++)
                {
                    if (pasoParadaFinal[0].text == CleanText(paradas[i]))
                    {
                        paradaPrimera = i + 1;
                        break;
                    }
                }
                ContarParadas();
            }
        }
        else
        {
            if (direccion == "positive") paradaPrimera++;
            else if (direccion == "negative") paradaPrimera--;
            ContarParadas();
        }
    }

    public void ContarParadasReset()
    {
        lineaPrimera = "0";
        paradaPrimera = 0;
        lineaFinal = "0";
        paradaFinal = 0;
        FindFirstObjectByType<metro>()?.ContarParadasReset();
        FindFirstObjectByType<renfe>()?.ContarParadasReset();
        FindFirstObjectByType<bus>()?.ContarParadasReset();
        FindFirstObjectByType<indicators>()?.AllToNormal();
    }
}
