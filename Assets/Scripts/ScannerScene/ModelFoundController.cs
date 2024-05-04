using System.Collections;
using TMPro;
using UnityEngine;

namespace VuforiaScripts
{
    public class ModelFoundController : MonoBehaviour
    {
        [SerializeField] private TMP_Text modelFoundText;
        [SerializeField] private StartStopButtonController startStopButtonController; // Link in the Inspector

        private Coroutine messageDisplayCoroutine = null;

        public void NewModelFound(string gameObjectName)
        {
            // Notify StartStopButtonController a model is found
            startStopButtonController.ModelFound();

            // Start or restart the coroutine with the "model found" message
            StartOrRestartMessageCoroutine($"Model found: {gameObjectName}", Color.green);
        }

        public void ModelLost(string gameObjectName)
        {
            // Start or restart the coroutine with the "model lost" message
            StartOrRestartMessageCoroutine($"Model lost: {gameObjectName}", Color.red);

            // Notify StartStopButtonController a model is found
            startStopButtonController.ModelLost();
        }

        private void StartOrRestartMessageCoroutine(string message, Color color)
        {
            if (messageDisplayCoroutine != null)
            {
                StopCoroutine(messageDisplayCoroutine);
            }
            messageDisplayCoroutine = StartCoroutine(DisplayMessageTemporarily(message, color, 3f)); // Display for 3 seconds
        }

        private IEnumerator DisplayMessageTemporarily(string message, Color color, float duration)
        {
            modelFoundText.text = message;
            modelFoundText.color = color;
            yield return new WaitForSeconds(duration);
            modelFoundText.text = "";
            messageDisplayCoroutine = null;
        }
    }
}
