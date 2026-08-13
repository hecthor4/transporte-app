using TMPro;
using UnityEngine;

public class fontSize : MonoBehaviour
{
    private int size;

    ajustes ajustes;
    TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ajustes = FindFirstObjectByType<ajustes>();
        text = GetComponent<TMP_Text>();

        size = Mathf.RoundToInt(text.fontSize + ajustes.fontSizeModifier);
        text.fontSize = size;
    }

    public void Refresh()
    {
        size = Mathf.RoundToInt(text.fontSize + ajustes.fontSizeModifier);
        text.fontSize = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
