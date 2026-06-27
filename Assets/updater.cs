using System;
using TMPro;
using UnityEngine;

public class updater : MonoBehaviour
{
    public TMP_Text licenciaInfo;

    public GameObject timeUntilUpdateMenu;
    public GameObject updateMenu;
    public TMP_InputField input;

    public int monthCaduca = 8;

    public int isCodeCorrect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCodeCorrect = PlayerPrefs.GetInt("isCodeCorrect", 0);


        if (isCodeCorrect == 1) { timeUntilUpdateMenu.SetActive(false); }
        else { timeUntilUpdateMenu.SetActive(true); }


        int month = DateTime.Now.Month;
        if (month > monthCaduca && isCodeCorrect == 0) { updateMenu.SetActive(true); }
        

        Refresh();
    }

    void Refresh()
    {
        DateTime now = DateTime.Now;
        DateTime caduca = new DateTime(now.Year, monthCaduca, 1);
        TimeSpan tiempoRestante = caduca - now;
        int diasRestantes = tiempoRestante.Days;

        licenciaInfo.text = "Esta licencia caduca en " + diasRestantes + " días.";
    }

    public void Check()
    {
        if (input.text == "codigocp" || input.text == "felaciones124")
        {
            updateMenu.SetActive(false);

            isCodeCorrect = 1;
            PlayerPrefs.SetInt("isCodeCorrect", isCodeCorrect);
            PlayerPrefs.Save();
        }


        if (isCodeCorrect == 1)
        {
            timeUntilUpdateMenu.SetActive(false);
        }
        else
        {
            timeUntilUpdateMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
