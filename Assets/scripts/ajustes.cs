using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ajustes : MonoBehaviour
{
    public Slider m_tamaño;
    public int m_tamañoSave;
    public Slider m_espaciado;
    public int m_espaciadoSave;
    public VerticalLayoutGroup m_layout;

    public Slider r_tamaño;
    public int r_tamañoSave;
    public Slider r_espaciado;
    public int r_espaciadoSave;
    public VerticalLayoutGroup r_layout;

    public Slider b_tamañoX;
    public int b_tamañoSaveX;
    public Slider b_tamañoY;
    public int b_tamañoSaveY;
    public Slider b_espaciado;
    public int b_espaciadoSave;
    public Slider b_preferenciaInterurbano;
    public int b_preferenciaInterurbanoSave;
    public GridLayoutGroup b_layout;

    public Slider espaciadoBus;

    metro metro_ins;
    renfe renfe_ins;
    bus bus_ins;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        metro_ins = FindFirstObjectByType<metro>();
        renfe_ins = FindFirstObjectByType<renfe>();
        bus_ins = FindFirstObjectByType<bus>();


        m_tamañoSave = PlayerPrefs.GetInt("m_tamañoSave", 85);
        m_espaciadoSave = PlayerPrefs.GetInt("m_espaciadoSave", 2);

        r_tamañoSave = PlayerPrefs.GetInt("r_tamañoSave", 130);
        r_espaciadoSave = PlayerPrefs.GetInt("r_espaciadoSave", 2);

        b_tamañoSaveX = PlayerPrefs.GetInt("b_tamañoSaveX", 500);
        b_tamañoSaveY = PlayerPrefs.GetInt("b_tamañoSaveY", 200);
        b_espaciadoSave = PlayerPrefs.GetInt("b_espaciadoSave", 0);
        b_preferenciaInterurbanoSave = PlayerPrefs.GetInt("b_preferenciaInterurbanoSave", 0);




        m_tamaño.value = m_tamañoSave;
        m_espaciado.value = m_espaciadoSave;

        r_tamaño.value = r_tamañoSave;
        r_espaciado.value = r_espaciadoSave;

        b_tamañoX.value = b_tamañoSaveX;
        b_tamañoY.value = b_tamañoSaveY;
        b_preferenciaInterurbano.value = b_preferenciaInterurbanoSave;
        b_espaciado.value = b_espaciadoSave;


        RefreshSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshSettings()
    {
        metro_ins.ModifyButtonHeight(Mathf.RoundToInt(m_tamaño.value));
        m_layout.spacing = m_espaciado.value;


        renfe_ins.ModifyButtonHeight(Mathf.RoundToInt(r_tamaño.value));
        r_layout.spacing = r_espaciado.value;


        //bus_ins.ModifyButtonHeight(Mathf.RoundToInt(b_tamañoX.value), Mathf.RoundToInt(b_tamañoY.value));
        b_layout.cellSize = new Vector2(b_tamañoX.value, b_tamañoY.value);
        b_layout.spacing = new Vector2(b_espaciado.value, b_espaciado.value);
        if (b_preferenciaInterurbano.value == 0) { bus_ins.isInterurbanPrefered = false; } else { bus_ins.isInterurbanPrefered = true; } 




        m_tamañoSave = Mathf.RoundToInt(m_tamaño.value);
        m_espaciadoSave = Mathf.RoundToInt(m_espaciado.value);


        r_tamañoSave = Mathf.RoundToInt(r_tamaño.value);
        r_espaciadoSave = Mathf.RoundToInt(r_espaciado.value);


        b_tamañoSaveX = Mathf.RoundToInt(b_tamañoX.value);
        b_tamañoSaveY = Mathf.RoundToInt(b_tamañoY.value);
        b_espaciadoSave = Mathf.RoundToInt(r_espaciado.value);
        b_preferenciaInterurbanoSave = Mathf.RoundToInt(b_preferenciaInterurbano.value);
    }

    public void SaveValues()
    {
        RefreshSettings();

        PlayerPrefs.SetInt("m_tamañoSave", m_tamañoSave);
        PlayerPrefs.SetInt("m_espaciadoSave", m_espaciadoSave);

        PlayerPrefs.SetInt("r_tamañoSave", r_tamañoSave);
        PlayerPrefs.SetInt("r_espaciadoSave", r_espaciadoSave);

        PlayerPrefs.SetInt("b_tamañoSaveX", b_tamañoSaveX);
        PlayerPrefs.SetInt("b_tamañoSaveY", b_tamañoSaveY);
        PlayerPrefs.SetInt("b_espaciadoSave", b_espaciadoSave);
        PlayerPrefs.SetInt("b_preferenciaInterurbanoSave", b_preferenciaInterurbanoSave);

        PlayerPrefs.Save();
    }

    public void ResetValues(string which)
    {
        if (which == "metro")
        {
            m_tamañoSave = 85;
            m_espaciadoSave = 2;

            m_tamaño.value = m_tamañoSave;
            m_espaciado.value = m_espaciadoSave;
        }
        if (which == "renfe")
        {
            r_tamañoSave = 130;
            r_espaciadoSave = 2;

            r_tamaño.value = r_tamañoSave;
            r_espaciado.value = r_espaciadoSave;
        }
        if (which == "bus")
        {
            b_tamañoSaveX = 500;
            b_tamañoSaveY = 200;
            b_espaciadoSave = 0;
            b_preferenciaInterurbanoSave = 0;

            b_tamañoX.value = b_tamañoSaveX;
            b_tamañoY.value = b_tamañoSaveY;
            b_espaciado.value = b_espaciadoSave;
            b_preferenciaInterurbano.value = b_preferenciaInterurbanoSave;
        }

        RefreshSettings();
        SaveValues();
    }
}
