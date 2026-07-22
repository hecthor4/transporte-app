using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goBack : MonoBehaviour
{
    public List<GameObject> lastScreen = new List<GameObject>();
    public List<GameObject> screenHistory = new List<GameObject>();
    public GameObject currentScreen;

    public GameObject main;
    public GameObject metro;
    public GameObject renfe;
    public GameObject bus;
    public GameObject tutorial;
    public GameObject mapas;

    public GameObject r_nuevo;
    public GameObject r_favs;

    public GameObject m_mapa;
    public GameObject m_settings;
    public GameObject m_all;

    public GameObject r_mapa;
    public GameObject r_settings;
    public GameObject r_all;

    public GameObject b_mapa;
    public GameObject b_settings;
    public GameObject b_all;

    public GameObject ma_mapa;
    public GameObject ma_mapaIntegrado;

    public GameObject b_buscar;

    public GameObject changelog;
    public GameObject mainSettings;

    bus bus_ins;
    trayecto trayecto_ins;
    ajustes ajustes_ins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastScreen.Add(main);
        currentScreen = main;

        bus_ins = FindFirstObjectByType<bus>();
        trayecto_ins = FindFirstObjectByType<trayecto>();
        ajustes_ins = FindFirstObjectByType<ajustes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseScreen();
        }
    }

    public void OpenScreen()
    {
        StartCoroutine(OpenScreenCoroutine());
    }
    IEnumerator OpenScreenCoroutine()
    {
        yield return new WaitForSeconds(0.05f);

        lastScreen.Add(currentScreen);

        //Debug.Log(lastScreen[lastScreen.Count - 1].name + " / " + lastScreen[lastScreen.Count - 2].name);
        if (lastScreen[lastScreen.Count - 1].name == lastScreen[lastScreen.Count - 2].name && lastScreen[lastScreen.Count - 1].name != "main")
        {
            lastScreen.RemoveAt(lastScreen.Count - 1);
        }


        //elimina el bug de que se stackean los "r nuevo" en la lista cuando le das a siguiente parada
        if (lastScreen[lastScreen.Count - 1].name == "r nuevo")
        {
            lastScreen.RemoveAt(lastScreen.Count - 1);
        }


        screenHistory.Add(currentScreen);
        FetchCurrentScreen();

        //Debug.Log("screen opened - current screen:" + currentScreen);
    }
    public void CloseScreen()
    {
        StartCoroutine(CloseScreenCoroutine());
    }
    IEnumerator CloseScreenCoroutine()
    {
        yield return new WaitForSeconds(0.05f);

        currentScreen.SetActive(false);

        GameObject last = lastScreen[lastScreen.Count - 1];
        last.SetActive(true);
        lastScreen.RemoveAt(lastScreen.Count - 1);

        FetchCurrentScreen();

        last.SetActive(false);
        currentScreen.SetActive(true);


        //close app
        if (lastScreen.Count < 0 || lastScreen.Count == 0)
        {
            //Application.Quit();
            Debug.Log("closing app...");

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);

            lastScreen.Add(main);
        }


        //other neccesary math
        bus_ins.GoBack();
        ajustes_ins.RefreshSettings();

        if (screenHistory[screenHistory.Count - 1] == r_favs || screenHistory[screenHistory.Count - 1] == r_nuevo) //currentScreen == main || )
        {
            trayecto_ins.ContarParadasReset();

            Debug.Log("contar paradas reset - case 1 - by goback");
        }
        if (screenHistory[screenHistory.Count - 1] == m_all || screenHistory[screenHistory.Count - 1] == r_all || screenHistory[screenHistory.Count - 1] == b_all)
        {
            trayecto_ins.ContarParadasReset();

            Debug.Log("contar paradas reset - case 2 - by goback");
        }

        //Debug.Log("screen closed - current screen:" + currentScreen);
    }


    public void HardcodeValues(string caseNumber)
    {
        if (caseNumber == "1_m") { currentScreen = m_all; lastScreen.RemoveAt(lastScreen.Count - 1); }
        if (caseNumber == "1_r") { currentScreen = r_all; lastScreen.RemoveAt(lastScreen.Count - 1); }
        if (caseNumber == "1_b") { currentScreen = b_all; lastScreen.RemoveAt(lastScreen.Count - 1); }


        if (caseNumber == "2_m") { currentScreen = m_all; lastScreen.RemoveAt(lastScreen.Count - 1); }
        if (caseNumber == "2_r") { currentScreen = r_all; lastScreen.RemoveAt(lastScreen.Count - 1); }
        if (caseNumber == "2_b") { currentScreen = b_all; lastScreen.RemoveAt(lastScreen.Count - 1); }
    }


    void FetchCurrentScreen()
    {
        if (main.activeInHierarchy == true)             { currentScreen = main; }
        else if (metro.activeInHierarchy == true)       { currentScreen = metro; }
        else if (renfe.activeInHierarchy == true)       { currentScreen = renfe; }
        else if (bus.activeInHierarchy == true)         { currentScreen = bus; }
        else if (tutorial.activeInHierarchy == true)    { currentScreen = tutorial; }
        else if (mapas.activeInHierarchy == true)       { currentScreen = mapas; }


        else if (m_mapa.activeInHierarchy == true)      { currentScreen = m_mapa; }
        else if (m_all.activeInHierarchy == true)       { currentScreen = m_all; }
        else if (m_settings.activeInHierarchy == true)  { currentScreen = m_settings; }


        else if (r_mapa.activeInHierarchy == true)      { currentScreen = r_mapa; }
        else if (r_all.activeInHierarchy == true)       { currentScreen = r_all; }
        else if (r_settings.activeInHierarchy == true)  { currentScreen = r_settings; }


        else if (b_mapa.activeInHierarchy == true)      { currentScreen = b_mapa; }
        else if (b_all.activeInHierarchy == true)       { currentScreen = b_all; }
        else if (b_settings.activeInHierarchy == true)  { currentScreen = b_settings; }


        else if (r_favs.activeInHierarchy == true)      { currentScreen = r_favs; }
        else if (r_nuevo.activeInHierarchy == true)     { currentScreen = r_nuevo; }


        else if (ma_mapa.activeInHierarchy == true)     { currentScreen = ma_mapa; }
        else if (ma_mapaIntegrado.activeInHierarchy == true) { currentScreen = ma_mapaIntegrado; }


        else if (b_buscar.activeInHierarchy == true) { currentScreen = b_buscar; }


        else if (changelog.activeInHierarchy == true)   { currentScreen = changelog; }
        else if (mainSettings.activeInHierarchy == true) { currentScreen = mainSettings; }
    }
}
