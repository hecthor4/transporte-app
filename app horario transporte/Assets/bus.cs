using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bus : MonoBehaviour
{
    public TMP_Text busLinea;

    public GameObject ciudadPrefab;
    public TMP_Text cp_ciudadTitle;
    public TMP_Text cp_paradasText;

    public GameObject linea_info;
    public TMP_Text linea_info_direccion;
    public GameObject linea_enlace;
    public Button linea_enlace_button;

    public GameObject ciudadParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void Iterate(int linea)
    {
        busLinea.text = "Línea " + linea;

        TextAsset paradasEnLinea = Resources.Load<TextAsset>("buses/" + linea);
        string[] paradas = paradasEnLinea.text.Split('\n');

        //destruye todo
        for (int i = ciudadParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ciudadParent.transform.GetChild(i).gameObject);
        }

        //crea todo
        Instantiate(linea_info, new Vector3(0, 0, 0), Quaternion.identity, ciudadParent.transform);
        Instantiate(linea_enlace, new Vector3(0, 0, 0), Quaternion.identity, ciudadParent.transform);

        //var linea_info_direccion = ciudadParent.transform.Find("info").GetComponent<TMP_Text>();
        linea_info_direccion.text = paradas[0].Trim();
        linea_enlace_button.onClick.RemoveAllListeners();
        linea_enlace_button.onClick.AddListener(() => OpenLink(paradas[1].Trim()));


        for (int i = 3; i < paradas.Length; i++)
        {
            if (paradas[i].Contains("#"))
            {
                Instantiate(ciudadPrefab, new Vector3(0, 0, 0), Quaternion.identity, ciudadParent.transform);
                cp_ciudadTitle.text = paradas[i];

                for (int j = i + 1; j < paradas.Length; j++)
                {
                    if (paradas[j].Contains("#") == false)
                    {
                        break;
                    }
                    else
                    {
                        cp_paradasText.text = cp_paradasText.text + paradas[j];
                    }
                }
            }
            /*
            paradasObj[i].SetActive(true);
            paradasTexts[i].text = paradas[i].Trim();
            */
        }
    }
}
