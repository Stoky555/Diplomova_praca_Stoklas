using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PopulateDropdownController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown; // Assign this in the Unity Inspector

    [SerializeField]
    private ManifestLoader manifestLoader; // Assign this in the Unity Inspector

    private void Start()
    {
        if (dropdown == null)
        {
            Debug.LogError("PopulateDropdown: Dropdown component is not assigned.");
            return;
        }

        if (manifestLoader == null)
        {
            Debug.LogError("ManifestLoader component is not assigned.");
            return;
        }

        PopulateDropdownWithDatabaseNames();
    }

    public void PopulateDropdownWithDatabaseNames()
    {
        List<string> options = new List<string> { "Choose database" };

        options.AddRange(manifestLoader.GetAllDatabaseNamesFromManifest());

        foreach (string name in manifestLoader.GetAllDatabaseNamesFromManifest())
        {
            Debug.Log("[PopulateDropdownController] [PopulateDropdownWithDatabaseNames] Name of database: " + name);
        }

        // Populate the dropdown with these options
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    ////public void PopulateDropdownWithXMLFiles()
    ////{
    ////    string path = Path.Combine(Application.streamingAssetsPath, "Vuforia");
    ////    List<string> options = new List<string>();

    ////    // Verify the directory exists
    ////    if (!Directory.Exists(path))
    ////    {
    ////        Debug.LogWarning($"PopulateDropdown: Directory does not exist at path: {path}");
    ////        return;
    ////    }

    ////    try
    ////    {
    ////        // Get all XML files in the directory
    ////        string[] files = Directory.GetFiles(path, "*.xml");
    ////        if (files.Length == 0)
    ////        {
    ////            Debug.LogWarning("PopulateDropdown: No XML files found in the directory.");
    ////            return;
    ////        }

    ////        options.Add("Choose database");

    ////        // Extract and add file names (without extension) to the options list
    ////        foreach (string file in files)
    ////        {
    ////            options.Add(Path.GetFileNameWithoutExtension(file));
    ////        }

    ////        // Populate the dropdown with these options
    ////        dropdown.ClearOptions();
    ////        dropdown.AddOptions(options);

    ////        databases = options;
    ////    }
    ////    catch (System.Exception ex)
    ////    {
    ////        Debug.LogError($"PopulateDropdown: Failed to read XML files from directory. Error: {ex.Message}");
    ////        databases = new List<string>();
    ////    }
    ////}
}
