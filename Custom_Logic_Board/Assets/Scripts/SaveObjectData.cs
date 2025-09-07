using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveObjectData : MonoBehaviour
{
    public TMP_InputField PrefabNameInput;
    public TMP_InputField PosXInput, PosYInput, PosZInput;
    public TMP_InputField RotXInput, RotYInput, RotZInput;
    public TMP_InputField ScaleXInput, ScaleYInput, ScaleZInput;

    public void SaveData()
    {
        // Only Prefab Name is required
        if (string.IsNullOrWhiteSpace(PrefabNameInput.text))
        {
            Debug.LogError("❌ Prefab Name is required.");
            return;
        }

        // Ensure a project is selected
        ProjectData proj = ProjectManager.GetCurrentProject();
        if (proj == null)
        {
            Debug.LogError("⚠️ No active project selected! Please create/select a project first.");
            return;
        }

        // --- Position (default 1,1,1) ---
        float x = SafeParse(PosXInput.text, 1f);
        float y = SafeParse(PosYInput.text, 1f);
        float z = SafeParse(PosZInput.text, 1f);

        // --- Rotation (default 0,0,0) ---
        float rx = SafeParse(RotXInput.text, 0f);
        float ry = SafeParse(RotYInput.text, 0f);
        float rz = SafeParse(RotZInput.text, 0f);

        // --- Scale (default 1,1,1) ---
        float sx = SafeParse(ScaleXInput.text, 1f);
        float sy = SafeParse(ScaleYInput.text, 1f);
        float sz = SafeParse(ScaleZInput.text, 1f);

        // Create object data
        ObjectData data = new ObjectData
        {
            prefabName = PrefabNameInput.text.ToLower(),
            position = new Vector3(x, y, z),
            rotation = new Vector3(rx, ry, rz),
            scale = new Vector3(sx, sy, sz)
        };

        // Save object inside the project
        proj.objects.Add(data);
        ProjectManager.Save();

        Debug.Log("✅ Object saved to project: " + proj.projectName);

        // Go back to editor scene
        SceneManager.LoadScene("Scene10 1");
    }

    // Helper to safely parse float or use default
    private float SafeParse(string input, float defaultValue)
    {
        if (string.IsNullOrWhiteSpace(input)) return defaultValue;
        if (float.TryParse(input, out float result))
            return result;
        return defaultValue;
    }
}
