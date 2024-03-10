using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManifestLoader : MonoBehaviour
{
    public List<string> databaseNames = new List<string>();

    //void Start()
    //{
    //    TextAsset manifestTextAsset = Resources.Load<TextAsset>("manifest");
    //    if (manifestTextAsset != null)
    //    {
    //        databaseNames = new List<string>(manifestTextAsset.text.Split('\n'));
    //        // Remove any empty entries or potential line break issues.
    //        databaseNames.RemoveAll(string.IsNullOrEmpty);

    //        foreach(string databaseName in databaseNames)
    //        {
    //            Debug.LogError("[ManifestLoader] [Start] Database names: " + databaseName);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Failed to load the manifest file.");
    //    }
    //}

    public List<string> GetAllDatabaseNamesFromManifest()
    {
        TextAsset manifestTextAsset = Resources.Load<TextAsset>("manifest");
        if (manifestTextAsset != null)
        {
            databaseNames = new List<string>(manifestTextAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            // Remove any empty entries or potential line break issues.
            databaseNames.RemoveAll(string.IsNullOrEmpty);

            foreach (string databaseName in databaseNames)
            {
                Debug.Log("[ManifestLoader] [Start] Database names: " + databaseName);
            }
        }
        else
        {
            Debug.LogError("Failed to load the manifest file.");
        }

        return databaseNames;
    }
}