using UnityEngine;
using UnityEngine.UI;
using System;

public class UIScrollviewController : MonoBehaviour
{
    public GameObject scrollView; // Reference to the ScrollView GameObject.
    public Button showScrollViewButton; // Button to trigger the display of the ScrollView.
    public Button hideScrollViewButton; // Button to hide the ScrollView.
    public Button[] staticButtons; // Array of buttons that will be repositioned based on the ScrollView's state.
    public float paddingTop = 125f; // Space between the ScrollView and the static buttons when the ScrollView is visible at the top.
    public float paddingBottom = 50f; // Space between the bottom of the screen and the buttons when the ScrollView is hidden.

    /// <summary>
    /// Initializes the UI components. Sets initial visibility of the scroll view and buttons,
    /// and assigns button click listeners. Positions buttons at the bottom with paddingBottom.
    /// </summary>
    void Start()
    {
        try
        {
            scrollView.SetActive(false);
            showScrollViewButton.gameObject.SetActive(true);
            hideScrollViewButton.gameObject.SetActive(false);

            showScrollViewButton.onClick.AddListener(ShowScrollView);
            hideScrollViewButton.onClick.AddListener(HideScrollView);

            PositionButtonsAtBottom();
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred during UIScrollviewController setup: {e.Message}");
        }
    }

    /// <summary>
    /// Shows the scroll view, hides the show button, and reveals the hide button.
    /// Repositions the static buttons above the scroll view with paddingTop.
    /// </summary>
    public void ShowScrollView()
    {
        try
        {
            scrollView.SetActive(true);
            hideScrollViewButton.gameObject.SetActive(true);
            showScrollViewButton.gameObject.SetActive(false);
            PositionButtonsAboveScrollView();
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred when showing the ScrollView: {e.Message}");
        }
    }

    /// <summary>
    /// Hides the scroll view, shows the show button, and hides the hide button.
    /// Repositions the static buttons at the bottom of the screen with paddingBottom.
    /// </summary>
    public void HideScrollView()
    {
        try
        {
            scrollView.SetActive(false);
            showScrollViewButton.gameObject.SetActive(true);
            hideScrollViewButton.gameObject.SetActive(false);
            PositionButtonsAtBottom();
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred when hiding the ScrollView: {e.Message}");
        }
    }

    /// <summary>
    /// Positions the static buttons above the scroll view based on its height and paddingTop.
    /// </summary>
    void PositionButtonsAboveScrollView()
    {
        try
        {
            RectTransform scrollViewRect = scrollView.GetComponent<RectTransform>();
            float scrollViewHeight = scrollViewRect.sizeDelta.y;
            float newYPosition = -scrollViewRect.anchoredPosition.y + scrollViewHeight + paddingTop;

            foreach (Button button in staticButtons)
            {
                RectTransform buttonRect = button.GetComponent<RectTransform>();
                buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, newYPosition);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred while positioning the buttons above the ScrollView: {e.Message}");
        }
    }

    /// <summary>
    /// Adjusts the position of static buttons to the bottom of the screen, with paddingBottom.
    /// </summary>
    void PositionButtonsAtBottom()
    {
        try
        {
            foreach (Button button in staticButtons)
            {
                RectTransform buttonRect = button.GetComponent<RectTransform>();
                // Use Screen.height to position buttons relative to the bottom of the screen if needed
                float newYPosition = paddingBottom; // Adjust this based on your UI layout
                buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, newYPosition);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"An error occurred while positioning the buttons at the bottom: {e.Message}");
        }
    }
}
