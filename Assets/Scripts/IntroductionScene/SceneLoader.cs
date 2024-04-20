// Using directives to include necessary namespaces
using UnityEngine;
using UnityEngine.SceneManagement;

// Define a public class SceneLoader that inherits from MonoBehaviour
public class SceneLoader : MonoBehaviour
{
    // Public method LoadSceneByIndex that takes an integer sceneIndex as a parameter
    public void LoadSceneByIndex(int sceneIndex)
    {
        // Call the LoadScene method from the SceneManager to load a scene by its index
        SceneManager.LoadScene(sceneIndex);
    }
}