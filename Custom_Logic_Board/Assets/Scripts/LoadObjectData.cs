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
        StartCoroutine(LoadProjectObjects());
    }

    IEnumerator LoadProjectObjects()
    {
        // 1. Get current project
        ProjectData proj = ProjectManager.GetCurrentProject();
        if (proj == null)
        {
            Debug.LogError("❌ No active project selected!");
            yield break;
        }

        // 2. Download AssetBundle
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("❌ Failed to download AssetBundle: " + www.error);
            yield break;
        }

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
        string[] allAssets = bundle.GetAllAssetNames();

        // 3. Loop through all objects in the project
        foreach (ObjectData data in proj.objects)
        {
            GameObject prefab = null;

            foreach (string asset in allAssets)
            {
                string cleanName = Path.GetFileNameWithoutExtension(asset);

                if (cleanName.Equals(data.prefabName, System.StringComparison.OrdinalIgnoreCase))
                {
                    prefab = bundle.LoadAsset<GameObject>(asset);
                    break;
                }
            }

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, data.position, Quaternion.Euler(data.rotation));
                obj.transform.localScale = data.scale;
                Debug.Log("✅ Instantiated prefab: " + data.prefabName);
            }
            else
            {
                Debug.LogError("❌ Prefab not found in AssetBundle: " + data.prefabName);
            }
        }

        // 4. Unload bundle but keep loaded objects alive
        bundle.Unload(false);
    }
}
