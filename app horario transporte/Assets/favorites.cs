using TMPro;
using UnityEditor.PackageManager;
using UnityEditor.U2D.Tooling.Analyzer;
using UnityEngine;

public class favorites : MonoBehaviour
{
    public string _1_name;
    public int _1_lineaInici;
    public int _1_paradInici;
    public int _1_lineaFinal;
    public int _1_paradFinal;

    public string _2_name;
    public int _2_lineaInici;
    public int _2_paradInici;
    public int _2_lineaFinal;
    public int _2_paradFinal;

    public string _3_name;
    public int _3_lineaInici;
    public int _3_paradInici;
    public int _3_lineaFinal;
    public int _3_paradFinal;

    public string _4_name;
    public int _4_lineaInici;
    public int _4_paradInici;
    public int _4_lineaFinal;
    public int _4_paradFinal;

    public string _5_name;
    public int _5_lineaInici;
    public int _5_paradInici;
    public int _5_lineaFinal;
    public int _5_paradFinal;


    public bool is1stEmpty;
    public bool is2ndEmpty;
    public bool is3rdEmpty;
    public bool is4thEmpty;
    public bool is5thEmpty;


    public GameObject fav1;
    public GameObject fav2;
    public GameObject fav3;
    public GameObject fav4;
    public GameObject fav5;

    public TMP_Text fav1_text1;
    public TMP_Text fav1_text2;

    public TMP_Text fav2_text1;
    public TMP_Text fav2_text2;

    public TMP_Text fav3_text1;
    public TMP_Text fav3_text2;

    public TMP_Text fav4_text1;
    public TMP_Text fav4_text2;

    public TMP_Text fav5_text1;
    public TMP_Text fav5_text2;


    public GameObject noRoutesError;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _1_name = PlayerPrefs.GetString("_1_name", "Default");
        _1_lineaInici = PlayerPrefs.GetInt("_1_lineaInici", 0);
        _1_paradInici = PlayerPrefs.GetInt("_1_paradInici", 0);
        _1_lineaFinal = PlayerPrefs.GetInt("_1_lineaFinal", 0);
        _1_paradFinal = PlayerPrefs.GetInt("_1_paradFinal", 0);

        _2_name = PlayerPrefs.GetString("_2_name", "Default");
        _2_lineaInici = PlayerPrefs.GetInt("_2_lineaInici", 0);
        _2_paradInici = PlayerPrefs.GetInt("_2_paradInici", 0);
        _2_lineaFinal = PlayerPrefs.GetInt("_2_lineaFinal", 0);
        _2_paradFinal = PlayerPrefs.GetInt("_2_paradFinal", 0);

        _3_name = PlayerPrefs.GetString("_3_name", "Default");
        _3_lineaInici = PlayerPrefs.GetInt("_3_lineaInici", 0);
        _3_paradInici = PlayerPrefs.GetInt("_3_paradInici", 0);
        _3_lineaFinal = PlayerPrefs.GetInt("_3_lineaFinal", 0);
        _3_paradFinal = PlayerPrefs.GetInt("_3_paradFinal", 0);

        _4_name = PlayerPrefs.GetString("_4_name", "Default");
        _4_lineaInici = PlayerPrefs.GetInt("_4_lineaInici", 0);
        _4_paradInici = PlayerPrefs.GetInt("_4_paradInici", 0);
        _4_lineaFinal = PlayerPrefs.GetInt("_4_lineaFinal", 0);
        _4_paradFinal = PlayerPrefs.GetInt("_4_paradFinal", 0);

        _5_name = PlayerPrefs.GetString("_5_name", "Default");
        _5_lineaInici = PlayerPrefs.GetInt("_5_lineaInici", 0);
        _5_paradInici = PlayerPrefs.GetInt("_5_paradInici", 0);
        _5_lineaFinal = PlayerPrefs.GetInt("_5_lineaFinal", 0);
        _5_paradFinal = PlayerPrefs.GetInt("_5_paradFinal", 0);

        GetEmptySlots();
        noRoutesError.SetActive(true);
    }

    void GetEmptySlots()
    {
        if (_1_lineaFinal == 0 && _1_lineaFinal == 0 && _1_paradFinal == 0 && _1_paradFinal == 0) { 
            is1stEmpty = true; fav1.SetActive(false); }
        else {
            is1stEmpty = false; fav1.SetActive(true); }


        if (_2_lineaFinal == 0 && _2_lineaFinal == 0 && _2_paradFinal == 0 && _2_paradFinal == 0) { 
            is2ndEmpty = true; fav2.SetActive(false); }
        else {
            is2ndEmpty = false; fav2.SetActive(true); }


        if (_3_lineaFinal == 0 && _3_lineaFinal == 0 && _3_paradFinal == 0 && _3_paradFinal == 0) { 
            is3rdEmpty = true; fav3.SetActive(false); }
        else {
            is3rdEmpty = false; fav3.SetActive(true); }


        if (_4_lineaFinal == 0 && _4_lineaFinal == 0 && _4_paradFinal == 0 && _4_paradFinal == 0) { 
            is4thEmpty = true; fav4.SetActive(false); }
        else {
            is4thEmpty = false; fav4.SetActive(true); }


        if (_5_lineaFinal == 0 && _5_lineaFinal == 0 && _5_paradFinal == 0 && _5_paradFinal == 0) { 
            is5thEmpty = true; fav5.SetActive(false); }
        else {
            is5thEmpty = false; fav5.SetActive(true); }


        if (is1stEmpty == true && is2ndEmpty == true && is3rdEmpty == true && is4thEmpty == true && is5thEmpty == true)
        {
            noRoutesError.SetActive(true);
        }
        else
        {
            noRoutesError.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshData()
    {
        if (is1stEmpty == true && is2ndEmpty == true && is3rdEmpty == true && is4thEmpty == true && is5thEmpty == true)
        {
            noRoutesError.SetActive(true);
        }
        else
        {
            noRoutesError.SetActive(false);
        }


        TextAsset _1_paradasEnLineaPrimera = Resources.Load<TextAsset>("lineas/" + _1_lineaInici);
        string[] _1_paradasPrimera = _1_paradasEnLineaPrimera.text.Split('\n');

        TextAsset _1_paradasEnLineaFinal = Resources.Load<TextAsset>("lineas/" + _1_lineaFinal);
        string[] _1_paradasFinal = _1_paradasEnLineaFinal.text.Split('\n');

        string cleanText(string texto) => texto.Split('[')[0].Trim();

        fav1_text1.text = cleanText(_1_paradasPrimera[_1_paradInici].Trim()) + " <color=#BCBCBC>(línea " + _1_lineaInici + ")";
        fav1_text2.text = cleanText(_1_paradasFinal[_1_lineaFinal].Trim()) + " <color=#BCBCBC>(línea " + _1_lineaFinal + ")";
    }

    public void SaveRoute(int paradaInicial, int lineaInicial, int paradaFinal, int lineaFinal)
    {
        if (is1stEmpty == true)
        {
            _1_paradFinal = paradaInicial;
            _1_lineaInici = lineaInicial;
            _1_paradFinal = paradaFinal;
            _1_lineaFinal = lineaFinal;

            is1stEmpty = false;
        }
        else
        {
            if (is2ndEmpty == true)
            {
                _2_paradFinal = paradaInicial;
                _2_lineaInici = lineaInicial;
                _2_paradFinal = paradaFinal;
                _2_lineaFinal = lineaFinal;

                is2ndEmpty = false;
            }
            else
            {
                if (is3rdEmpty == true)
                {
                    _3_paradFinal = paradaInicial;
                    _3_lineaInici = lineaInicial;
                    _3_paradFinal = paradaFinal;
                    _3_lineaFinal = lineaFinal;

                    is3rdEmpty = false;
                }
                else
                {
                    if (is4thEmpty == true)
                    {
                        _4_paradFinal = paradaInicial;
                        _4_lineaInici = lineaInicial;
                        _4_paradFinal = paradaFinal;
                        _4_lineaFinal = lineaFinal;

                        is4thEmpty = false;
                    }
                    else
                    {
                        if (is5thEmpty == true)
                        {
                            _5_paradFinal = paradaInicial;
                            _5_lineaInici = lineaInicial;
                            _5_paradFinal = paradaFinal;
                            _5_lineaFinal = lineaFinal;

                            is5thEmpty = false;
                        }
                    }
                }
            }
        }

        GetEmptySlots();
        SaveDataToPlayerPrefs();
    }

    void SaveDataToPlayerPrefs()
    {
        PlayerPrefs.SetString("_1_name", _1_name);
        PlayerPrefs.SetInt("_1_lineaInici", _1_lineaInici);
        PlayerPrefs.SetInt("_1_paradInici", _1_paradInici);
        PlayerPrefs.SetInt("_1_lineaFinal", _1_lineaFinal);
        PlayerPrefs.SetInt("_1_paradFinal", _1_paradFinal);

        PlayerPrefs.SetString("_2_name", _2_name);
        PlayerPrefs.SetInt("_2_lineaInici", _2_lineaInici);
        PlayerPrefs.SetInt("_2_paradInici", _2_paradInici);
        PlayerPrefs.SetInt("_2_lineaFinal", _2_lineaFinal);
        PlayerPrefs.SetInt("_2_paradFinal", _2_paradFinal);

        PlayerPrefs.SetString("_3_name", _3_name);
        PlayerPrefs.SetInt("_3_lineaInici", _3_lineaInici);
        PlayerPrefs.SetInt("_3_paradInici", _3_paradInici);
        PlayerPrefs.SetInt("_3_lineaFinal", _3_lineaFinal);
        PlayerPrefs.SetInt("_3_paradFinal", _3_paradFinal);

        PlayerPrefs.SetString("_4_name", _4_name);
        PlayerPrefs.SetInt("_4_lineaInici", _4_lineaInici);
        PlayerPrefs.SetInt("_4_paradInici", _4_paradInici);
        PlayerPrefs.SetInt("_4_lineaFinal", _4_lineaFinal);
        PlayerPrefs.SetInt("_4_paradFinal", _4_paradFinal);

        PlayerPrefs.SetString("_5_name", _5_name);
        PlayerPrefs.SetInt("_5_lineaInici", _5_lineaInici);
        PlayerPrefs.SetInt("_5_paradInici", _5_paradInici);
        PlayerPrefs.SetInt("_5_lineaFinal", _5_lineaFinal);
        PlayerPrefs.SetInt("_5_paradFinal", _5_paradFinal);
    }

    public void OnClick(int favNumber)
    {
        metro metro = FindFirstObjectByType<metro>();

        if (favNumber == 1)
        {
            metro.GetRouteFromFavs(_1_paradInici, _1_lineaInici, _1_paradFinal, _1_lineaFinal);
        }
        if (favNumber == 2)
        {
            metro.GetRouteFromFavs(_2_paradInici, _2_lineaInici, _2_paradFinal, _2_lineaFinal);
        }
        if (favNumber == 3)
        {
            metro.GetRouteFromFavs(_3_paradInici, _3_lineaInici, _3_paradFinal, _3_lineaFinal);
        }
        if (favNumber == 4)
        {
            metro.GetRouteFromFavs(_4_paradInici, _4_lineaInici, _4_paradFinal, _4_lineaFinal);
        }
        if (favNumber == 5)
        {
            metro.GetRouteFromFavs(_5_paradInici, _5_lineaInici, _5_paradFinal, _5_lineaFinal);
        }
    }

    public void EditFav(int favNumber, string name)
    {
        if (favNumber == 1)
        {
            _1_name = name;
        }
        if (favNumber == 2)
        {
            _2_name = name;
        }
        if (favNumber == 3)
        {
            _3_name = name;
        }
        if (favNumber == 4)
        {
            _4_name = name;
        }
        if (favNumber == 5)
        {
            _5_name = name;
        }
    }
}
