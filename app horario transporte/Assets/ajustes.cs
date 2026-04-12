using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ajustes : MonoBehaviour
{
    public Slider espaciado;
    public TMP_Text m_l1;
    public TMP_Text m_l2;
    public TMP_Text m_l3;
    public TMP_Text m_l4;
    public TMP_Text m_l5;
    public TMP_Text m_l6;
    public TMP_Text m_l7;
    public TMP_Text m_l8;
    public TMP_Text m_l9;
    public TMP_Text m_l10;
    public TMP_Text m_l11;
    public TMP_Text m_l12;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshSettings()
    {
        m_l1.lineSpacing = espaciado.value;
        m_l2.lineSpacing = espaciado.value;
        m_l3.lineSpacing = espaciado.value;
        m_l4.lineSpacing = espaciado.value;
        m_l5.lineSpacing = espaciado.value;
        m_l6.lineSpacing = espaciado.value;
        m_l7.lineSpacing = espaciado.value;
        m_l8.lineSpacing = espaciado.value;
        m_l9.lineSpacing = espaciado.value;
        m_l10.lineSpacing = espaciado.value;
        m_l11.lineSpacing = espaciado.value;
        m_l12.lineSpacing = espaciado.value;
    }
}
