using System;
using TMPro;
using UnityEngine;

public class updater : MonoBehaviour
{
    public GameObject updateMenu;
    public TMP_InputField input;

    public int isCodeCorrect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCodeCorrect = PlayerPrefs.GetInt("isCodeCorrect", 0);

        //
        //isCodeCorrect = 0;
        //PlayerPrefs.SetInt("isCodeCorrect", isCodeCorrect);
        //PlayerPrefs.Save();
        //

        int month = DateTime.Now.Month;
        if (month > 5 && isCodeCorrect == 0)
        {
            Debug.Log("error. actualiza app");
            updateMenu.SetActive(true);
        }
        else
        {

        }
    }

    public void Check()
    {
        if (input.text == "cp")
        {
            updateMenu.SetActive(false);

            isCodeCorrect = 1;
            PlayerPrefs.SetInt("isCodeCorrect", isCodeCorrect);
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
