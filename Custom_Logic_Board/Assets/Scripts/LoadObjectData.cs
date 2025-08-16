using UnityEngine;
using System.IO;

public class LoadObjectData : MonoBehaviour
{
    void Start()
    {
        string path = Application.persistentDataPath + "/data.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ObjectData data = JsonUtility.FromJson<ObjectData>(json);

            GameObject prefab = Resources.Load<GameObject>(data.prefabName);
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, data.position, Quaternion.Euler(data.rotation));
                obj.transform.localScale = data.scale;
                Debug.Log("✅ Instantiated: " + data.prefabName);
            }
            else
            {
                Debug.LogError("❌ Prefab not found in Resources: " + data.prefabName);
            }
        }
        else
        {
            Debug.LogError("❌ Data file not found: " + path);
        }
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
