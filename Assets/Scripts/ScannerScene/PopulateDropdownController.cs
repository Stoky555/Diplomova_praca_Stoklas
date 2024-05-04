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
}
