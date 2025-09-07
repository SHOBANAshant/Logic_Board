using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene2 : MonoBehaviour
{
    public void LoadSceneWithObject()
    {
        // Get current project
        ProjectData proj = ProjectManager.GetCurrentProject();
        if (proj == null)
        {
            Debug.LogError("⚠️ Cannot load Scene2: No active project selected.");
            return;
        }

        if (string.IsNullOrEmpty(proj.returnKey))
        {
            Debug.LogError("⚠️ Cannot load Scene2: returnKey is missing. Please assign a return key.");
            return;
        }

        if (proj.objects == null || proj.objects.Count == 0)
        {
            Debug.LogWarning("⚠️ Scene2 loaded, but this project has no objects.");
        }

        // ✅ Load Scene2
        SceneManager.LoadScene("Scene2");
    }
}
