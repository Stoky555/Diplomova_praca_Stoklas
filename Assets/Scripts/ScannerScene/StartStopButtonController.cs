using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class StartStopButtonController : MonoBehaviour
{
    public TextMeshProUGUI buttonText; // Assign in the inspector
    public TextMeshProUGUI timerText; // Assign in the inspector
    public Button button; // Assign in the inspector

    private float timerStartTime; // When Vuforia tracking started
    private bool timerRunning = false; // Is the timer currently running?
    private Coroutine timerCoroutine; // To handle the continuous timer update

    void Start()
    {
        // Initialize UI to show that tracking will start
        UpdateButtonUI("STOP", Color.red);
    }

    public void ToggleTracking()
    {
        VuforiaBehaviour.Instance.enabled = !VuforiaBehaviour.Instance.enabled; // Toggle Vuforia tracking

        if (VuforiaBehaviour.Instance.enabled)
        {
            Debug.Log("Vuforia tracking resumed.");
            UpdateButtonUI("STOP", Color.red);
            if (!timerRunning) StartTimer(); // Stop the timer if tracking is stopped
        }
        else
        {
            Debug.Log("Vuforia tracking paused.");
            UpdateButtonUI("START", Color.green);
            if (timerRunning) StopTimer(); // Stop the timer if tracking is stopped
        }
    }

    public void ModelFound()
    {
        if (timerRunning)
        {
            StopTimer();
            float timeElapsed = Time.time - timerStartTime;
            Debug.Log($"Model found after {timeElapsed} seconds.");
            timerText.text = $"Time: {timeElapsed:F2} seconds"; // Update timerText with final time
        }
    }

    public void ModelLost()
    {
        StartTimer();
    }

    // Start the timer when ModelTargets are being created
    public void StartModelTargetsTimer()
    {
        if (!timerRunning)
        {
            StartTimer();
        }
    }

    // Call this method when a model is found or when checking if the container is empty
    public void CheckAndStopTimer(GameObject modelContainer)
    {
        if (modelContainer.transform.childCount == 0)
        {
            // If the ModelContainer is empty, don't start the timer
            Debug.Log("No ModelTargets to track.");
            if (timerRunning) StopTimer();
        }
        else if (timerRunning)
        {
            // Stop the timer when a model is found and log time
            StopTimer();
            float timeElapsed = Time.time - timerStartTime;
            Debug.Log($"Model found or confirmed after {timeElapsed} seconds.");
            timerText.text = $"Time: {timeElapsed:F2} seconds";
        }
    }

    private void UpdateButtonUI(string text, Color backgroundColor)
    {
        buttonText.text = text;
        button.image.color = backgroundColor;
    }

    // Make StartTimer() public to be called directly by the Vuforia event
    public void StartTimer()
    {
        // Ensure the timer starts only if it's not already running
        if (!timerRunning)
        {
            timerStartTime = Time.time;
            timerRunning = true;
            timerCoroutine = StartCoroutine(UpdateTimerText());
        }
    }

    // Ensure to unsubscribe from the OnVuforiaStarted event when this GameObject is destroyed
    void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaStarted -= StartTimer;
    }

    private void StopTimer()
    {
        timerRunning = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void ResetTimer()
    {
        if (timerRunning) StopTimer();
        else
        {
            StartTimer();
        }
    }

    private IEnumerator UpdateTimerText()
    {
        while (timerRunning)
        {
            float timeElapsed = Time.time - timerStartTime;
            timerText.text = $"Time: {timeElapsed:F2} seconds"; // Update timerText continuously
            yield return null; // Wait for the next frame
        }
    }
}