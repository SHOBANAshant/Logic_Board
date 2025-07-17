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
            }
            else
            {
                Debug.LogError("Prefab not found: " + data.prefabName);
            }
        }
    }
}