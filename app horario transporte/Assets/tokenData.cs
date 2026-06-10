using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// Estructuras para parsear la respuesta JSON del Login
[System.Serializable]
public class tokenData
{
    public string accessToken;
}

[System.Serializable]
public class LoginResponse
{
    public string code;
    public string description;
    public tokenData[] data; // La API devuelve un array "data"
}

public class EmtManager : MonoBehaviour
{
    // Reemplaza estos valores con los que obtuviste en MobilityLabs
    private string clientId = "TU_X_CLIENT_ID";
    private string passKey = "TU_PASS_KEY";

    // Aquí guardaremos el token temporal de forma dinámica
    private string currentAccessToken = "";

    IEnumerator LoginEmt()
    {
        string url = "https://emtmadrid.es";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Se envían las credenciales fijas en las cabeceras
            webRequest.SetRequestHeader("X-ClientId", clientId);
            webRequest.SetRequestHeader("passKey", passKey);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en Login: " + webRequest.error);
            }
            else
            {
                string jsonResult = webRequest.downloadHandler.text;

                // Parsear el JSON para extraer el accessToken
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResult);

                if (response != null && response.data != null && response.data.Length > 0)
                {
                    currentAccessToken = response.data[0].accessToken;
                    Debug.Log("¡Token obtenido con éxito!: " + currentAccessToken);
                }
                else
                {
                    Debug.LogError("La respuesta de la EMT no contiene un token válido.");
                }
            }
        }
    }
}
