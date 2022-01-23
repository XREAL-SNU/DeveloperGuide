/*
Path: Assets/Editor
*/

#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
public class ResourcesPrefabPathBuilder : IPreprocessBuildWithReport {
    public int callbackOrder => 0;
    // public int callbackOrder = { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report) {
        MasterManager.PopulateNetworkedPrefabs();
    }
}
#endif