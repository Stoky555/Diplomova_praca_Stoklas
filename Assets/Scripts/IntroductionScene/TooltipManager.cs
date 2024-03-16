using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI; // For IEnumerator

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance;
    public TextMeshProUGUI helpText;
    public GameObject tooltipPanel; // Reference to the tooltip panel
    public Vector3 offset = new Vector3(-40, 40, 0); // Offset from the button position
    private Coroutine displayCoroutine = null; // To keep track of the coroutine
    private Transform lastButtonTransform; // To store the last button's transform for positioning

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        tooltipPanel.SetActive(false); // Ensure the tooltip panel is initially hidden
    }

    public void ToggleTooltip(Transform buttonTransform)
    {
        lastButtonTransform = buttonTransform; // Store the current button's transform

        // If the tooltip is currently shown, hide it immediately
        if (tooltipPanel.activeSelf)
        {
            StopAndHideTooltip();
        }
        else
        {
            // If the tooltip is not shown, show it for 10 seconds
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }
            displayCoroutine = StartCoroutine(ShowTooltipForDuration(10));
        }
    }

    private IEnumerator ShowTooltipForDuration(float duration)
    {
        PositionTooltip(); // Position the tooltip based on the last button's transform and offset
        tooltipPanel.SetActive(true); // Show the tooltip panel
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        StopAndHideTooltip(); // Hide the tooltip panel after the duration
    }

    private void PositionTooltip()
    {
        if (lastButtonTransform != null)
        {
            // Calculate and set the new position based on the button's position and the offset
            tooltipPanel.transform.position = lastButtonTransform.position + offset;
        }
    }

    private void StopAndHideTooltip()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }
        tooltipPanel.SetActive(false); // Hide the tooltip panel
    }

    public void OnHelpButtonClick(Button button)
    {
        TooltipManager._instance.ToggleTooltip(button.transform);
    }
}