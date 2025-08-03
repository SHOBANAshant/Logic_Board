using UnityEngine;
using UnityEngine.UI;
using System.IO;
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
        // Safety check: Ensure all input fields are assigned in the Inspector
        if (PrefabNameInput == null || PosXInput == null || PosYInput == null || PosZInput == null ||
            RotXInput == null || RotYInput == null || RotZInput == null ||
            ScaleXInput == null || ScaleYInput == null || ScaleZInput == null)
        {
            Debug.LogError("❌ One or more input fields are not assigned in the Inspector.");
            return;
        }

        // Check for empty values
        if (string.IsNullOrWhiteSpace(PrefabNameInput.text) ||
            string.IsNullOrWhiteSpace(PosXInput.text) ||
            string.IsNullOrWhiteSpace(PosYInput.text) ||
            string.IsNullOrWhiteSpace(PosZInput.text) ||
            string.IsNullOrWhiteSpace(RotXInput.text) ||
            string.IsNullOrWhiteSpace(RotYInput.text) ||
            string.IsNullOrWhiteSpace(RotZInput.text) ||
            string.IsNullOrWhiteSpace(ScaleXInput.text) ||
            string.IsNullOrWhiteSpace(ScaleYInput.text) ||
            string.IsNullOrWhiteSpace(ScaleZInput.text))
        {
            Debug.LogError("⚠️ One or more fields are empty. Please fill all fields before proceeding.");
            return;
        }

        // Parse input values to float
        float x = float.Parse(PosXInput.text);
        float y = float.Parse(PosYInput.text);
        float z = float.Parse(PosZInput.text);

        float rx = float.Parse(RotXInput.text);
        float ry = float.Parse(RotYInput.text);
        float rz = float.Parse(RotZInput.text);

        float sx = float.Parse(ScaleXInput.text);
        float sy = float.Parse(ScaleYInput.text);
        float sz = float.Parse(ScaleZInput.text);

        // Create and populate object data
        ObjectData data = new ObjectData
        {
            prefabName = PrefabNameInput.text.ToLower(),
            position = new Vector3(x, y, z),
            rotation = new Vector3(rx, ry, rz),
            scale = new Vector3(sx, sy, sz),
            returnKey = ""
        };

        // Serialize to JSON and save to file
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "data.json");
        File.WriteAllText(path, json);
        Debug.Log("✅ Data saved to: " + path);

        // Load next scene
        SceneManager.LoadScene("Scene10 1");
    }
}
