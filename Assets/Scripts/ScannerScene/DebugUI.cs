using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab; // Assign your TextMeshPro prefab in the inspector
    [SerializeField] private Transform contentContainer; // Assign the ScrollRect's content transform in the inspector
    [SerializeField] private ScrollRect scrollRect; // Assign the ScrollRect component in the inspector

    private void Awake()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Instantiate a new TextMeshPro object for each log message
        var newTextObj = Instantiate(textPrefab, contentContainer);
        var tmpText = newTextObj.GetComponent<TMP_Text>();
        if (tmpText != null)
        {
            tmpText.text = logString;
        }

        // Adjust the ScrollToBottom call to work with the instantiation
        StartCoroutine(ScrollToBottomNextFrame());
    }

    private IEnumerator ScrollToBottomNextFrame()
    {
        // Wait for the end of the frame after all UI updates are done
        yield return new WaitForEndOfFrame();

        // Then set the scroll to the bottom
        scrollRect.verticalNormalizedPosition = 0;

        // Optionally, force update the canvas if the scrolling does not work as expected
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }
}