using UnityEditor;
using System.IO;

public class BuildAssetBundles {
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles() {
        string path = System.IO.Path.Combine(
    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
    "MyAssetBundles"
);

        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
