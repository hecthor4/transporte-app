using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class debugScreen : MonoBehaviour
{
    public TMP_InputField input;

    public GameObject devBanner;

    public string isDev = "false";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDev = PlayerPrefs.GetString("isDev", "false");

        devBanner.SetActive(false);

        if (isDev == "true")
        {
            DevMode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DevMode()
    {
        devBanner.SetActive(true);
    }

    public void Verify()
    {
        if (input.text == "cacafutis28.5")
        {
            isDev = "true";

            PlayerPrefs.SetString("isDev", "true");
            PlayerPrefs.Save();

            DevMode();
        }
    }
}
