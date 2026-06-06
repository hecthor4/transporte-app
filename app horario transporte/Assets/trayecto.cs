using System.Collections;
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
    public int lineaPrimera;
    public int paradaPrimera;
    public int lineaFinal;
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

    public void FetchInfo(int par1, int lin1, int par2, int lin2, string infoType)
    {
        transportType.text = infoType;

        paradaPrimera = par1;
        lineaPrimera = lin1;

        paradaFinal = par2;
        lineaFinal = lin2;

        ContarParadas();
    }

    public void ContarParadas()
    {
        paso1.SetActive(false);
        paso2.SetActive(false);
        paso1_5.SetActive(false);
        paso3.SetActive(false);
        paso2_5.SetActive(false);


        m_all.SetActive(false);
        r_all.SetActive(false);
        m_metroLineas.SetActive(false);
        recorridoViajes.SetActive(true);

        goBack goback = FindFirstObjectByType<goBack>();
        goback.OpenScreen();


        //el principio y el final están en la misma línea
        if (lineaPrimera == lineaFinal)
        {
            string location = "";
            if (transportType.text == "metro") { location = "lineas/"; }
            if (transportType.text == "renfe") { location = "renfe/c"; }
            if (transportType.text == "bus") { location = "buses/paradas/"; }

            TextAsset paradasEnLinea = Resources.Load<TextAsset>(location + lineaPrimera);
            string[] paradas = paradasEnLinea.text.Split('\n');

            string cleanText(string texto) => texto.Split('[')[0].Trim();

            paso1.SetActive(true);

            if (transportType.text == "metro") { paso1_currentLinea.text = "Línea " + lineaPrimera; }
            if (transportType.text == "renfe") { paso1_currentLinea.text = "C" + lineaPrimera; }
            paso1_paradaInicial.text = cleanText(paradas[paradaPrimera - 1]);
            paso1_paradaFinal.text = cleanText(paradas[paradaFinal - 1]);
            paso1_paradasDiferencia.text = Mathf.Abs(paradaPrimera - paradaFinal) + " paradas";

            paradasTotales.text = "Paradas totales: " + Mathf.Abs(paradaPrimera - paradaFinal);
            tiempoEstimado.text = "Tiempo estimado: " + Mathf.Abs(paradaPrimera - paradaFinal) * 3 + "''";

            if (paradaPrimera > paradaFinal)
            {
                paso1_direccion.text = "Dirección " + cleanText(paradas[0]);
                direccion = "negative";
            }
            else
            {
                paso1_direccion.text = "Dirección " + cleanText(paradas[paradas.Length - 1]);
                direccion = "positive";
            }


            ButtonMath(paradas.Length);
        }

        //hay que hacer transbordos
        else if (transportType.text != "renfe")
        {
            TextAsset paradasEnLineaPrimera = Resources.Load<TextAsset>("lineas/" + lineaPrimera);
            string[] paradasPrimera = paradasEnLineaPrimera.text.Split('\n');

            TextAsset paradasEnLineaFinal = Resources.Load<TextAsset>("lineas/" + lineaFinal);
            string[] paradasFinal = paradasEnLineaFinal.text.Split('\n');

            paso1.SetActive(true);
            paso1_5.SetActive(true);
            paso2.SetActive(true);

            string cleanText(string texto) => texto.Split('[')[0].Trim();

            paso1_currentLinea.text = "Línea " + lineaPrimera;
            paso1_paradaInicial.text = cleanText(paradasPrimera[paradaPrimera - 1]);

            isWayPossible = false;
            for (int i = 0; i < paradasPrimera.Length; i++)
            {
                //si en la linea final hay parada de la linea inicial
                if (paradasPrimera[i].Contains("[" + lineaFinal + "]"))
                {
                    paso1_paradaFinal.text = cleanText(paradasPrimera[i]);
                    isWayPossible = true;

                    //buscar index en linea final
                    int indexEnLineaFinal = -1;
                    for (int j = 0; j < paradasFinal.Length; j++)
                    {
                        if (cleanText(paradasFinal[j]) == paso1_paradaFinal.text)
                        {
                            indexEnLineaFinal = j;
                            break;
                        }
                    }

                    //si no se encuentra el index
                    if (indexEnLineaFinal == -1)
                    {
                        continue;
                    }

                    if (paradaPrimera > i + 1) { paso1_direccion.text = "Dirección " + cleanText(paradasPrimera[0]); direccion = "negative"; }
                    else { paso1_direccion.text = "Dirección " + cleanText(paradasPrimera[paradasPrimera.Length - 1]); direccion = "positive"; }

                    if ((indexEnLineaFinal + 1) > paradaFinal) { paso2_direccion.text = "Dirección " + cleanText(paradasFinal[0]); }
                    else { paso2_direccion.text = "Dirección " + cleanText(paradasFinal[paradasFinal.Length - 1]); }


                    int dif1 = Mathf.Abs(paradaPrimera - (i + 1));
                    int dif2 = Mathf.Abs(paradaFinal - (indexEnLineaFinal + 1));
                    int totalParadas = dif1 + dif2;

                    paso1_paradasDiferencia.text = dif1 + " paradas";
                    paso2_paradasDiferencia.text = dif2 + " paradas";

                    paradasTotales.text = "Paradas totales: " + totalParadas;
                    if ((totalParadas * 3) > 60)
                    {
                        tiempoEstimado.text = "Tiempo estimado: 1h y " + ((totalParadas * 3) - 60) + "''";
                    }
                    else
                    {
                        tiempoEstimado.text = "Tiempo estimado: " + (totalParadas * 3) + "''";
                    }

                    break;
                }
            }
            StartCoroutine(ErrorIfTripIsntPossible());

            //transbordo
            paso1_5_text.text = "Transbordo a línea " + lineaFinal;
            //fin transbordo


            paso2_currentLinea.text = "Línea " + lineaFinal;
            paso2_paradaInicial.text = paso1_paradaFinal.text;
            paso2_paradaFinal.text = cleanText(paradasFinal[paradaFinal - 1]);


            ButtonMath(paradasFinal.Length);
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
        favorites.SaveRoute(paradaPrimera, lineaPrimera, paradaFinal, lineaFinal, transportType.text);
    }

    public void GetRouteFromFavs(int paradaInicial, int lineaInicial, int paradaFinal2, int lineaFinal2, string type)
    {
        paradaPrimera = paradaInicial;
        lineaPrimera = lineaInicial;
        paradaFinal = paradaFinal2;
        lineaFinal = lineaFinal2;

        //lineaNumb = lineaFinal2;
        //ParadaNumb = paradaFinal2;

        //isContarParadas = true;

        transportType.text = type;
        ContarParadas();
    }


    public void ContarParadasReset()
    {
        //isContarParadas = false;
        lineaPrimera = 0;
        paradaPrimera = 0;
        lineaFinal = 0;
        paradaFinal = 0;

        metro metro = FindFirstObjectByType<metro>();
        metro.ContarParadasReset();

        renfe renfe = FindFirstObjectByType<renfe>();
        renfe.ContarParadasReset();
    }




    void ButtonMath(int finalLenght)
    {
        if (paradaFinal == finalLenght - 1) { siguienteButton.SetActive(false); cantContinueNextButton.SetActive(true);
            cantContinueText.text = "No hay más paradas en la línea."; }
        else { siguienteButton.SetActive(true); cantContinueNextButton.SetActive(false); }

        if ((paso1_paradaInicial.text == paso1_paradaFinal.text) && paso1_5.activeInHierarchy == true) { siguienteText.text = "Hacer transbordo"; }
        else { siguienteText.text = "Siguiente parada"; }

        if ((paso1_paradaInicial.text == paso1_paradaFinal.text) && paso1_5.activeInHierarchy == false) { siguienteButton.SetActive(false); 
            cantContinueNextButton.SetActive(true); cantContinueText.text = "Ha llegado a su destino."; }
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
}
