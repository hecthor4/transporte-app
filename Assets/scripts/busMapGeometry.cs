using UnityEngine;

public class busMapGeometry : MonoBehaviour
{
    public GameObject mapParent;

    public GameObject map;
    public GameObject gaze;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mapParent.activeInHierarchy == true)
        {
            gaze.transform.localScale = map.transform.localScale;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {

        }
    }
}
