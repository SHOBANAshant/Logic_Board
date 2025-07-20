using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;



public class GoBackWithAssignedKey : MonoBehaviour
{
    private string userKey = "";
    private KeyCode keyCode;
    private bool isInputMissing = false;

   void Start()
{
    string path = Application.persistentDataPath + "/data.json";

    if (File.Exists(path))
    {
        string json = File.ReadAllText(path);
        ObjectData data = JsonUtility.FromJson<ObjectData>(json);

        userKey = data.returnKey;

        if (string.IsNullOrEmpty(userKey))
        {
            isInputMissing = true;
            Debug.LogWarning("Please enter a value");
            return;
        }
    }
    else
    {
        isInputMissing = true;
        Debug.LogWarning("data.json not found. Please create the file.");
        return;
    }

    // Handle number keys explicitly (0-9)
    if (userKey.Length == 1 && char.IsDigit(userKey[0]))
    {
        int number = int.Parse(userKey);
        keyCode = KeyCode.Alpha0 + number;
    }
    else
    {
        if (!System.Enum.TryParse(userKey, true, out keyCode))
        {
            isInputMissing = true;
            Debug.LogWarning($"The key '{userKey}' is invalid. Please enter a valid KeyCode");
        }
    }
}


    void Update()
    {
        if (isInputMissing)
        {
            return;
        }

        if (Input.GetKeyDown(keyCode))
        {
            SceneManager.LoadScene("Scene10");
        }
    }
}