using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class timers : MonoBehaviour
{
    public int activeTimers;

    public GameObject parent;
    public GameObject iteration;

    public GameObject timerButton;

    public GameObject noData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (activeTimers >= 1)
        {
            noData.SetActive(false);
        }
        else
        {
            noData.SetActive(true);
        }
        timerButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTimer(float minutes, string line, string parada)
    {
        StartCoroutine(Timer(minutes, line, parada.Trim()));

        timerButton.SetActive(true);
        noData.SetActive(false);
        activeTimers++;
    }

    IEnumerator Timer(float minutes, string line, string parada)
    {
        GameObject instance = Instantiate(iteration, parent.transform);
        TMP_Text lineNumber = instance.transform.Find("linea numero").GetComponent<TMP_Text>();
        TMP_Text lineLongName = instance.transform.Find("linea nombre grande").GetComponent<TMP_Text>();
        TMP_Text paradaText = instance.transform.Find("parada encontrada").GetComponent<TMP_Text>();
        Slider slider = instance.transform.Find("time slider").GetComponent<Slider>();
        TMP_Text remaining = instance.transform.Find("tiempo restante").GetComponent<TMP_Text>();

        lineNumber.text = line;
        lineLongName.text = "Línea de interurbano " + line;
        paradaText.text = "Parada '" + parada + "'.";

        DateTime now = DateTime.Now;
        DateTime end = DateTime.Now.AddMinutes(minutes);

        slider.maxValue = minutes * 60;
        slider.value = 0;


        while (now < end)
        {
            now = DateTime.Now;
            TimeSpan difference = end - now;
            slider.value = slider.maxValue - (float)difference.TotalSeconds;

            remaining.text = string.Format("{0:00}:{1:00}", difference.Minutes, difference.Seconds);

            yield return new WaitForSeconds(1);
        }

        activeTimers--;

        if (activeTimers == 0)
        {
            //timerButton.SetActive(false); //old: false
            noData.SetActive(false);
        }

        Destroy(instance);
    }
}
