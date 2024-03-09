using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DatabaseSelectorController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown databaseDropdown; // Assign this in the Unity Inspector

    public string selectedDatabase = "";
    public string[] modelsInDatabase;

    private void Start()
    {
        // Ensure the dropdown is assigned
        if (databaseDropdown == null)
        {
            Debug.LogError("DatabaseSelectorController: The databaseDropdown is not assigned.");
            return;
        }

        // Add listener for the dropdown value changes
        databaseDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(databaseDropdown); });
    }

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        // Fetch the database to model mapping from XML
        var xmlReader = GameObject.FindGameObjectWithTag("GameController").GetComponent<ModelTargetDatabaseXmlReader>();
        var databaseModelTargets = xmlReader.GetAllModelTargetNames();

        if (databaseModelTargets == null || databaseModelTargets.Count == 0)
        {
            Debug.LogWarning("Could not fetch model target names from the database or no databases are available.");
            return;
        }

        // Reset or set the selected database based on dropdown selection
        if (index > 0 && index <= dropdown.options.Count) // Ensure index is within the valid range
        {
            selectedDatabase = dropdown.options[index].text;
        }
        else
        {
            selectedDatabase = ""; // This signifies no valid selection is made or a placeholder is selected
        }

        Debug.Log($"Selected Database: {selectedDatabase}");

        // Determine models to be assigned based on the selected database
        if (!string.IsNullOrEmpty(selectedDatabase))
        {
            if (databaseModelTargets.ContainsKey(selectedDatabase))
            {
                // Fetch models from the selected database
                modelsInDatabase = databaseModelTargets[selectedDatabase].ToArray();
            }
            else
            {
                // Handle the case where the selected database name does not directly map to a key in the dictionary
                // This might mean the dropdown has a special entry or an error in selection logic
                Debug.LogWarning($"Selected database '{selectedDatabase}' does not directly map to available data.");
                modelsInDatabase = new string[0]; // Fallback to an empty array
            }
        }
        else
        {
            // If no valid selection is made, this can serve to reset or handle "Please select" cases
            modelsInDatabase = new string[0]; // Assign an empty array
        }

        // Add any additional functionality needed for the new selection
        // E.g., updating UI elements or initiating data loading based on the selected models
    }
}
