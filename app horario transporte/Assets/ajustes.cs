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

    public Slider espaciadoBus;

    metro metro_ins;
    renfe renfe_ins;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        metro_ins = FindFirstObjectByType<metro>();
        renfe_ins = FindFirstObjectByType<renfe>();


        m_tamañoSave = PlayerPrefs.GetInt("m_tamañoSave", 85);
        m_espaciadoSave = PlayerPrefs.GetInt("m_espaciadoSave", 2);

        r_tamañoSave = PlayerPrefs.GetInt("r_tamañoSave", 85);
        r_espaciadoSave = PlayerPrefs.GetInt("r_espaciadoSave", 2);


        m_tamaño.value = m_tamañoSave;
        m_espaciado.value = m_espaciadoSave;

        r_tamaño.value = r_tamañoSave;
        r_espaciado.value = r_espaciadoSave;


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


        m_tamañoSave = Mathf.RoundToInt(m_tamaño.value);
        m_espaciadoSave = Mathf.RoundToInt(m_espaciado.value);

        r_tamañoSave = Mathf.RoundToInt(r_tamaño.value);
        r_espaciadoSave = Mathf.RoundToInt(r_espaciado.value);
    }

    public void SaveValues()
    {
        RefreshSettings();

        PlayerPrefs.SetInt("m_tamañoSave", m_tamañoSave);
        PlayerPrefs.SetInt("m_espaciadoSave", m_tamañoSave);

        PlayerPrefs.SetInt("r_tamañoSave", r_tamañoSave);
        PlayerPrefs.SetInt("r_espaciadoSave", r_tamañoSave);

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
            r_tamañoSave = 85;
            r_espaciadoSave = 2;

            r_tamaño.value = r_tamañoSave;
            r_espaciado.value = r_espaciadoSave;
        }

        RefreshSettings();
        SaveValues();
    }
}
