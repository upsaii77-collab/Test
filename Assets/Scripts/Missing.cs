using UnityEngine;
using UnityEditor;

public class RemoveMissingScripts 
{
    [MenuItem("Tools/Remove Missing Scripts in Scene")]
    public static void RemoveMissingScriptsInScene()
    {
        var objects = GameObject.FindObjectsOfType<GameObject>();

        int count = 0;

        foreach (var go in objects)
        {
            count += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        }

        Debug.Log($"Removed {count} missing scripts!");
    }
}

