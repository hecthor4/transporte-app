using UnityEngine;

public class DeleteEverything : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete()
    {
        ajustes ajustes = FindFirstObjectByType<ajustes>();
        ajustes.ResetValues("font");
        ajustes.ResetValues("metro");
        ajustes.ResetValues("renfe");
        ajustes.ResetValues("bus");

        favorites favorites = FindFirstObjectByType<favorites>();
        favorites.EraseAllData();

        fps fps = FindFirstObjectByType<fps>();
        fps.RestoreFPS();
    }
}
