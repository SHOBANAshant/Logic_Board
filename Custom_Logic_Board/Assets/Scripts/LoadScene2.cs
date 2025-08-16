using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadScene2 : MonoBehaviour
{
    public void LoadSceneWithObject()
    {
        string path = Application.persistentDataPath + "/data.json";

        if (!File.Exists(path))
        {
            Debug.LogError("Cannot load Scene2: data.json not found.");
            return;
        }

        string json = File.ReadAllText(path);
        ObjectData data = JsonUtility.FromJson<ObjectData>(json);

        if (string.IsNullOrEmpty(data.returnKey))
        {
            Debug.LogError("Cannot load Scene2: returnKey is missing. Please assign a return key.");
            return;
        }

       
        

        
        SceneManager.LoadScene("Scene2");
    }
}
