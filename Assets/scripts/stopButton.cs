using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class stopButton : MonoBehaviour
{
    public RectTransform contRect;

    public float min = 0.05f;
    public float max = 1f;

    public string code;

    public GameObject onClickMenu;
    public TMP_Text paradaName;
    public TMP_Text paradaLines;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contRect = GameObject.FindWithTag("Finish").GetComponent<RectTransform>(); //finish tag. change
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.InverseLerp(1f, 4f, contRect.localScale.x);
        float newScale = Mathf.Lerp(max, min, t);

        transform.localScale = Vector3.one * Mathf.Clamp(newScale, min, max);
    }

    public void SetCode(string newCode)
    {
        code = newCode;
    }

    public void OnClickMath()
    {
        Debug.Log("parada iterated stop button click warn");

        goBack goback = FindFirstObjectByType<goBack>();
        goback.OpenScreen();

        stopVisualizer visorDeParadas = FindFirstObjectByType<stopVisualizer>();
        visorDeParadas.Load(code);
    }
}
