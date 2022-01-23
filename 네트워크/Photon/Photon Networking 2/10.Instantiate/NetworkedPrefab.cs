/*
Path: Assets/Scripts/MasterManager/Managers
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkedPrefab {
    public GameObject Prefab;
    public string Path;

    public NetworkedPrefab(GameObject obj, string path) {
        Prefab = obj;
        Path = ReturnPrefabPathModified(path);
    }

    private string ReturnPrefabPathModified(string path) {
        int extensionLength = System.IO.Path.GetExtension(path).Length;
        int additionalLength = 10;
        int startIndex = path.ToLower().IndexOf("resources");

        if(startIndex == -1) {
            Debug.LogError("Prefab path is not in Resources folder: " + path);
            return null;
        }
        return path.Substring(startIndex + additionalLength, path.Length - (additionalLength + startIndex + extensionLength));
    }
}
