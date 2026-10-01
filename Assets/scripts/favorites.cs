using TMPro;
using UnityEngine;

public class favorites : MonoBehaviour
{
    //trayectos favoritos
    public string _1_name;
    public string _1_type;
    public string _1_lineaInici;
    public int _1_paradInici;
    public string _1_lineaFinal;
    public int _1_paradFinal;

    public string _2_name;
    public string _2_type;
    public string _2_lineaInici;
    public int _2_paradInici;
    public string _2_lineaFinal;
    public int _2_paradFinal;

    public string _3_name;
    public string _3_type;
    public string _3_lineaInici;
    public int _3_paradInici;
    public string _3_lineaFinal;
    public int _3_paradFinal;

    public string _4_name;
    public string _4_type;
    public string _4_lineaInici;
    public int _4_paradInici;
    public string _4_lineaFinal;
    public int _4_paradFinal;

    public string _5_name;
    public string _5_type;
    public string _5_lineaInici;
    public int _5_paradInici;
    public string _5_lineaFinal;
    public int _5_paradFinal;

    //lineas favoritas
    public string _f_l1_name;
    public string _f_l1_type;
    public string _f_l1_linea;

    public string _f_l2_name;
    public string _f_l2_type;
    public string _f_l2_linea;

    public string _f_l3_name;
    public string _f_l3_type;
    public string _f_l3_linea;

    public string _f_l4_name;
    public string _f_l4_type;
    public string _f_l4_linea;

    public string _f_l5_name;
    public string _f_l5_type;
    public string _f_l5_linea;


    //paradas favoritas
    public string _f_p1_name;
    public string _f_p1_parada;

    public string _f_p2_name;
    public string _f_p2_parada;

    public string _f_p3_name;
    public string _f_p3_parada;

    public string _f_p4_name;
    public string _f_p4_parada;

    public string _f_p5_name;
    public string _f_p5_parada;


    //check if its empty
    public bool is1stEmpty;
    public bool is2ndEmpty;
    public bool is3rdEmpty;
    public bool is4thEmpty;
    public bool is5thEmpty;
    public bool isf_l1_Empty;
    public bool isf_l2_Empty;
    public bool isf_l3_Empty;
    public bool isf_l4_Empty;
    public bool isf_l5_Empty;
    public bool isf_p1_Empty;
    public bool isf_p2_Empty;
    public bool isf_p3_Empty;
    public bool isf_p4_Empty;
    public bool isf_p5_Empty;


    //objetos en escena - parents
    public GameObject fav1;
    public GameObject fav2;
    public GameObject fav3;
    public GameObject fav4;
    public GameObject fav5;

    public GameObject f_l1;
    public GameObject f_l2;
    public GameObject f_l3;
    public GameObject f_l4;
    public GameObject f_l5;

    public GameObject f_p1;
    public GameObject f_p2;
    public GameObject f_p3;
    public GameObject f_p4;
    public GameObject f_p5;


    //objetos en escena - children
    public TMP_Text fav1_name;
    public TMP_Text fav1_text1;
    public TMP_Text fav1_text2;

    public TMP_Text fav2_name;
    public TMP_Text fav2_text1;
    public TMP_Text fav2_text2;

    public TMP_Text fav3_name;
    public TMP_Text fav3_text1;
    public TMP_Text fav3_text2;

    public TMP_Text fav4_name;
    public TMP_Text fav4_text1;
    public TMP_Text fav4_text2;

    public TMP_Text fav5_name;
    public TMP_Text fav5_text1;
    public TMP_Text fav5_text2;


    public TMP_Text f_l1_name;
    public TMP_Text f_l1_text1;

    public TMP_Text f_l2_name;
    public TMP_Text f_l2_text1;

    public TMP_Text f_l3_name;
    public TMP_Text f_l3_text1;

    public TMP_Text f_l4_name;
    public TMP_Text f_l4_text1;

    public TMP_Text f_l5_name;
    public TMP_Text f_l5_text1;


    public TMP_Text f_p1_name;
    public TMP_Text f_p1_code;

    public TMP_Text f_p2_name;
    public TMP_Text f_p2_code;

    public TMP_Text f_p3_name;
    public TMP_Text f_p3_code;

    public TMP_Text f_p4_name;
    public TMP_Text f_p4_code;

    public TMP_Text f_p5_name;
    public TMP_Text f_p5_code;


    //editing
    public int editingNumb;
    public bool isEditing;
    public TMP_InputField newName;


    //error message if no routes
    public GameObject noRoutesError;


    //screens to derivate to
    public GameObject r_all;
    public GameObject m_all;
    public GameObject b_all;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _1_name = PlayerPrefs.GetString("_1_name", "Default name");
        _1_type = PlayerPrefs.GetString("_1_type", "metro");
        _1_lineaInici = PlayerPrefs.GetString("_1_lineaInici", "0");
        _1_paradInici = PlayerPrefs.GetInt("_1_paradInici", 0);
        _1_lineaFinal = PlayerPrefs.GetString("_1_lineaFinal", "0");
        _1_paradFinal = PlayerPrefs.GetInt("_1_paradFinal", 0);

        _2_name = PlayerPrefs.GetString("_2_name", "Default name");
        _2_type = PlayerPrefs.GetString("_2_type", "metro");
        _2_lineaInici = PlayerPrefs.GetString("_2_lineaInici", "0");
        _2_paradInici = PlayerPrefs.GetInt("_2_paradInici", 0);
        _2_lineaFinal = PlayerPrefs.GetString("_2_lineaFinal", "0");
        _2_paradFinal = PlayerPrefs.GetInt("_2_paradFinal", 0);

        _3_name = PlayerPrefs.GetString("_3_name", "Default name");
        _3_type = PlayerPrefs.GetString("_3_type", "metro");
        _3_lineaInici = PlayerPrefs.GetString("_3_lineaInici", "0");
        _3_paradInici = PlayerPrefs.GetInt("_3_paradInici", 0);
        _3_lineaFinal = PlayerPrefs.GetString("_3_lineaFinal", "0");
        _3_paradFinal = PlayerPrefs.GetInt("_3_paradFinal", 0);

        _4_name = PlayerPrefs.GetString("_4_name", "Default name");
        _4_type = PlayerPrefs.GetString("_4_type", "metro");
        _4_lineaInici = PlayerPrefs.GetString("_4_lineaInici", "0");
        _4_paradInici = PlayerPrefs.GetInt("_4_paradInici", 0);
        _4_lineaFinal = PlayerPrefs.GetString("_4_lineaFinal", "0");
        _4_paradFinal = PlayerPrefs.GetInt("_4_paradFinal", 0);

        _5_name = PlayerPrefs.GetString("_5_name", "Default name");
        _5_type = PlayerPrefs.GetString("_5_type", "metro");
        _5_lineaInici = PlayerPrefs.GetString("_5_lineaInici", "0");
        _5_paradInici = PlayerPrefs.GetInt("_5_paradInici", 0);
        _5_lineaFinal = PlayerPrefs.GetString("_5_lineaFinal", "0");
        _5_paradFinal = PlayerPrefs.GetInt("_5_paradFinal", 0);


        _f_l1_name = PlayerPrefs.GetString("_f_l1_name", "Default name");
        _f_l1_type = PlayerPrefs.GetString("_f_l1_type", "metro");
        _f_l1_linea = PlayerPrefs.GetString("_f_l1_linea", "0");

        _f_l2_name = PlayerPrefs.GetString("_f_l2_name", "Default name");
        _f_l2_type = PlayerPrefs.GetString("_f_l2_type", "metro");
        _f_l2_linea = PlayerPrefs.GetString("_f_l2_linea", "0");

        _f_l3_name = PlayerPrefs.GetString("_f_l3_name", "Default name");
        _f_l3_type = PlayerPrefs.GetString("_f_l3_type", "metro");
        _f_l3_linea = PlayerPrefs.GetString("_f_l3_linea", "0");

        _f_l4_name = PlayerPrefs.GetString("_f_l4_name", "Default name");
        _f_l4_type = PlayerPrefs.GetString("_f_l4_type", "metro");
        _f_l4_linea = PlayerPrefs.GetString("_f_l4_linea", "0");

        _f_l5_name = PlayerPrefs.GetString("_f_l5_name", "Default name");
        _f_l5_type = PlayerPrefs.GetString("_f_l5_type", "metro");
        _f_l5_linea = PlayerPrefs.GetString("_f_l5_linea", "0");


        _f_p1_name = PlayerPrefs.GetString("_f_p1_name", "Default name");
        _f_p1_parada = PlayerPrefs.GetString("_f_p1_parada", "0");

        _f_p2_name = PlayerPrefs.GetString("_f_p2_name", "Default name");
        _f_p2_parada = PlayerPrefs.GetString("_f_p2_parada", "0");

        _f_p3_name = PlayerPrefs.GetString("_f_p3_name", "Default name");
        _f_p3_parada = PlayerPrefs.GetString("_f_p3_parada", "0");

        _f_p4_name = PlayerPrefs.GetString("_f_p4_name", "Default name");
        _f_p4_parada = PlayerPrefs.GetString("_f_p4_parada", "0");

        _f_p5_name = PlayerPrefs.GetString("_f_p5_name", "Default name");
        _f_p5_parada = PlayerPrefs.GetString("_f_p5_parada", "0");


        GetEmptySlots();
        RefreshData();
        noRoutesError.SetActive(true);
    }

    void GetEmptySlots()
    {
        if (_1_lineaFinal == "0" && _1_lineaFinal == "0" && _1_paradFinal == 0 && _1_paradFinal == 0) { 
            is1stEmpty = true; fav1.SetActive(false); }
        else {
            is1stEmpty = false; fav1.SetActive(true); }


        if (_2_lineaFinal == "0" && _2_lineaFinal == "0" && _2_paradFinal == 0 && _2_paradFinal == 0) { 
            is2ndEmpty = true; fav2.SetActive(false); }
        else {
            is2ndEmpty = false; fav2.SetActive(true); }


        if (_3_lineaFinal == "0" && _3_lineaFinal == "0" && _3_paradFinal == 0 && _3_paradFinal == 0) { 
            is3rdEmpty = true; fav3.SetActive(false); }
        else {
            is3rdEmpty = false; fav3.SetActive(true); }


        if (_4_lineaFinal == "0" && _4_lineaFinal == "0" && _4_paradFinal == 0 && _4_paradFinal == 0) { 
            is4thEmpty = true; fav4.SetActive(false); }
        else {
            is4thEmpty = false; fav4.SetActive(true); }


        if (_5_lineaFinal == "0" && _5_lineaFinal == "0" && _5_paradFinal == 0 && _5_paradFinal == 0) { 
            is5thEmpty = true; fav5.SetActive(false); }
        else {
            is5thEmpty = false; fav5.SetActive(true); }


        if (_f_l1_linea == "0") { isf_l1_Empty = true; f_l1.SetActive(false); }
        else { isf_l1_Empty = false; f_l1.SetActive(true); }

        if (_f_l2_linea == "0") { isf_l2_Empty = true; f_l2.SetActive(false); }
        else { isf_l2_Empty = false; f_l2.SetActive(true); }

        if (_f_l3_linea == "0") { isf_l3_Empty = true; f_l3.SetActive(false); }
        else { isf_l3_Empty = false; f_l3.SetActive(true); }

        if (_f_l4_linea == "0") { isf_l4_Empty = true; f_l4.SetActive(false); }
        else { isf_l4_Empty = false; f_l4.SetActive(true); }

        if (_f_l5_linea == "0") { isf_l5_Empty = true; f_l5.SetActive(false); }
        else { isf_l5_Empty = false; f_l5.SetActive(true); }


        if (_f_p1_parada == "0") { isf_p1_Empty = true; f_p1.SetActive(false); }
        else { isf_p1_Empty = false; f_p1.SetActive(true); }

        if (_f_p2_parada == "0") { isf_p2_Empty = true; f_p2.SetActive(false); }
        else { isf_p2_Empty = false; f_p2.SetActive(true); }

        if (_f_p3_parada == "0") { isf_p3_Empty = true; f_p3.SetActive(false); }
        else { isf_p3_Empty = false; f_p3.SetActive(true); }

        if (_f_p4_parada == "0") { isf_p4_Empty = true; f_p4.SetActive(false); }
        else { isf_p4_Empty = false; f_p4.SetActive(true); }

        if (_f_p5_parada == "0") { isf_p5_Empty = true; f_p5.SetActive(false); }
        else { isf_p5_Empty = false; f_p5.SetActive(true); }


        CheckForNoRouteError();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckForNoRouteError()
    {
        if (is1stEmpty == true && is2ndEmpty == true && is3rdEmpty == true && is4thEmpty == true && is5thEmpty == true &&
            isf_l1_Empty == true && isf_l2_Empty == true && isf_l3_Empty == true && isf_l4_Empty == true && isf_l5_Empty == true &&
            isf_p1_Empty == true && isf_p2_Empty == true && isf_p3_Empty == true && isf_p4_Empty == true && isf_p5_Empty == true)
        {
            noRoutesError.SetActive(true);
        }
        else
        {
            noRoutesError.SetActive(false);
        }
    }

    public void RefreshData()
    {
        CheckForNoRouteError();

        string cleanText(string texto) => texto.Split('[')[0].Trim();

        if (is1stEmpty == false)
        {
            string location = "";
            string location_extra = "";
            string lineaNomenclatura = "[error]";

            if (_1_type == "metro") { location = "lineas/"; lineaNomenclatura = "Metro: línea "; }
            if (_1_type == "renfe") { location = "renfe/c"; lineaNomenclatura = "C";  }
            if (_1_type == "bus_dir1") { location = "buses/paradas/"; location_extra = "/dir1_pa"; lineaNomenclatura = "Interurbano "; }
            if (_1_type == "bus_dir2") { location = "buses/paradas/"; location_extra = "/dir2_pa"; lineaNomenclatura = "Interurbano "; }

            TextAsset _1_paradasEnLineaPrimera = Resources.Load<TextAsset>(location + _1_lineaInici + location_extra);
            string[] _1_paradasPrimera = _1_paradasEnLineaPrimera.text.Split('\n');

            TextAsset _1_paradasEnLineaFinal = Resources.Load<TextAsset>(location + _1_lineaFinal + location_extra);
            string[] _1_paradasFinal = _1_paradasEnLineaFinal.text.Split('\n');

            fav1_name.text = _1_name;
            fav1_text1.text = cleanText(_1_paradasPrimera[_1_paradInici - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _1_lineaInici + ")";
            fav1_text2.text = cleanText(_1_paradasFinal[_1_paradFinal - 1].Trim()) + " <color=#BCBCBC>("+ lineaNomenclatura + _1_lineaFinal + ")";
        }

        if (is2ndEmpty == false)
        {
            string location = "";
            string location_extra = "";
            string lineaNomenclatura = "[error]";

            if (_2_type == "metro") { location = "lineas/"; lineaNomenclatura = "línea "; }
            if (_2_type == "renfe") { location = "renfe/c"; lineaNomenclatura = "C"; }
            if (_2_type == "bus_dir1") { location = "buses/paradas/"; location_extra = "/dir1_pa"; lineaNomenclatura = "Interurbano "; }
            if (_2_type == "bus_dir2") { location = "buses/paradas/"; location_extra = "/dir2_pa"; lineaNomenclatura = "Interurbano "; }

            TextAsset _2_paradasEnLineaPrimera = Resources.Load<TextAsset>(location + _2_lineaInici + location_extra);
            string[] _2_paradasPrimera = _2_paradasEnLineaPrimera.text.Split('\n');

            TextAsset _2_paradasEnLineaFinal = Resources.Load<TextAsset>(location + _2_lineaFinal + location_extra);
            string[] _2_paradasFinal = _2_paradasEnLineaFinal.text.Split('\n');

            fav2_name.text = _2_name;
            fav2_text1.text = cleanText(_2_paradasPrimera[_2_paradInici - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _2_lineaInici + ")";
            fav2_text2.text = cleanText(_2_paradasFinal[_2_paradFinal - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _2_lineaFinal + ")";
        }

        if (is3rdEmpty == false)
        {
            string location = "";
            string location_extra = "";
            string lineaNomenclatura = "[error]";

            if (_3_type == "metro") { location = "lineas/"; lineaNomenclatura = "línea "; }
            if (_3_type == "renfe") { location = "renfe/c"; lineaNomenclatura = "C"; }
            if (_3_type == "bus_dir1") { location = "buses/paradas/"; location_extra = "/dir1_pa"; lineaNomenclatura = "Interurbano "; }
            if (_3_type == "bus_dir2") { location = "buses/paradas/"; location_extra = "/dir2_pa"; lineaNomenclatura = "Interurbano "; }

            TextAsset _3_paradasEnLineaPrimera = Resources.Load<TextAsset>(location + _3_lineaInici + location_extra);
            string[] _3_paradasPrimera = _3_paradasEnLineaPrimera.text.Split('\n');

            TextAsset _3_paradasEnLineaFinal = Resources.Load<TextAsset>(location + _3_lineaFinal + location_extra);
            string[] _3_paradasFinal = _3_paradasEnLineaFinal.text.Split('\n');

            fav3_name.text = _3_name;
            fav3_text1.text = cleanText(_3_paradasPrimera[_3_paradInici - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _3_lineaInici + ")";
            fav3_text2.text = cleanText(_3_paradasFinal[_3_paradFinal - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _3_lineaFinal + ")";
        }

        if (is4thEmpty == false)
        {
            string location = "";
            string location_extra = "";
            string lineaNomenclatura = "[error]";

            if (_4_type == "metro") { location = "lineas/"; lineaNomenclatura = "línea "; }
            if (_4_type == "renfe") { location = "renfe/c"; lineaNomenclatura = "C"; }
            if (_4_type == "bus_dir1") { location = "buses/paradas/"; location_extra = "/dir1_pa"; lineaNomenclatura = "Interurbano "; }
            if (_4_type == "bus_dir2") { location = "buses/paradas/"; location_extra = "/dir2_pa"; lineaNomenclatura = "Interurbano "; }

            TextAsset _4_paradasEnLineaPrimera = Resources.Load<TextAsset>(location + _4_lineaInici + location_extra);
            string[] _4_paradasPrimera = _4_paradasEnLineaPrimera.text.Split('\n');

            TextAsset _4_paradasEnLineaFinal = Resources.Load<TextAsset>(location + _4_lineaFinal + location_extra);
            string[] _4_paradasFinal = _4_paradasEnLineaFinal.text.Split('\n');

            fav4_name.text = _4_name;
            fav4_text1.text = cleanText(_4_paradasPrimera[_4_paradInici - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _4_lineaInici + ")";
            fav4_text2.text = cleanText(_4_paradasFinal[_4_paradFinal - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _4_lineaFinal + ")";
        }

        if (is5thEmpty == false)
        {
            string location = "";
            string location_extra = "";
            string lineaNomenclatura = "[error]";

            if (_5_type == "metro") { location = "lineas/"; lineaNomenclatura = "línea "; }
            if (_5_type == "renfe") { location = "renfe/c"; lineaNomenclatura = "C"; }
            if (_5_type == "bus_dir1") { location = "buses/paradas/"; location_extra = "/dir1_pa"; lineaNomenclatura = "Interurbano "; }
            if (_5_type == "bus_dir2") { location = "buses/paradas/"; location_extra = "/dir2_pa"; lineaNomenclatura = "Interurbano "; }

            TextAsset _5_paradasEnLineaPrimera = Resources.Load<TextAsset>(location + _5_lineaInici + location_extra);
            string[] _5_paradasPrimera = _5_paradasEnLineaPrimera.text.Split('\n');

            TextAsset _5_paradasEnLineaFinal = Resources.Load<TextAsset>(location + _5_lineaFinal + location_extra);
            string[] _5_paradasFinal = _5_paradasEnLineaFinal.text.Split('\n');

            fav5_name.text = _5_name;
            fav5_text1.text = cleanText(_5_paradasPrimera[_5_paradInici - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _5_lineaInici + ")";
            fav5_text2.text = cleanText(_5_paradasFinal[_5_paradFinal - 1].Trim()) + " <color=#BCBCBC>(" + lineaNomenclatura + _5_lineaFinal + ")";
        }


        if (isf_l1_Empty == false)
        {
            string lineaNomenclatura = "[error]";

            if (_f_l1_type == "metro") { lineaNomenclatura = "Metro: línea "; }
            if (_f_l1_type == "renfe") { lineaNomenclatura = "Cercanías: C"; }
            if (_f_l1_type == "bus") { lineaNomenclatura = "Interurbano: "; }

            f_l1_name.text = _f_l1_name;
            f_l1_text1.text = lineaNomenclatura + _f_l1_linea;
        }
        if (isf_l2_Empty == false)
        {
            string lineaNomenclatura = "[error]";

            if (_f_l2_type == "metro") { lineaNomenclatura = "Metro: línea "; }
            if (_f_l2_type == "renfe") { lineaNomenclatura = "C"; }
            if (_f_l2_type == "bus") { lineaNomenclatura = "Interurbano "; }

            f_l2_name.text = _f_l2_name;
            f_l2_text1.text = lineaNomenclatura + _f_l2_linea;
        }
        if (isf_l3_Empty == false)
        {
            string lineaNomenclatura = "[error]";

            if (_f_l3_type == "metro") { lineaNomenclatura = "Metro: línea "; }
            if (_f_l3_type == "renfe") { lineaNomenclatura = "C"; }
            if (_f_l3_type == "bus") { lineaNomenclatura = "Interurbano "; }

            f_l3_name.text = _f_l3_name;
            f_l3_text1.text = lineaNomenclatura + _f_l3_linea;
        }
        if (isf_l4_Empty == false)
        {
            string lineaNomenclatura = "[error]";

            if (_f_l4_type == "metro") { lineaNomenclatura = "Metro: línea "; }
            if (_f_l4_type == "renfe") { lineaNomenclatura = "C"; }
            if (_f_l4_type == "bus") { lineaNomenclatura = "Interurbano "; }

            f_l4_name.text = _f_l4_name;
            f_l4_text1.text = lineaNomenclatura + _f_l4_linea;
        }
        if (isf_l5_Empty == false)
        {
            string lineaNomenclatura = "[error]";

            if (_f_l5_type == "metro") { lineaNomenclatura = "Metro: línea "; }
            if (_f_l5_type == "renfe") { lineaNomenclatura = "C"; }
            if (_f_l5_type == "bus") { lineaNomenclatura = "Interurbano "; }

            f_l5_name.text = _f_l5_name;
            f_l5_text1.text = lineaNomenclatura + _f_l5_linea;
        }


        if (isf_p1_Empty == false)
        {
            f_p1_name.text = _f_p1_name;
            f_p1_code.text = _f_p1_parada;
        }
        if (isf_p2_Empty == false)
        {
            f_p2_name.text = _f_p2_name;
            f_p2_code.text = _f_p2_parada;
        }
        if (isf_p3_Empty == false)
        {
            f_p3_name.text = _f_p3_name;
            f_p3_code.text = _f_p3_parada;
        }
        if (isf_p4_Empty == false)
        {
            f_p4_name.text = _f_p4_name;
            f_p4_code.text = _f_p4_parada;
        }
        if (isf_p5_Empty == false)
        {
            f_p5_name.text = _f_p5_name;
            f_p5_code.text = _f_p5_parada;
        }
    }

    public void SaveRoute(int paradaInicial, string lineaInicial, int paradaFinal, string lineaFinal, string type)
    {
        if (is1stEmpty == true)
        {
            _1_name = "Default name";
            _1_type = type;
            _1_paradInici = paradaInicial;
            _1_lineaInici = lineaInicial;
            _1_paradFinal = paradaFinal;
            _1_lineaFinal = lineaFinal;

            is1stEmpty = false;
        }
        else if (is2ndEmpty == true)
        {
            _2_name = "Default name";
            _2_type = type;
            _2_paradInici = paradaInicial;
            _2_lineaInici = lineaInicial;
            _2_paradFinal = paradaFinal;
            _2_lineaFinal = lineaFinal;

            is2ndEmpty = false;
        }
        else if (is3rdEmpty == true)
        {
            _3_name = "Default name";
            _3_type = type;
            _3_paradInici = paradaInicial;
            _3_lineaInici = lineaInicial;
            _3_paradFinal = paradaFinal;
            _3_lineaFinal = lineaFinal;

            is3rdEmpty = false;
        }
        else if (is4thEmpty == true)
        {
            _4_name = "Default name";
            _4_type = type;
            _4_paradInici = paradaInicial;
            _4_lineaInici = lineaInicial;
            _4_paradFinal = paradaFinal;
            _4_lineaFinal = lineaFinal;

            is4thEmpty = false;
        }
        else if (is5thEmpty == true)
        {
            _5_name = "Default name";
            _5_type = type;
            _5_paradInici = paradaInicial;
            _5_lineaInici = lineaInicial;
            _5_paradFinal = paradaFinal;
            _5_lineaFinal = lineaFinal;

            is5thEmpty = false;
        }
        else
        {
            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.NoMemoryWarning("save_route");
        }

        GetEmptySlots();
        SaveDataToPlayerPrefs();
    }
    public void SaveLinea(string linea, string type)
    {
        if (isf_l1_Empty == true)
        {
            _f_l1_name = "Default name";
            _f_l1_type = type;
            _f_l1_linea = linea;

            isf_l1_Empty = false;
        }
        else if (isf_l2_Empty == true)
        {
            _f_l2_name = "Default name";
            _f_l2_type = type;
            _f_l2_linea = linea;

            isf_l2_Empty = false;
        }
        else if (isf_l3_Empty == true)
        {
            _f_l3_name = "Default name";
            _f_l3_type = type;
            _f_l3_linea = linea;

            isf_l3_Empty = false;
        }
        else if (isf_l4_Empty == true)
        {
            _f_l4_name = "Default name";
            _f_l4_type = type;
            _f_l4_linea = linea;

            isf_l4_Empty = false;
        }
        else if (isf_l5_Empty == true)
        {
            _f_l5_name = "Default name";
            _f_l5_type = type;
            _f_l5_linea = linea;

            isf_l5_Empty = false;
        }
        else
        {
            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.NoMemoryWarning("save_route");
        }

        GetEmptySlots();
        SaveDataToPlayerPrefs();
    }
    public void SaveParada(string code)
    {
        if (isf_p1_Empty == true)
        {
            _f_p1_name = "Default name";
            _f_p1_parada = code;

            isf_p1_Empty = false;
        }
        else if (isf_p2_Empty == true)
        {
            _f_p2_name = "Default name";
            _f_p2_parada = code;

            isf_p2_Empty = false;
        }
        else if (isf_p3_Empty == true)
        {
            _f_p3_name = "Default name";
            _f_p3_parada = code;

            isf_p3_Empty = false;
        }
        else if (isf_p4_Empty == true)
        {
            _f_p4_name = "Default name";
            _f_p4_parada = code;

            isf_p4_Empty = false;
        }
        else if (isf_p5_Empty == true)
        {
            _f_p5_name = "Default name";
            _f_p5_parada = code;

            isf_p5_Empty = false;
        }
        else
        {
            indicators indicators = FindFirstObjectByType<indicators>();
            indicators.NoMemoryWarning("save_parada");
        }

        GetEmptySlots();
        SaveDataToPlayerPrefs();
    }


    public void EraseAllData()
    {
        Delete1();
        Delete2();
        Delete3();
        Delete4();
        Delete5();

        DeleteFL1();
        DeleteFL2();
        DeleteFL3();
        DeleteFL4();
        DeleteFL5();

        DeleteFP1();
        DeleteFP2();
        DeleteFP3();
        DeleteFP4();
        DeleteFP5();

        GetEmptySlots();
        SaveDataToPlayerPrefs();
        RefreshData();
}

    void SaveDataToPlayerPrefs()
    {
        PlayerPrefs.SetString("_1_name", fav1_name.text);
        PlayerPrefs.SetString("_1_type", _1_type);
        PlayerPrefs.SetString("_1_lineaInici", _1_lineaInici);
        PlayerPrefs.SetInt("_1_paradInici", _1_paradInici);
        PlayerPrefs.SetString("_1_lineaFinal", _1_lineaFinal);
        PlayerPrefs.SetInt("_1_paradFinal", _1_paradFinal);

        PlayerPrefs.SetString("_2_name", fav2_name.text);
        PlayerPrefs.SetString("_2_type", _2_type);
        PlayerPrefs.SetString("_2_lineaInici", _2_lineaInici);
        PlayerPrefs.SetInt("_2_paradInici", _2_paradInici);
        PlayerPrefs.SetString("_2_lineaFinal", _2_lineaFinal);
        PlayerPrefs.SetInt("_2_paradFinal", _2_paradFinal);

        PlayerPrefs.SetString("_3_name", fav3_name.text);
        PlayerPrefs.SetString("_3_type", _3_type);
        PlayerPrefs.SetString("_3_lineaInici", _3_lineaInici);
        PlayerPrefs.SetInt("_3_paradInici", _3_paradInici);
        PlayerPrefs.SetString("_3_lineaFinal", _3_lineaFinal);
        PlayerPrefs.SetInt("_3_paradFinal", _3_paradFinal);

        PlayerPrefs.SetString("_4_name", fav4_name.text);
        PlayerPrefs.SetString("_4_type", _4_type);
        PlayerPrefs.SetString("_4_lineaInici", _4_lineaInici);
        PlayerPrefs.SetInt("_4_paradInici", _4_paradInici);
        PlayerPrefs.SetString("_4_lineaFinal", _4_lineaFinal);
        PlayerPrefs.SetInt("_4_paradFinal", _4_paradFinal);

        PlayerPrefs.SetString("_5_name", fav5_name.text);
        PlayerPrefs.SetString("_5_type", _5_type);
        PlayerPrefs.SetString("_5_lineaInici", _5_lineaInici);
        PlayerPrefs.SetInt("_5_paradInici", _5_paradInici);
        PlayerPrefs.SetString("_5_lineaFinal", _5_lineaFinal);
        PlayerPrefs.SetInt("_5_paradFinal", _5_paradFinal);


        PlayerPrefs.SetString("_f_l1_name", f_l1_name.text);
        PlayerPrefs.SetString("_f_l1_type", _f_l1_type);
        PlayerPrefs.SetString("_f_l1_linea", _f_l1_linea);

        PlayerPrefs.SetString("_f_l2_name", f_l2_name.text);
        PlayerPrefs.SetString("_f_l2_type", _f_l2_type);
        PlayerPrefs.SetString("_f_l2_linea", _f_l2_linea);

        PlayerPrefs.SetString("_f_l3_name", f_l3_name.text);
        PlayerPrefs.SetString("_f_l3_type", _f_l3_type);
        PlayerPrefs.SetString("_f_l3_linea", _f_l3_linea);

        PlayerPrefs.SetString("_f_l4_name", f_l4_name.text);
        PlayerPrefs.SetString("_f_l4_type", _f_l4_type);
        PlayerPrefs.SetString("_f_l4_linea", _f_l4_linea);

        PlayerPrefs.SetString("_f_l5_name", f_l5_name.text);
        PlayerPrefs.SetString("_f_l5_type", _f_l5_type);
        PlayerPrefs.SetString("_f_l5_linea", _f_l5_linea);


        PlayerPrefs.SetString("_f_p1_name", f_p1_name.text);
        PlayerPrefs.SetString("_f_p1_parada", _f_p1_parada);

        PlayerPrefs.SetString("_f_p2_name", f_p2_name.text);
        PlayerPrefs.SetString("_f_p2_parada", _f_p2_parada);

        PlayerPrefs.SetString("_f_p3_name", f_p3_name.text);
        PlayerPrefs.SetString("_f_p3_parada", _f_p3_parada);

        PlayerPrefs.SetString("_f_p4_name", f_p4_name.text);
        PlayerPrefs.SetString("_f_p4_parada", _f_p4_parada);

        PlayerPrefs.SetString("_f_p5_name", f_p5_name.text);
        PlayerPrefs.SetString("_f_p5_parada", _f_p5_parada);


        PlayerPrefs.Save();
    }

    public void OnClick(int favNumber)
    {
        metro metro = FindFirstObjectByType<metro>();
        trayecto trayecto = FindFirstObjectByType<trayecto>();

        if (favNumber == 1)
        {
            trayecto.GetRouteFromFavs(_1_paradInici, _1_lineaInici, _1_paradFinal, _1_lineaFinal, _1_type);
        }
        if (favNumber == 2)
        {
            trayecto.GetRouteFromFavs(_2_paradInici, _2_lineaInici, _2_paradFinal, _2_lineaFinal, _2_type);
        }
        if (favNumber == 3)
        {
            trayecto.GetRouteFromFavs(_3_paradInici, _3_lineaInici, _3_paradFinal, _3_lineaFinal, _3_type);
        }
        if (favNumber == 4)
        {
            trayecto.GetRouteFromFavs(_4_paradInici, _4_lineaInici, _4_paradFinal, _4_lineaFinal, _4_type);
        }
        if (favNumber == 5)
        {
            trayecto.GetRouteFromFavs(_5_paradInici, _5_lineaInici, _5_paradFinal, _5_lineaFinal, _5_type);
        }

        if (favNumber == 101)
        {
            renfe renfe = FindFirstObjectByType<renfe>();
            bus bus = FindFirstObjectByType<bus>();

            if (_f_l1_type == "metro") { metro.Iterate(_f_l1_linea); m_all.SetActive(true); }
            if (_f_l1_type == "renfe") { renfe.Iterate(_f_l1_linea); r_all.SetActive(true); }
            if (_f_l1_type == "bus") { bus.OpenLineInfo(_f_l1_linea + ""); b_all.SetActive(true); }
        }
        if (favNumber == 102)
        {
            renfe renfe = FindFirstObjectByType<renfe>();
            bus bus = FindFirstObjectByType<bus>();

            if (_f_l2_type == "metro") { metro.Iterate(_f_l2_linea); m_all.SetActive(true); }
            if (_f_l2_type == "renfe") { renfe.Iterate(_f_l2_linea); r_all.SetActive(true); }
            if (_f_l2_type == "bus") { bus.OpenLineInfo(_f_l2_linea + ""); b_all.SetActive(true); }
        }
        if (favNumber == 103)
        {
            renfe renfe = FindFirstObjectByType<renfe>();
            bus bus = FindFirstObjectByType<bus>();

            if (_f_l3_type == "metro") { metro.Iterate(_f_l3_linea); m_all.SetActive(true); }
            if (_f_l3_type == "renfe") { renfe.Iterate(_f_l3_linea); r_all.SetActive(true); }
            if (_f_l3_type == "bus") { bus.OpenLineInfo(_f_l3_linea + ""); b_all.SetActive(true); }
        }
        if (favNumber == 104)
        {
            renfe renfe = FindFirstObjectByType<renfe>();
            bus bus = FindFirstObjectByType<bus>();

            if (_f_l4_type == "metro") { metro.Iterate(_f_l4_linea); m_all.SetActive(true); }
            if (_f_l4_type == "renfe") { renfe.Iterate(_f_l4_linea); r_all.SetActive(true); }
            if (_f_l4_type == "bus") { bus.OpenLineInfo(_f_l4_linea + ""); b_all.SetActive(true); }
        }
        if (favNumber == 105)
        {
            renfe renfe = FindFirstObjectByType<renfe>();
            bus bus = FindFirstObjectByType<bus>();

            if (_f_l5_type == "metro") { metro.Iterate(_f_l5_linea); m_all.SetActive(true); }
            if (_f_l5_type == "renfe") { renfe.Iterate(_f_l5_linea); r_all.SetActive(true); }
            if (_f_l5_type == "bus") { bus.OpenLineInfo(_f_l5_linea + ""); b_all.SetActive(true); }
        }

        if (favNumber == 201)
        {
            stopVisualizer stopVisualizer = FindFirstObjectByType<stopVisualizer>();
            stopVisualizer.Load(_f_p1_parada);
        }
        if (favNumber == 202)
        {
            stopVisualizer stopVisualizer = FindFirstObjectByType<stopVisualizer>();
            stopVisualizer.Load(_f_p2_parada);
        }
        if (favNumber == 203)
        {
            stopVisualizer stopVisualizer = FindFirstObjectByType<stopVisualizer>();
            stopVisualizer.Load(_f_p3_parada);
        }
        if (favNumber == 204)
        {
            stopVisualizer stopVisualizer = FindFirstObjectByType<stopVisualizer>();
            stopVisualizer.Load(_f_p4_parada);
        }
        if (favNumber == 205)
        {
            stopVisualizer stopVisualizer = FindFirstObjectByType<stopVisualizer>();
            stopVisualizer.Load(_f_p5_parada);
        }
    }

    public void GetEditingInfo(int favNumber)
    {
        isEditing = true;
        editingNumb = favNumber;

        newName.text = "";
    }

    void Delete1()
    {
        _1_name = "Default name";
        _1_type = "0";
        _1_lineaInici = "0";
        _1_paradInici = 0;
        _1_lineaFinal = "0";
        _1_paradFinal = 0;
    }
    void Delete2()
    {
        _2_name = "Default name";
        _2_type = "0";
        _2_lineaInici = "0";
        _2_paradInici = 0;
        _2_lineaFinal = "0";
        _2_paradFinal = 0;
    }
    void Delete3()
    {
        _3_name = "Default name";
        _3_type = "0";
        _3_lineaInici = "0";
        _3_paradInici = 0;
        _3_lineaFinal = "0";
        _3_paradFinal = 0;
    }
    void Delete4()
    {
        _4_name = "Default name";
        _4_type = "0";
        _4_lineaInici = "0";
        _4_paradInici = 0;
        _4_lineaFinal = "0";
        _4_paradFinal = 0;
    }
    void Delete5()
    {
        _5_name = "Default name";
        _5_type = "0";
        _5_lineaInici = "0";
        _5_paradInici = 0;
        _5_lineaFinal = "0";
        _5_paradFinal = 0;
    }
    void DeleteFL1()
    {
        _f_l1_name = "Default name";
        _f_l1_linea = "0";
        _f_l1_type = "0";
    }
    void DeleteFL2()
    {
        _f_l2_name = "Default name";
        _f_l2_linea = "0";
        _f_l2_type = "0";
    }
    void DeleteFL3()
    {
        _f_l3_name = "Default name";
        _f_l3_linea = "0";
        _f_l3_type = "0";
    }
    void DeleteFL4()
    {
        _f_l4_name = "Default name";
        _f_l4_linea = "0";
        _f_l4_type = "0";
    }
    void DeleteFL5()
    {
        _f_l5_name = "Default name";
        _f_l5_linea = "0";
        _f_l5_type = "0";
    }
    void DeleteFP1()
    {
        _f_p1_name = "Default name";
        _f_p1_parada = "0";
    }
    void DeleteFP2()
    {
        _f_p2_name = "Default name";
        _f_p2_parada = "0";
    }
    void DeleteFP3()
    {
        _f_p3_name = "Default name";
        _f_p3_parada = "0";
    }
    void DeleteFP4()
    {
        _f_p4_name = "Default name";
        _f_p4_parada = "0";
    }
    void DeleteFP5()
    {
        _f_p5_name = "Default name";
        _f_p5_parada = "0";
    }

    public void SaveEditedContent()
    {
        if (editingNumb == 1)
        {
            _1_name = newName.text;

            if (_1_name == "<borrar>")
            {
                Delete1();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 2)
        {
            _2_name = newName.text;

            if (_2_name == "<borrar>")
            {
                Delete2();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 3)
        {
            _3_name = newName.text;

            if (_3_name == "<borrar>")
            {
                Delete3();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 4)
        {
            _4_name = newName.text;

            if (_4_name == "<borrar>")
            {
                Delete4();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 5)
        {
            _5_name = newName.text;

            if (_5_name == "<borrar>")
            {
                Delete5();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }

        if (editingNumb == 101)
        {
            _f_l1_name = newName.text;

            if (_f_l1_name == "<borrar>")
            {
                DeleteFL1();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 102)
        {
            _f_l2_name = newName.text;

            if (_f_l2_name == "<borrar>")
            {
                DeleteFL2();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 103)
        {
            _f_l3_name = newName.text;

            if (_f_l3_name == "<borrar>")
            {
                DeleteFL3();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 104)
        {
            _f_l4_name = newName.text;

            if (_f_l4_name == "<borrar>")
            {
                DeleteFL4();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 105)
        {
            _f_l5_name = newName.text;

            if (_f_l5_name == "<borrar>")
            {
                DeleteFL5();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }

        if (editingNumb == 201)
        {
            _f_p1_name = newName.text;

            if (_f_p1_name == "<borrar>")
            {
                DeleteFP1();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 202)
        {
            _f_p2_name = newName.text;

            if (_f_p2_name == "<borrar>")
            {
                DeleteFP2();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 203)
        {
            _f_p3_name = newName.text;

            if (_f_p3_name == "<borrar>")
            {
                DeleteFP3();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 204)
        {
            _f_p4_name = newName.text;

            if (_f_p4_name == "<borrar>")
            {
                DeleteFP4();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }
        if (editingNumb == 205)
        {
            _f_p5_name = newName.text;

            if (_f_p5_name == "<borrar>")
            {
                DeleteFP5();

                GetEmptySlots();
                SaveDataToPlayerPrefs();
            }
        }

        isEditing = false;
    }
}
