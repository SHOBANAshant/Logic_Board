using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public class LoadObjectData : MonoBehaviour
{
    // URL of your bundle file
    string bundleUrl = "http://localhost:8080/allprefabs";

    void Start()
    {
        StartCoroutine(LoadDataAndPrefab());
    }

    IEnumerator LoadDataAndPrefab()
    {
        string path = Application.persistentDataPath + "/data.json";
        if (!File.Exists(path))
        {
            Debug.LogError(" Data file not found: " + path);
            yield break;
        }

        // 1. Read JSON data
        string json = File.ReadAllText(path);
        ObjectData data = JsonUtility.FromJson<ObjectData>(json);

        // 2. Download AssetBundle
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download AssetBundle: " + www.error);
            yield break;
        }

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

        // 3. Load prefab from bundle
        GameObject prefab = bundle.LoadAsset<GameObject>(data.prefabName);
        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab, data.position, Quaternion.Euler(data.rotation));
            obj.transform.localScale = data.scale;
            Debug.Log("Instantiated prefab from bundle: " + data.prefabName);
        }
        else
        {
            Debug.LogError("Prefab not found in AssetBundle: " + data.prefabName);
        }

        // 4. Free memory (keep instantiated object alive)
        bundle.Unload(false);
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
