using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class GPS : MonoBehaviour
{
    public RectTransform mapParentRect;
    public RectTransform playerIndicator;

    public float minLatitude;
    public float maxLatitude;
    public float minLongitude;
    public float maxLongitude;

    public float smoothSpeed = 5f;
    public float rotationSmoothSpeed = 10f;

    private Vector2 targetAnchoredPosition;
    private float targetRotation;
    private float lastKnownLat;
    private float lastKnownLon;
    private bool isServiceStarted = false;

    void Start()
    {
        StartCoroutine(InitializeGPSPipeline());
    }

    IEnumerator InitializeGPSPipeline()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            int permissionWait = 10;
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation) && permissionWait > 0)
            {
                yield return new WaitForSeconds(1);
                permissionWait--;
            }
        }
#endif


        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("gps desactivado warning");
            yield break;
        }


        Input.location.Start(1f, 1f);
        Input.compass.enabled = true;

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("localizacion failed");
            yield break;
        }

        isServiceStarted = true;
    }

    void Update()
    {
        ProcessRealGPS();

        float positionLerpFactor = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime);
        float rotationLerpFactor = 1f - Mathf.Exp(-rotationSmoothSpeed * Time.deltaTime);

        playerIndicator.anchoredPosition = Vector2.Lerp(playerIndicator.anchoredPosition, targetAnchoredPosition, positionLerpFactor);

        Quaternion targetVisualRotation = Quaternion.Euler(0, 0, targetRotation);
        playerIndicator.rotation = Quaternion.Lerp(playerIndicator.rotation, targetVisualRotation, rotationLerpFactor);
    }

    private void ProcessRealGPS()
    {
        if (!isServiceStarted || Input.location.status != LocationServiceStatus.Running)
            return;


        float currentLat = Input.location.lastData.latitude;
        float currentLon = Input.location.lastData.longitude;

        if (!Mathf.Approximately(currentLat, lastKnownLat) || !Mathf.Approximately(currentLon, lastKnownLon))
        {
            lastKnownLat = currentLat;
            lastKnownLon = currentLon;
            CalculateTargetPosition(currentLat, currentLon);
        }


        targetRotation = -Input.compass.trueHeading;
    }

    void CalculateTargetPosition(float lat, float lon)
    {
        float pctX = (lon - minLongitude) / (maxLongitude - minLongitude);
        float pctY = (lat - minLatitude) / (maxLatitude - minLatitude);

        pctX = Mathf.Clamp01(pctX);
        pctY = Mathf.Clamp01(pctY);


        float mapWidth = mapParentRect.rect.width;
        float mapHeight = mapParentRect.rect.height;


        float localX = (pctX - mapParentRect.pivot.x) * mapWidth;
        float localY = (pctY - mapParentRect.pivot.y) * mapHeight;

        targetAnchoredPosition = new Vector2(localX, localY);
    }
}
