using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveObjectData : MonoBehaviour
{
    public TMP_InputField PrefabNameInput;
    public TMP_InputField PosXInput, PosYInput, PosZInput;
    public TMP_InputField RotXInput, RotYInput, RotZInput;
    public TMP_InputField ScaleXInput, ScaleYInput, ScaleZInput;

    public void SaveData()
    {
        // Safety check for null references
        if (PrefabNameInput == null || PosXInput == null || PosYInput == null || PosZInput == null ||
            RotXInput == null || RotYInput == null || RotZInput == null ||
            ScaleXInput == null || ScaleYInput == null || ScaleZInput == null)
        {
            Debug.LogError("❌ One or more input fields are not assigned in the Inspector.");
            return;
        }

        // Check for empty fields
        if (string.IsNullOrWhiteSpace(PrefabNameInput.text) ||
            string.IsNullOrWhiteSpace(PosXInput.text) || string.IsNullOrWhiteSpace(PosYInput.text) || string.IsNullOrWhiteSpace(PosZInput.text) ||
            string.IsNullOrWhiteSpace(RotXInput.text) || string.IsNullOrWhiteSpace(RotYInput.text) || string.IsNullOrWhiteSpace(RotZInput.text) ||
            string.IsNullOrWhiteSpace(ScaleXInput.text) || string.IsNullOrWhiteSpace(ScaleYInput.text) || string.IsNullOrWhiteSpace(ScaleZInput.text))
        {
            Debug.LogError("⚠️ One or more fields are empty. Please fill all fields.");
            return;
        }

        // Parse input
        float posX = float.Parse(PosXInput.text);
        float posY = float.Parse(PosYInput.text);
        float posZ = float.Parse(PosZInput.text);

        float rotX = float.Parse(RotXInput.text);
        float rotY = float.Parse(RotYInput.text);
        float rotZ = float.Parse(RotZInput.text);

        float scaleX = float.Parse(ScaleXInput.text);
        float scaleY = float.Parse(ScaleYInput.text);
        float scaleZ = float.Parse(ScaleZInput.text);

        // Create data
        ObjectData data = new ObjectData
        {
            prefabName = PrefabNameInput.text.ToLower().Trim(), // lowercase to match Resources folder prefab
            position = new Vector3(posX, posY, posZ),
            rotation = new Vector3(rotX, rotY, rotZ),
            scale = new Vector3(scaleX, scaleY, scaleZ),
            returnKey = "" // Optional use
        };

        // Save as JSON
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "data.json");
        File.WriteAllText(path, json);

        Debug.Log("✅ Data saved to: " + path);

        // Load next scene (change to your actual scene name)
        SceneManager.LoadScene("Scene10 1");
    }

    [System.Serializable]
    public class ObjectData
    {
        public string prefabName;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public string returnKey;
    }
}
