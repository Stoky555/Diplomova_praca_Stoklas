using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DatabaseSelectorController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown databaseDropdown; // Assign this in the Unity Inspector
    [SerializeField] private ManifestLoader manifestLoader; // Assign this in the Unity Inspector
    [SerializeField] private ModelTargetDatabaseXmlReader xmlReader; // Assign this in the Unity Inspector

    public string selectedDatabase = "";
    public string[] modelsInDatabase;

    private void Start()
    {
        try
        {
            if (databaseDropdown == null)
            {
                Debug.LogError("DatabaseSelectorController: The databaseDropdown is not assigned.");
                return;
            }

            if (manifestLoader == null)
            {
                Debug.LogError("DatabaseSelectorController: The manifestLoader is not assigned.");
                return;
            }

            if (xmlReader == null)
            {
                Debug.LogError("DatabaseSelectorController: The xmlReader is not assigned.");
                return;
            }

            PopulateDropdownWithDatabaseNames();
            databaseDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(databaseDropdown); });
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"DatabaseSelectorController Start method encountered an exception: {ex.Message}");
        }
    }

    private void PopulateDropdownWithDatabaseNames()
    {
        try
        {
            List<string> options = new List<string> { "Choose database" };
            options.AddRange(manifestLoader.GetAllDatabaseNamesFromManifest());

            foreach(string name in manifestLoader.GetAllDatabaseNamesFromManifest())
            {
                Debug.Log("[DatabaseSelectorController] [PopulateDropdownWithDatabaseNames] Name of database: " + name);
            }

            databaseDropdown.ClearOptions();
            databaseDropdown.AddOptions(options);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error populating database names in dropdown: {ex.Message}");
        }
    }

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        try
        {
            int index = dropdown.value;

            // Ensure index is within the valid range
            if (index > 0 && index <= dropdown.options.Count)
            {
                selectedDatabase = dropdown.options[index].text;
                LoadModelsForSelectedDatabase(selectedDatabase);
            }
            else
            {
                selectedDatabase = ""; // Placeholder or invalid selection
                modelsInDatabase = new string[0];
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error occurred on dropdown value change: {ex.Message}");
        }
    }

    private void LoadModelsForSelectedDatabase(string databaseName)
    {
        try
        {
            xmlReader.GetAllModelTargetNames(manifestLoader.GetAllDatabaseNamesFromManifest(), databaseModelTargets =>
            {
                if (databaseModelTargets == null || databaseModelTargets.Count == 0)
                {
                    Debug.LogWarning("Could not fetch model target names from the database or no databases are available.");
                    modelsInDatabase = new string[0];
                    return;
                }

                if (!string.IsNullOrEmpty(databaseName) && databaseModelTargets.ContainsKey(databaseName))
                {
                    // Fetch models from the selected database
                    modelsInDatabase = databaseModelTargets[databaseName].ToArray();

                    foreach (string name in modelsInDatabase)
                    {
                        Debug.Log("[DatabaseSelectorController] [LoadModelsForSelectedDatabase] Name of database: " + name);
                    }
                }
                else
                {
                    Debug.LogWarning($"Selected database '{databaseName}' does not directly map to available data.");
                    modelsInDatabase = new string[0]; // Fallback to an empty array
                }

                // Here, you could update the UI or do other actions based on the new modelsInDatabase
                // This might need to be executed on the main thread if you're modifying the UI, for example:
                // this.Invoke(() => UpdateUIWithModels(modelsInDatabase));
            });
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading models for selected database '{databaseName}': {ex.Message}");
            modelsInDatabase = new string[0]; // Fallback to an empty array in case of an exception
        }
    }
}
