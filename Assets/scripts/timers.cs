using System.Collections;
using UnityEngine;

public class timers : MonoBehaviour
{
    public int activeTimers;

    public GameObject timerButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (activeTimers >= 1)
        {
            timerButton.SetActive(true);
        }
        else
        {
            timerButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTimer(int minutes)
    {
        StartCoroutine(Timer(minutes));

        timerButton.SetActive(true);
        activeTimers++;
    }

    IEnumerator Timer(int minutes)
    {
        yield return new WaitForSeconds(minutes * 60);

        activeTimers--;

        if (activeTimers == 0)
        {
            timerButton.SetActive(false);
        }
    }
}
