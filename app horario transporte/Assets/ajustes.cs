using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ajustes : MonoBehaviour
{
    public Slider tamaño;
    public Slider espaciado;

    public VerticalLayoutGroup metroLayout;

    public Slider espaciadoBus;


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
        metro metro = FindFirstObjectByType<metro>();
        metro.ModifyButtonHeight(Mathf.RoundToInt(tamaño.value));

        metroLayout.spacing = espaciado.value;
    }
}
