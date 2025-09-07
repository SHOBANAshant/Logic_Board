using UnityEngine;
using TMPro;

public class KeyAssigner : MonoBehaviour
{
    public GameObject promptPanel;         
    public TMP_Text promptText;
    public GameObject modulesPanel;        
    private bool isListening = false;

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
                    AssignKeyToProject(key);
                    break;
                }
            }
        }
    }

    private void AssignKeyToProject(KeyCode key)
    {
        ProjectData proj = ProjectManager.GetCurrentProject();
        if (proj == null)
        {
            Debug.LogError("⚠️ No active project found. Please create/select a project first.");
            return;
        }

        proj.returnKey = key.ToString();
        ProjectManager.Save();

        Debug.Log("✅ Assigned return key: " + key);

        promptText.text = "Key Assigned: " + key;

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
