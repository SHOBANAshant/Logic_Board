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
    public TMP_InputField KeyInput;

    public void SaveData()
    {
        if (string.IsNullOrWhiteSpace(PrefabNameInput.text) ||
            string.IsNullOrWhiteSpace(PosXInput.text) ||
            string.IsNullOrWhiteSpace(PosYInput.text) ||
            string.IsNullOrWhiteSpace(PosZInput.text) ||
            string.IsNullOrWhiteSpace(RotXInput.text) ||
            string.IsNullOrWhiteSpace(RotYInput.text) ||
            string.IsNullOrWhiteSpace(RotZInput.text) ||
            string.IsNullOrWhiteSpace(ScaleXInput.text) ||
            string.IsNullOrWhiteSpace(ScaleYInput.text) ||
            string.IsNullOrWhiteSpace(ScaleZInput.text) ||
            string.IsNullOrWhiteSpace(KeyInput.text))
        {
            Debug.LogError("One or more fields are empty. Please fill all fields before proceeding.");
            return;
        }

        float x = float.Parse(PosXInput.text);
        float y = float.Parse(PosYInput.text);
        float z = float.Parse(PosZInput.text);

        float rx = float.Parse(RotXInput.text);
        float ry = float.Parse(RotYInput.text);
        float rz = float.Parse(RotZInput.text);

        float sx = float.Parse(ScaleXInput.text);
        float sy = float.Parse(ScaleYInput.text);
        float sz = float.Parse(ScaleZInput.text);

        ObjectData data = new ObjectData
        {
            prefabName = PrefabNameInput.text.ToLower(),
            position = new Vector3(x, y, z),
            rotation = new Vector3(rx, ry, rz),
            scale = new Vector3(sx, sy, sz),
            returnKey = KeyInput.text.ToLower()
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/data.json", json);

        
        SceneManager.LoadScene("Scene10");
    }
}
