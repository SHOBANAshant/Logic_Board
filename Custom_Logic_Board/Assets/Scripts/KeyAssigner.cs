using UnityEngine;
using TMPro;
using System.IO;

public class KeyAssigner : MonoBehaviour
{
    public GameObject promptPanel;         
    public TMP_Text promptText;
    public GameObject modulesPanel;        
    private bool isListening = false;

    private string prefabPath => Application.persistentDataPath + "/data.json";

    public void BeginKeyAssignment()
    {
        promptPanel.SetActive(true);
        promptText.text = "Press any key to assign return key";
        isListening = true;

        if (modulesPanel != null)
        {
            modulesPanel.SetActive(false); 
        }
    }

    void Update()
    {
        if (!isListening) return;

        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    AssignKeyToJson(key);
                    break;
                }
            }
        }
    }

    private void AssignKeyToJson(KeyCode key)
    {
        if (File.Exists(prefabPath))
        {
            string json = File.ReadAllText(prefabPath);
            ObjectData data = JsonUtility.FromJson<ObjectData>(json);

            data.returnKey = key.ToString();  
            string updatedJson = JsonUtility.ToJson(data);
            File.WriteAllText(prefabPath, updatedJson);

            Debug.Log("Assigned return key: " + key);

            promptText.text = "Key Assigned: " + key;
        }
        else
        {
            Debug.LogError("data.json not found.");
        }

        isListening = false;
        Invoke("HidePrompt", 1.5f); 
    }

    private void HidePrompt()
    {
        promptPanel.SetActive(false);

        if (modulesPanel != null)
        {
            modulesPanel.SetActive(true); 
        }
    }
}
