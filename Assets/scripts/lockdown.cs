using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class lockdown : MonoBehaviour
{
    public GameObject locked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        locked.SetActive(false);
        StartCoroutine(GetStatus("https://raw.githubusercontent.com/hecthor4/temp-buses/main/lockdown.txt"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetStatus(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string result = webRequest.downloadHandler.text.Trim();

                ProcessStatus(result);
            }
        }
    }

    void ProcessStatus(string result)
    {
        Debug.Log("lockdown: " + result);

        debugScreen debugScreen = FindFirstObjectByType<debugScreen>();

        if (result == "true" && debugScreen.isDev == "false")
        {
            locked.SetActive(true);
            SceneManager.LoadScene(2);
        }
    }
}
