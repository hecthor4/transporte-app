using UnityEngine;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{
    public string currentUI = "old";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentUI = PlayerPrefs.GetString("currentUI", "old");

        ApplyChanges();
    }

    void ApplyChanges()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        int activeSceneInt = activeScene.buildIndex;

        if (currentUI == "old" && activeSceneInt == 1)
        {
            SceneManager.LoadScene(0);
        }
        if (currentUI == "new" && activeSceneInt == 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void SwitchUI()
    {
        if (currentUI == "old")
        {
            currentUI = "new";
        }
        else if (currentUI == "new")
        {
            currentUI = "old";
        }

        PlayerPrefs.SetString("currentUI", currentUI);
        PlayerPrefs.Save();
        ApplyChanges();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
