using UnityEngine;

public class goBack : MonoBehaviour
{
    public GameObject main;
    public GameObject metro;
    public GameObject renfe;
    public GameObject bus;
    public GameObject tutorial;
    public GameObject mapas;

    public GameObject r_nuevo;
    public GameObject r_favs;

    public GameObject m_mapa;
    public GameObject m_all;

    public GameObject r_mapa;
    public GameObject r_all;

    public GameObject b_mapa;
    public GameObject b_all;

    public GameObject ma_mapa;

    public GameObject changelog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }

    void GoBack()
    {
        if (main.activeInHierarchy == true)
        {
            Application.Quit();
        }
        if (metro.activeInHierarchy == true)
        {
            main.SetActive(true);
            metro.SetActive(false);
        }
        if (renfe.activeInHierarchy == true)
        {
            main.SetActive(true);
            renfe.SetActive(false);
        }
        if (bus.activeInHierarchy == true)
        {
            main.SetActive(true);
            bus.SetActive(false);
        }
        if (tutorial.activeInHierarchy == true)
        {
            main.SetActive(true);
            tutorial.SetActive(false);
        }
        if (mapas.activeInHierarchy == true)
        {
            main.SetActive(true);
            mapas.SetActive(false);
        }


        if (m_mapa.activeInHierarchy == true)
        {
            mapas.SetActive(true);
            m_mapa.SetActive(false);
        }
        if (m_all.activeInHierarchy == true)
        {
            metro.SetActive(true);
            m_all.SetActive(false);
        }


        if (r_mapa.activeInHierarchy == true)
        {
            mapas.SetActive(true);
            r_mapa.SetActive(false);
        }
        if (r_all.activeInHierarchy == true)
        {
            renfe.SetActive(true);
            r_all.SetActive(false);
        }


        if (b_mapa.activeInHierarchy == true)
        {
            mapas.SetActive(true);
            b_mapa.SetActive(false);
        }
        if (b_all.activeInHierarchy == true)
        {
            bus.SetActive(true);
            b_all.SetActive(false);
        }


        if (r_favs.activeInHierarchy == true)
        {
            main.SetActive(true);
            r_favs.SetActive(false);
        }
        if (r_nuevo.activeInHierarchy == true)
        {
            main.SetActive(true);
            r_nuevo.SetActive(false);
        }


        if (ma_mapa.activeInHierarchy == true)
        {
            mapas.SetActive(true);
            ma_mapa.SetActive(false);
        }


        if (changelog.activeInHierarchy == true)
        {
            main.SetActive(true);
            changelog.SetActive(false);
        }
    }
}
