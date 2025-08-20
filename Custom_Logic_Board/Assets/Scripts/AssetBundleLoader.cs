using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{
    // URL of your AssetBundle (local server or cloud)
    string bundleUrl = "http://localhost:8080/allprefabs";  // change if you upload to cloud

    IEnumerator Start()
    {
        // 1. Download the AssetBundle from server
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("❌ Download failed: " + www.error);
            yield break;
        }

        // 2. Get the bundle from the downloaded data
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

        // 3. Load every prefab stored inside the bundle
        foreach (string assetName in bundle.GetAllAssetNames())
        {
            GameObject prefab = bundle.LoadAsset<GameObject>(assetName);
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
            Debug.Log("✅ Loaded prefab: " + assetName);
        }

        // 4. Unload the bundle to free memory (keeps the instantiated prefabs alive)
        bundle.Unload(false);
    }
}
