using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;
using VuforiaScripts;
public class LoadMultipleTargetsFromDatabase : MonoBehaviour
{    
    /// <summary>
    /// this will be downloaded as the information from the database so i will know what xml i need to work with
    /// </summary>
    private string databasePathGeneral = $"{Application.streamingAssetsPath}/Vuforia/";
    ////private string databasePath = $"{Application.streamingAssetsPath}/Vuforia/AllOfThem.xml";
    
    public StartStopButtonController startStopButtonController;

    [SerializeField]
    public GameObject ModelContainer;

    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += OnVuforiaInitialized;
    }

    /// <summary>
    /// Starts when the app is initialized!
    /// </summary>
    /// <param name="error"></param>
    void OnVuforiaInitialized(VuforiaInitError error)
    {
        //if (error == VuforiaInitError.NONE)
        //{
        //    CreateIT();
        //}
    }

    public async void CreateIT()
    {
        if (!TryGetModelFoundController(out var modelFoundController))
        {
            Debug.LogError("ModelFoundController not found in the scene!");
            return;
        }

        if (!TryGetDatabaseSelector(out var databaseSelector))
        {
            Debug.LogError("GameController or DatabaseSelectorController component not found!");
            return;
        }

        var selectedDatabasePath = ConstructDatabasePath(databaseSelector);

        if (ModelContainer == null)
        {
            Debug.LogError("ModelContainer not found!");
            return;
        }

        DestroyAllModelTargets();

        foreach (var target in databaseSelector.modelsInDatabase)
        {
            await CreateAndSetupModelTarget(selectedDatabasePath, target, ModelContainer, modelFoundController);
        }

        if (ModelContainer.transform.childCount > 0)
        {
            startStopButtonController.StartModelTargetsTimer();
        }
    }

    public void DestroyAllModelTargets()
    {
        // Iterate through all children and destroy them
        foreach (Transform child in ModelContainer.transform)
        {
            Destroy(child.gameObject);
        }

        if (startStopButtonController != null) // Ensure the reference is set
        {
            startStopButtonController.CheckAndStopTimer(ModelContainer);
        }
    }

    bool TryGetModelFoundController(out ModelFoundController modelFoundController)
    {
        modelFoundController = FindObjectOfType<ModelFoundController>();
        return modelFoundController != null;
    }

    bool TryGetDatabaseSelector(out DatabaseSelectorController databaseSelector)
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        databaseSelector = gameController?.GetComponent<DatabaseSelectorController>();
        return gameController != null && databaseSelector != null;
    }

    string ConstructDatabasePath(DatabaseSelectorController databaseSelector)
    {
        return $"{databasePathGeneral}{databaseSelector.selectedDatabase}.xml";
    }

    async Task CreateAndSetupModelTarget(string databasePath, string targetName, GameObject modelContainer, ModelFoundController modelFoundController)
    {
        try
        {
            var itBehaviour = await VuforiaBehaviour.Instance.ObserverFactory.CreateModelTargetAsync(databasePath, targetName);
            if (itBehaviour == null) throw new System.Exception("Model Target creation failed.");

            itBehaviour.transform.SetParent(modelContainer.transform, false);
            Debug.Log("Target created: " + targetName);

            AttachEventHandlers(itBehaviour, modelFoundController);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error creating model target '{targetName}': {ex.Message}");
        }
    }

    void AttachEventHandlers(ObserverBehaviour itBehaviour, ModelFoundController modelFoundController)
    {
        var customHandler = itBehaviour.gameObject.AddComponent<CustomObserverEventHandler>();
        customHandler.OnModelFound += modelFoundController.NewModelFound;
        customHandler.OnModelLost += modelFoundController.ModelLost;
        itBehaviour.OnTargetStatusChanged += OnTargetStatusChanged; // Assume OnTargetStatusChanged is already defined in the class
    }


    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        Debug.Log($"target status: {behaviour.TargetName} {status.Status}");
    }
}