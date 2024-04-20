using UnityEngine;
using TMPro; // TMPro for TextMeshProUGUI
using System.Collections; // For IEnumerator
using UnityEngine.UI; // UI components, specifically for Button

// Manages the display of tooltips in the UI
public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance; // Singleton instance of TooltipManager
    public TextMeshProUGUI helpText; // Text component to display the tooltip text
    public GameObject tooltipPanel; // UI Panel that contains the tooltip
    public Vector3 offset = new Vector3(-40, 40, 0); // Offset to position the tooltip relative to the button
    private Coroutine displayCoroutine = null; // Reference to the currently running tooltip display Coroutine
    private Transform lastButtonTransform; // The transform of the last button that triggered the tooltip

    // Ensures that only one instance of the TooltipManager exists
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject); // Destroy duplicate
        }
        else
        {
            _instance = this; // Set the singleton instance
        }
    }

    // Initialize tooltipPanel state
    void Start()
    {
        tooltipPanel.SetActive(false); // Initially hide the tooltip panel
    }

    // Toggles the visibility of the tooltip based on the button's transform
    public void ToggleTooltip(Transform buttonTransform)
    {
        lastButtonTransform = buttonTransform; // Store the transform of the button that triggered the tooltip

        if (tooltipPanel.activeSelf)
        {
            StopAndHideTooltip(); // Hide tooltip if it's currently visible
        }
        else
        {
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine); // Stop the current coroutine if one is running
            }
            displayCoroutine = StartCoroutine(ShowTooltipForDuration(10)); // Show tooltip for 10 seconds
        }
    }

    // Coroutine to show the tooltip for a specific duration
    private IEnumerator ShowTooltipForDuration(float duration)
    {
        PositionTooltip(); // Set the tooltip's position
        tooltipPanel.SetActive(true); // Make the tooltip visible
        yield return new WaitForSeconds(duration); // Wait for the duration
        StopAndHideTooltip(); // Hide the tooltip after the wait
    }

    // Positions the tooltip relative to the last button's position plus an offset
    private void PositionTooltip()
    {
        if (lastButtonTransform != null)
        {
            tooltipPanel.transform.position = lastButtonTransform.position + offset; // Adjust position based on offset
        }
    }

    // Stops the display coroutine and hides the tooltip
    private void StopAndHideTooltip()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine); // Stop the running coroutine
            displayCoroutine = null; // Clear the coroutine reference
        }
        tooltipPanel.SetActive(false); // Hide the tooltip panel
    }

    // Called when the help button is clicked, toggles the tooltip for the button
    public void OnHelpButtonClick(Button button)
    {
        TooltipManager._instance.ToggleTooltip(button.transform); // Trigger tooltip toggle for the button
    }
}