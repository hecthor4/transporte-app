using System.Collections;
using TMPro;
using UnityEngine;

public class metro : MonoBehaviour
{
    public TMP_Text metroLinea;

    public GameObject parada1;
    public GameObject parada2;
    public GameObject parada3;
    public GameObject parada4;
    public GameObject parada5;
    public GameObject parada6;
    public GameObject parada7;
    public GameObject parada8;
    public GameObject parada9;
    public GameObject parada10;
    public GameObject parada11;
    public GameObject parada12;
    public GameObject parada13;
    public GameObject parada14;
    public GameObject parada15;
    public GameObject parada16;
    public GameObject parada17;
    public GameObject parada18;
    public GameObject parada19;
    public GameObject parada20;
    public GameObject parada21;
    public GameObject parada22;
    public GameObject parada23;
    public GameObject parada24;
    public GameObject parada25;
    public GameObject parada26;
    public GameObject parada27;
    public GameObject parada28;
    public GameObject parada29;
    public GameObject parada30;
    public GameObject parada31;
    public GameObject parada32;
    public GameObject parada33;
    public GameObject parada34;
    public GameObject parada35;
    public GameObject parada36;
    public GameObject parada37;
    public GameObject parada38;
    public GameObject parada39;
    public GameObject parada40;

    public TMP_Text parada1_text;
    public TMP_Text parada2_text;
    public TMP_Text parada3_text;
    public TMP_Text parada4_text;
    public TMP_Text parada5_text;
    public TMP_Text parada6_text;
    public TMP_Text parada7_text;
    public TMP_Text parada8_text;
    public TMP_Text parada9_text;
    public TMP_Text parada10_text;
    public TMP_Text parada11_text;
    public TMP_Text parada12_text;
    public TMP_Text parada13_text;
    public TMP_Text parada14_text;
    public TMP_Text parada15_text;
    public TMP_Text parada16_text;
    public TMP_Text parada17_text;
    public TMP_Text parada18_text;
    public TMP_Text parada19_text;
    public TMP_Text parada20_text;
    public TMP_Text parada21_text;
    public TMP_Text parada22_text;
    public TMP_Text parada23_text;
    public TMP_Text parada24_text;
    public TMP_Text parada25_text;
    public TMP_Text parada26_text;
    public TMP_Text parada27_text;
    public TMP_Text parada28_text;
    public TMP_Text parada29_text;
    public TMP_Text parada30_text;
    public TMP_Text parada31_text;
    public TMP_Text parada32_text;
    public TMP_Text parada33_text;
    public TMP_Text parada34_text;
    public TMP_Text parada35_text;
    public TMP_Text parada36_text;
    public TMP_Text parada37_text;
    public TMP_Text parada38_text;
    public TMP_Text parada39_text;
    public TMP_Text parada40_text;

    public int lineaNumb;
    public int ParadaNumb;

    public bool isContarParadas = false;
    public GameObject selection;
    public TMP_Text selection_parada;
    public GameObject selection_corresObj;
    public TMP_Text selection_corres;
    public TMP_Text selection_contarParadas;

    public int lineaPrimera;
    public int paradaPrimera;
    public int lineaFinal;
    public int paradaFinal;

    public GameObject m_all;
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

    public void ContarParadas()
    {
        paso1.SetActive(false);
        paso2.SetActive(false);
        paso1_5.SetActive(false);
        paso3.SetActive(false);
        paso2_5.SetActive(false);

        if (isContarParadas == true)
        {
            m_all.SetActive(false);
            m_metroLineas.SetActive(false);
            recorridoViajes.SetActive(true);

            lineaFinal = lineaNumb;
            paradaFinal = ParadaNumb;


            //el principio y el final están en la misma línea
            if (lineaPrimera == lineaFinal)
            {
                TextAsset paradasEnLinea = Resources.Load<TextAsset>("lineas/" + lineaNumb);
                string[] paradas = paradasEnLinea.text.Split('\n');

                paso1.SetActive(true);
                
                paso1_currentLinea.text = "Línea " + lineaPrimera;
                paso1_paradaInicial.text = paradas[paradaPrimera - 1];
                paso1_paradaFinal.text = paradas[paradaFinal - 1];
                paso1_paradasDiferencia.text = Mathf.Abs(paradaPrimera - paradaFinal) + " paradas";

                paradasTotales.text = "Paradas totales: " + Mathf.Abs(paradaPrimera - paradaFinal);
                tiempoEstimado.text = "Tiempo estimado: " + Mathf.Abs(paradaPrimera - paradaFinal) * 3 + "''";

                if (paradaPrimera > paradaFinal)
                {
                    paso1_direccion.text = "Dirección " + paradas[0];
                }
                else
                {
                    paso1_direccion.text = "Dirección " + paradas[paradas.Length - 1];
                }
            }

            //hay que hacer transbordos
            else
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
                    if (paradasPrimera[i].Contains("[" + lineaFinal))
                    {
                        paso1_paradaFinal.text = cleanText(paradasPrimera[i]);
                        isWayPossible = true;

                        if (paradaPrimera > i) { paso1_direccion.text = "Dirección " + cleanText(paradasPrimera[0]); }
                        else { paso1_direccion.text = "Dirección " + cleanText(paradasPrimera[paradasPrimera.Length - 1]); }

                        if (i > paradaFinal) { paso2_direccion.text = "Dirección " + cleanText(paradasFinal[0]); }
                        else { paso2_direccion.text = "Dirección " + cleanText(paradasFinal[paradasFinal.Length - 1]); }

                        paso1_paradasDiferencia.text = Mathf.Abs(paradaPrimera - i) + " paradas";
                        paso2_paradasDiferencia.text = Mathf.Abs(paradaFinal - i) + " paradas";

                        paradasTotales.text = "Paradas totales: " + (Mathf.Abs(paradaPrimera - i) + Mathf.Abs(paradaFinal - i));
                        if ((((Mathf.Abs(paradaPrimera - i) + Mathf.Abs(paradaFinal - i)) * 2) + 3) > 60) {
                            tiempoEstimado.text = "Tiempo estimado: 1h y " + (((Mathf.Abs(paradaPrimera - i) + Mathf.Abs(paradaFinal - i)) * 2) - 63) + "''"; }
                        else {
                            tiempoEstimado.text = "Tiempo estimado: " + ((Mathf.Abs(paradaPrimera - i) + Mathf.Abs(paradaFinal - i)) * 3) + "''"; }
                    }
                }
                StartCoroutine(ErrorIfTripIsntPossible());

                //transbordo
                paso1_5_text.text = "Transbordo a línea " + lineaFinal;
                //fin transbordo


                paso2_currentLinea.text = "Línea " + lineaFinal;
                paso2_paradaInicial.text = paso1_paradaFinal.text;
                paso2_paradaFinal.text = cleanText(paradasFinal[paradaFinal - 1]);
            }
        }
        
        if (isContarParadas == false)
        {
            isContarParadas = true;
            m_all.SetActive(false);
            m_metroLineas.SetActive(true);
            recorridoViajes.SetActive(false);
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
        favorites.SaveRoute(paradaPrimera, lineaPrimera, paradaFinal, lineaFinal);
    }

    public void GetRouteFromFavs(int paradaInicial, int lineaInicial, int paradaFinal2, int lineaFinal2)
    {
        paradaPrimera = paradaInicial;
        lineaPrimera = lineaInicial;
        paradaFinal = paradaFinal2;
        lineaFinal = lineaFinal2;

        lineaNumb = paradaFinal2;
        ParadaNumb = paradaFinal2;

        isContarParadas = true;
        ContarParadas();
    }


    public void ContarParadasReset()
    {
        isContarParadas = false;
        lineaPrimera = 0;
        paradaPrimera = 0;
        lineaFinal = 0;
        paradaFinal = 0;
    }

    public void Selection(int paradaNumb2)
    {
        metroLinea.text = "Línea " + lineaNumb;

        TextAsset paradasEnLinea = Resources.Load<TextAsset>("lineas/" + lineaNumb);
        string[] paradas = paradasEnLinea.text.Split('\n');


        ParadaNumb = paradaNumb2;
        selection.SetActive(true);

        int indice = paradas[paradaNumb2 - 1].IndexOf('[');
        string parada_texto;
        string corres_texto;

        if (indice != -1)
        {
            parada_texto = paradas[paradaNumb2 - 1].Substring(0, indice).Trim();
            corres_texto = paradas[paradaNumb2 - 1].Substring(indice);

            selection_corresObj.SetActive(true);
            selection_corres.text = corres_texto;
        }
        else
        {
            parada_texto = paradas[paradaNumb2 - 1].Trim();

            selection_corres.text = "";
            selection_corresObj.SetActive(false);
        }

        selection_parada.text = parada_texto;


        if (isContarParadas == false)
        {
            selection_contarParadas.text = "Viajar desde aquí hasta...";
            lineaPrimera = lineaNumb;
            paradaPrimera = paradaNumb2;
        }
        if (isContarParadas == true)
        {
            selection_contarParadas.text = "Viajar hasta aquí";
        }
    }

    public void Iterate(int lineaNumb1)
    {
        metroLinea.text = "Línea " + lineaNumb1;
        lineaNumb = lineaNumb1;

        TextAsset paradasEnLinea = Resources.Load<TextAsset>("lineas/" + lineaNumb);
        string[] paradas = paradasEnLinea.text.Split('\n');

        TMP_Text[] paradasTexts = { parada1_text, parada2_text, parada3_text, parada4_text, parada5_text, parada6_text, parada7_text, parada8_text, parada9_text, parada10_text, parada11_text, parada12_text, parada13_text, parada14_text, parada15_text, parada16_text, parada17_text, parada18_text, parada19_text, parada20_text, parada21_text, parada22_text, parada23_text, parada24_text, parada25_text, parada26_text, parada27_text, parada28_text, parada29_text, parada30_text, parada31_text, parada32_text, parada33_text, parada34_text, parada35_text, parada36_text, parada37_text, parada38_text, parada39_text, parada40_text };
        GameObject[] paradasObj = { parada1, parada2, parada3, parada4, parada5, parada6, parada7, parada8, parada9, parada10, parada11, parada12, parada13, parada14, parada15, parada16, parada17, parada18, parada19, parada20, parada21, parada22, parada23, parada24, parada25, parada26, parada27, parada28, parada29, parada30, parada31, parada32, parada33, parada34, parada35, parada36, parada37, parada38, parada39, parada40 };

        for (int i = 0; i < paradas.Length; i++)
        {
            paradasObj[i].SetActive(true);
            paradasTexts[i].text = paradas[i].Trim();
        }
        for (int i = paradas.Length; i < 40; i++)
        {
            paradasObj[i].SetActive(false);
        }
    }
}
