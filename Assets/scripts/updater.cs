using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class updater : MonoBehaviour
{
    public string url;
    public string path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "update.apk");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartUpdate()
    {
        StartCoroutine(DownloadAndInstall());
    }

    IEnumerator DownloadAndInstall()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.downloadHandler = new DownloadHandlerFile(path);
            UnityWebRequestAsyncOperation operation = www.SendWebRequest();

            while (operation.isDone == false)
            {
                float progress = www.downloadProgress;
                yield return null;
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                TriggerAndroidInstaller(path);
            }
            else
            {
                //error
            }
        }
    }

    void TriggerAndroidInstaller(string apkPath)
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {

        }
    }
}
