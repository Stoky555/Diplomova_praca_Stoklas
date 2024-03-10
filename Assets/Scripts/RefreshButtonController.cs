using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshButtonController : MonoBehaviour
{
    public void RefreshModels()
    {
        // Find the GameController GameObject in the scene.
        GameObject gameController = GameObject.Find("GameController");
        if (gameController != null)
        {
            // Attempt to retrieve the LoadMultipleTargetsFromDatabase script component from the GameController GameObject.
            LoadMultipleTargetsFromDatabase loadTargetsScript = gameController.GetComponent<LoadMultipleTargetsFromDatabase>();

            if (loadTargetsScript != null)
            {
                // Call a method on the script to destroy the current targets.
                // This method must be implemented in the LoadMultipleTargetsFromDatabase script.
                loadTargetsScript.DestroyAllModelTargets();

                // Call a method on the script to create (or recreate) targets.
                // This method must also be implemented in the LoadMultipleTargetsFromDatabase script.
                loadTargetsScript.CreateIT();
            }
            else
            {
                Debug.LogError("LoadMultipleTargetsFromDatabase script not found on GameController object.");
            }
        }
        else
        {
            Debug.LogError("GameController object not found in the scene.");
        }
    }
}
