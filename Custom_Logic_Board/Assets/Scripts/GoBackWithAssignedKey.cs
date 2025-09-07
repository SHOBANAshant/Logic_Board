using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackWithAssignedKey : MonoBehaviour
{
    private KeyCode assignedKey;
    private bool hasKeyAssigned = false;

    void Start()
    {
        ProjectData proj = ProjectManager.GetCurrentProject();
        if (proj == null)
        {
            Debug.LogWarning("⚠️ No active project selected.");
            return;
        }

        if (string.IsNullOrEmpty(proj.returnKey))
        {
            Debug.LogWarning("⚠️ No return key assigned for this project.");
            return;
        }

        // Try parsing the saved key
        if (System.Enum.TryParse(proj.returnKey, true, out assignedKey))
        {
            hasKeyAssigned = true;
            Debug.Log("✅ Home key loaded: " + assignedKey);
        }
        else
        {
            Debug.LogWarning($"⚠️ Invalid key '{proj.returnKey}' in project {proj.projectName}");
        }
    }

    void Update()
    {
        if (!hasKeyAssigned) return;

        if (Input.GetKeyDown(assignedKey))
        {
            SceneManager.LoadScene("ProjectManager 1"); // go back to main scene
        }
    }
}
