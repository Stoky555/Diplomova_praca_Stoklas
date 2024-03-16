using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDatabaseController : MonoBehaviour
{
    public GameObject ChangeDatabaseButton;
    public GameObject UseThisDatabaseButton;
    public GameObject DatabaseDropdown;

    private TMP_Dropdown databaseDropdownComponent;

    // Use this for initialization
    void Start()
    {
        // Initially, DatabaseDropdown and UseThisDatabaseButton are visible, ChangeDatabaseButton is not
        DatabaseDropdown.SetActive(true);
        UseThisDatabaseButton.SetActive(true);
        ChangeDatabaseButton.SetActive(false);

        // Get the TMP_Dropdown component
        databaseDropdownComponent = DatabaseDropdown.GetComponent<TMP_Dropdown>();

        // Assuming you have Button components attached to your GameObjects, add click listeners here
        UseThisDatabaseButton.GetComponent<Button>().onClick.AddListener(() => OnUseThisDatabaseClicked());
        ChangeDatabaseButton.GetComponent<Button>().onClick.AddListener(() => OnChangeDatabaseClicked());
    }

    void OnUseThisDatabaseClicked()
    {
        // Check if the selected option is not the default one (assuming the default is at index 0)
        if (databaseDropdownComponent.value != 0)
        {
            // Hide UseThisDatabaseButton and DatabaseDropdown
            UseThisDatabaseButton.SetActive(false);
            DatabaseDropdown.SetActive(false);

            // Show ChangeDatabaseButton
            ChangeDatabaseButton.SetActive(true);
        }
        else
        {
            // Optionally, you can handle the case where the default option is selected
            Debug.Log("Default option selected. Please select a different database.");
        }
    }

    void OnChangeDatabaseClicked()
    {
        // Hide ChangeDatabaseButton
        ChangeDatabaseButton.SetActive(false);

        // Show DatabaseDropdown and UseThisDatabaseButton for new selection
        DatabaseDropdown.SetActive(true);
        UseThisDatabaseButton.SetActive(true);
    }
}