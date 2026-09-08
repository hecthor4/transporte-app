using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class busMap : MonoBehaviour
{
    public GameObject iterationParent;
    public RectTransform iterationParentRect;

    public GameObject iteration;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KillChildren();
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void KillChildren()
    {
        for (int i = iterationParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(iterationParent.transform.GetChild(i).gameObject);
        }
    }

    public void CloseBusMap()
    {
        iterationParent.SetActive(false);
    }
    public void OpenBusMap()
    {
        iterationParent.SetActive(true);
    }

    public void Load()
    {
        iterationParent.SetActive(true);

        StartCoroutine(GetPositions("https://welbits.github.io/static_stops.json"));
    }

    IEnumerator GetPositions(string apiURL)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = webRequest.downloadHandler.text;

                if (jsonResult.Trim().StartsWith("["))
                {
                    jsonResult = "{\"stops\":" + jsonResult + "}";
                }

                StopCollectionAP data = JsonUtility.FromJson<StopCollectionAP>(jsonResult);

                //StartCoroutine(ProcessPositions(data.stops));
            }
        }
    }

    IEnumerator ProcessPositions(List<StopDataAP> stops)
    {
        if (stops == null)
        {
            yield break;
        }

        float minLatitude = 40.25002f;
        float maxLatitude = 40.56269f;
        float minLongitude = -3.9829f;
        float maxLongitude = -3.253411f;

        foreach (StopDataAP stop in stops)
        {
            if (stop == null) continue;

            GameObject iteratedStop = Instantiate(iteration, iterationParent.transform);
            RectTransform iteratedTransform = iteratedStop.GetComponent<RectTransform>();

            Button button = iteratedStop.GetComponent<Button>();
            stopButton script = iteratedStop.GetComponent<stopButton>();

            //button.onClick.RemoveAllListeners();
            //button.onClick.AddListener(script.OnClickMath);


            script.SetCode(stop.id);

            float pctX = (stop.longitude - minLongitude) / (maxLongitude - minLongitude);
            float pctY = (stop.latitude - minLatitude) / (maxLatitude - minLatitude);

            pctX = Mathf.Clamp01(pctX);
            pctY = Mathf.Clamp01(pctY);


            float mapWidth = iterationParentRect.rect.width;
            float mapHeight = iterationParentRect.rect.height;


            float localX = (pctX - iterationParentRect.pivot.x) * mapWidth;
            float localY = (pctY - iterationParentRect.pivot.y) * mapHeight;

            iteratedTransform.anchoredPosition = new Vector2(localX, localY);

            //yield return new WaitForSeconds(0.1f);
        }
    }
}
