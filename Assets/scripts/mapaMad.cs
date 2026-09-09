using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class mapaMad : MonoBehaviour
{
    public RectTransform content;
    public RectTransform locator;
    public GameObject locatorObj;
    public ScrollRect scroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Locate()
    {
        if (locatorObj.activeInHierarchy == true)
        {
            Canvas.ForceUpdateCanvases();

            RectTransform viewport = scroll.viewport;

            Vector2 locatorLocalPos = content.InverseTransformPoint(locator.position);

            Vector3 viewportWorldCenter = viewport.TransformPoint(viewport.rect.center);
            Vector2 viewportCenterInContentSpace = content.InverseTransformPoint(viewportWorldCenter);

            Vector2 offset = viewportCenterInContentSpace - locatorLocalPos;

            content.anchoredPosition += offset;
        }
        else
        {
            StartCoroutine(InitializeGPSPipeline());
        }
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
    }
}
